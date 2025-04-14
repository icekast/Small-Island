using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public string itemID;
    public string displayName;
    public ItemType type;
    public Sprite icon;
    public int quantity;
    public int cost;

    [TextArea] public string description;

    // Seed/Plant related fields
    public float growTime;
    public int baseValue;
    public GameObject plantPrefab;
    public GameObject harvestPrefab;
    public string harvestItemID;  // ID ��������, ������� �������� ��� ����� ������
    public int harvestAmount = 1; // ���������� ����������� ������
    public Sprite plantSprite;    // ������ �������� (��� ����������� �� ����)

    // Properties
    public bool IsStackable => type != ItemType.Tool;
    public bool IsSeed => type == ItemType.Seed;

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
            description = this.description,
            growTime = this.growTime,
            baseValue = this.baseValue,
            plantPrefab = this.plantPrefab,
            harvestPrefab = this.harvestPrefab,
            harvestItemID = this.harvestItemID,
            harvestAmount = this.harvestAmount,
            plantSprite = this.plantSprite
        };
    }
}