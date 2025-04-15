using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ItemsDatabase", menuName = "Inventory/Items Database")]
public class ItemsDatabase : ScriptableObject
{
    [System.Serializable]
    public class ItemData
    {
        [Header("Basic Settings")]
        public string itemID;
        public string displayName;
        public ItemType type;
        public Sprite icon;
        public int cost;

        [Header("Plant Settings")]
        public float growTime;
        public GameObject plantPrefab;
        public string harvestItemID;
        public int harvestAmount = 1;
        public Sprite plantSprite;
    }

    [SerializeField] private List<ItemData> items = new List<ItemData>();

    private static ItemsDatabase _instance;
    public static ItemsDatabase Instance
    {
        get
        {
            if (_instance == null)
                _instance = Resources.Load<ItemsDatabase>("ItemsDatabase");
            return _instance;
        }
    }

    public InventoryItem CreateItemInstance(string itemID, int amount = 1)
    {
        ItemData data = GetItemData(itemID);
        if (data == null) return null;

        return new InventoryItem
        {
            itemID = data.itemID,
            displayName = data.displayName,
            type = data.type,
            icon = data.icon,
            cost = data.cost,
            quantity = amount,
            growTime = data.growTime,
            plantPrefab = data.plantPrefab,
            harvestItemID = data.harvestItemID,
            harvestAmount = data.harvestAmount,
            plantSprite = data.plantSprite
        };
    }
    public ItemData GetItemData(string itemID) => items.Find(i => i.itemID == itemID);
}