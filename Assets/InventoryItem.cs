using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public string itemName;
    public Sprite icon;
    public int quantity;
    public ItemType type;
}

public enum ItemType
{
    Seed,
    Crop
}