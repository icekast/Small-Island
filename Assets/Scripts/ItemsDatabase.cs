using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ItemsDatabase", menuName = "Inventory/Items Database")]
public class ItemsDatabase : ScriptableObject
{
    [System.Serializable]
    public class ItemData
    {
        public string itemID;
        public string displayName;
        public ItemType type;
        public Sprite icon;
        [TextArea] public string description;
        public int cost;

        [Header("Stack Settings")]
        public bool isStackable = true;
        public int maxStackSize = 99;

        [Header("Prefabs")]
        public GameObject worldPrefab;
        public GameObject inventoryPrefab;

        [Header("Growth Settings")]
        public float growTime = 60f;
        public int baseValue = 10;

        [Header("Plant Settings")]
        public GameObject plantPrefab;  // Добавляем это поле
        public GameObject harvestPrefab; // И это поле
    }

    [SerializeField] private List<ItemData> items = new List<ItemData>();

    private static ItemsDatabase _instance;
    public static ItemsDatabase Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<ItemsDatabase>("ItemsDatabase");
                if (_instance == null)
                    Debug.LogError("ItemsDatabase не найдена!");
            }
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
            quantity = Mathf.Clamp(amount, 1, data.maxStackSize),
            growTime = data.growTime,
            baseValue = data.baseValue,
            cost = data.cost,
            plantPrefab = data.plantPrefab,    // Добавляем эту строку
            harvestPrefab = data.harvestPrefab // И эту строку
        };
    }

    public ItemData GetItemData(string itemID) => items.Find(i => i.itemID == itemID);
    public bool ItemExists(string itemID) => items.Exists(i => i.itemID == itemID);
    public GameObject GetWorldPrefab(string itemID) => GetItemData(itemID)?.worldPrefab;

#if UNITY_EDITOR
    public void AddNewItem(ItemType type)
    {
        items.Add(new ItemData
        {
            itemID = $"new_{type}_{System.Guid.NewGuid().ToString("N").Substring(0, 8)}",
            displayName = $"New {type}",
            type = type,
            isStackable = type != ItemType.Tool,
            maxStackSize = type == ItemType.Tool ? 1 : 99
        });
    }
#endif

}