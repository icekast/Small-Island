using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    public GameObject slotPrefab;
    public Transform slotParent;
    public Inventory inventory;

    private List<GameObject> slotInstances = new List<GameObject>();

    void Start()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        foreach (var slot in slotInstances)
            Destroy(slot);
        slotInstances.Clear();

        foreach (InventoryItem item in inventory.items)
        {
            GameObject slotGO = Instantiate(slotPrefab, slotParent);
            slotGO.GetComponentInChildren<Image>().sprite = item.icon;
            slotGO.GetComponentInChildren<Text>().text = $"{item.itemName} ({item.quantity})";

            slotInstances.Add(slotGO);
        }
    }
}