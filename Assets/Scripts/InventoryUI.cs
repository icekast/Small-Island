using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform slotParent;
    [SerializeField] private Inventory inventory;
    [SerializeField] private Color selectionColor = new Color(1, 0.8f, 0, 0.3f);

    private List<GameObject> slotInstances = new List<GameObject>();

    private void OnEnable() => inventory.onInventoryChanged.AddListener(RefreshUI);
    private void OnDisable() => inventory.onInventoryChanged.RemoveListener(RefreshUI);

    public void RefreshUI()
    {
        ClearSlots();
        CreateSlots();
    }

    private void ClearSlots()
    {
        foreach (var slot in slotInstances)
        {
            Destroy(slot);
        }
        slotInstances.Clear();
    }

    private void CreateSlots()
    {
        foreach (var item in inventory.GetAllItems())
        {
            GameObject slotGO = Instantiate(slotPrefab, slotParent);
            SetupSlot(slotGO, item);
            slotInstances.Add(slotGO);
        }
    }

    private void SetupSlot(GameObject slotGO, InventoryItem item)
    {
        Image iconImage = slotGO.transform.Find("Icon")?.GetComponent<Image>();
        Text itemText = slotGO.GetComponentInChildren<Text>();
        Image background = slotGO.GetComponent<Image>();

        if (iconImage != null)
        {
            iconImage.sprite = item.icon;
            iconImage.color = item.icon != null ? Color.white : Color.clear;
        }

        if (itemText != null)
        {
            itemText.text = item.IsStackable ? $"{item.displayName} ({item.quantity})" : item.displayName;
        }

        Button button = slotGO.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(() => {
                inventory.SetSelectedItem(item);
                RefreshUI();
            });
        }

        if (background != null)
        {
            var selected = inventory.GetSelectedItem();
            background.color = (selected != null && selected.itemID == item.itemID) ? selectionColor : new Color(1f, 1f, 1f, 0f);
        }
    }
}