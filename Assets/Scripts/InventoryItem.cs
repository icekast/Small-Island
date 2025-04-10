using UnityEngine;

[System.Serializable]
public enum ItemType
{
    Seed,
    Crop,
    Tool,
    Resource
}
public class InventoryItem
{
    public string itemName;
    public ItemType type;
    public Sprite icon;
    public int quantity = 1;

    // Специфичные для семян параметры
    public GameObject plantPrefab;  // Для семян
    public float growTime;          // Для семян

    public bool IsSeed => plantPrefab != null;
    public int cost;
}