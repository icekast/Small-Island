using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public string itemID;
    public string displayName;
    public ItemType type;
    public Sprite icon;
    public int quantity = 1;
    public int cost;
    public float growTime;
    public int baseValue;

    // ��������� ����� ���� ��� ��������
    public GameObject plantPrefab;  // ������ �������� ��� �������
    public GameObject harvestPrefab; // ������ ��� ����� ������

    public bool IsStackable => type != ItemType.Tool;
    public bool IsSeed => type == ItemType.Seed;
    public bool IsEdible => type == ItemType.Food;

    public InventoryItem Clone() => (InventoryItem)this.MemberwiseClone();
}