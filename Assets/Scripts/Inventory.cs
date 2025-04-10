using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int money = 100;
    public List<InventoryItem> items = new List<InventoryItem>();

    public void AddItem(InventoryItem newItem, int amount = 1)
    {
        InventoryItem existing = items.Find(i => i.itemName == newItem.itemName);
        if (existing != null)
        {
            existing.quantity += amount;
        }
        else
        {
            var copy = new InventoryItem()
            {
                itemName = newItem.itemName,
                type = newItem.type,
                icon = newItem.icon,
                quantity = amount,
                plantPrefab = newItem.plantPrefab,
                growTime = newItem.growTime
            };
            items.Add(copy);
        }
    }

    public bool RemoveItem(string name, int amount = 1)
    {
        InventoryItem existing = items.Find(i => i.itemName == name);
        if (existing != null && existing.quantity >= amount)
        {
            existing.quantity -= amount;
            if (existing.quantity <= 0)
                items.Remove(existing);
            return true;
        }
        return false;
    }

    public InventoryItem GetSeed(string seedName)
    {
        return items.Find(i => i.itemName == seedName && i.IsSeed);
    }

    public bool HasItem(string name, int amount = 1)
    {
        InventoryItem item = items.Find(i => i.itemName == name);
        return item != null && item.quantity >= amount;
    }
}