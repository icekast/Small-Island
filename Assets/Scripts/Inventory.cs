using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int initialCapacity = 20;
    [SerializeField] private bool allowStacking = true;
    public int money;

    [Header("Events")]
    public UnityEvent onInventoryChanged;

    private List<InventoryItem> items = new List<InventoryItem>();

    public bool AddItem(string itemID, int amount = 1)
    {
        if (amount <= 0) return false;

        if (ItemsDatabase.Instance == null)
        {
            Debug.LogError("ItemsDatabase not initialized!");
            return false;
        }

        InventoryItem template = ItemsDatabase.Instance.CreateItemInstance(itemID);
        if (template == null)
        {
            Debug.LogWarning($"Item {itemID} not found in database");
            return false;
        }

        if (allowStacking && template.IsStackable)
        {
            InventoryItem existing = items.Find(i => i.itemID == itemID);
            if (existing != null)
            {
                existing.quantity += amount;
                onInventoryChanged.Invoke();
                return true;
            }
        }

        if (items.Count >= initialCapacity)
        {
            Debug.LogWarning("Inventory is full!");
            return false;
        }

        InventoryItem newItem = template.Clone();
        newItem.quantity = amount;
        items.Add(newItem);
        onInventoryChanged.Invoke();
        return true;
    }

    public bool RemoveItem(string itemID, int amount = 1)
    {
        InventoryItem item = items.Find(i => i.itemID == itemID);
        if (item == null) return false;

        if (item.quantity > amount)
        {
            item.quantity -= amount;
        }
        else
        {
            items.Remove(item);
        }

        onInventoryChanged.Invoke();
        return true;
    }

    public List<InventoryItem> GetAllItems() => new List<InventoryItem>(items);
    public InventoryItem GetItem(string itemID) => items.Find(i => i.itemID == itemID);
    public int GetItemCount(string itemID) => GetItem(itemID)?.quantity ?? 0;
    public bool HasItem(string itemID, int minAmount = 1) => GetItemCount(itemID) >= minAmount;
}