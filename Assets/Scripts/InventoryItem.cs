using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    // Базовые свойства
    public string itemID;
    public string displayName;
    public ItemType type;
    public Sprite icon;
    public int quantity;
    public int cost;

    // Свойства для растений
    public float growTime;
    public GameObject plantPrefab;
    public string harvestItemID;
    public int harvestAmount;
    public Sprite plantSprite;

    // Вычисляемые свойства
    public bool IsStackable => type != ItemType.Tool;
    public bool IsSeed => type == ItemType.Seed;
    public int MaxStackSize => IsStackable ? 99 : 1;

    public InventoryItem Clone()
    {
        return new InventoryItem
        {
            itemID = this.itemID,
            displayName = this.displayName,
            type = this.type,
            icon = this.icon,
            quantity = this.quantity,
            cost = this.cost,
            growTime = this.growTime,
            plantPrefab = this.plantPrefab,
            harvestItemID = this.harvestItemID,
            harvestAmount = this.harvestAmount,
            plantSprite = this.plantSprite
        };
    }
}