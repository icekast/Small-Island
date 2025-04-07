using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int money = 100;
    public List<Seed> ownedSeeds = new List<Seed>();
    public List<InventoryItem> items = new List<InventoryItem>();

    public void AddItem(string name, ItemType type, Sprite icon, int amount = 1)
    {
        InventoryItem existing = items.Find(i => i.itemName == name);
        if (existing != null)
        {
            existing.quantity += amount;
        }
        else
        {
            items.Add(new InventoryItem
            {
                itemName = name,
                quantity = amount,
                type = type,
                icon = icon
            });
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

    public int GetItemCount(string name)
    {
        InventoryItem item = items.Find(i => i.itemName == name);
        return item != null ? item.quantity : 0;
    }

    public bool HasItem(string name, int amount = 1)
    {
        return GetItemCount(name) >= amount;
    }
}
