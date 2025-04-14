using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    public GameObject slotPrefab;
    public Transform slotParent;
    public Inventory inventory;

    private List<GameObject> slotInstances = new List<GameObject>();

    void OnEnable()
    {
        inventory.onInventoryChanged.AddListener(RefreshUI);
        RefreshUI();
    }

    void OnDisable()
    {
        inventory.onInventoryChanged.RemoveListener(RefreshUI);
    }

    public void RefreshUI()
    {
        // Очистка старых слотов
        foreach (var slot in slotInstances)
        {
            Destroy(slot);
        }
        slotInstances.Clear();

        // Создание новых слотов для каждого предмета
        foreach (InventoryItem item in inventory.GetAllItems())
        {
            GameObject slotGO = Instantiate(slotPrefab, slotParent);

            // Настройка изображения и текста
            Image iconImage = slotGO.GetComponentInChildren<Image>();
            if (iconImage != null && item.icon != null)
            {
                iconImage.sprite = item.icon;
            }

            Text itemText = slotGO.GetComponentInChildren<Text>();
            if (itemText != null)
            {
                itemText.text = item.IsStackable ?
                    $"{item.displayName} ({item.quantity})" :
                    item.displayName;
            }

            Button slotButton = slotGO.GetComponent<Button>();
            if (slotButton != null)
                slotButton.onClick.AddListener(() => inventory.SetSelectedItem(item));


            slotInstances.Add(slotGO);
        }
    }
}