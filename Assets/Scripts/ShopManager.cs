using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    public string[] availableSeedIDs;
    [Header("UI Elements")]
    public GameObject shopPanel;
    public Button ShopButton;

    [Header("Основные компоненты")]
    public Transform shopItemsContainer; 
    public GameObject shopItemPrefab;

    [Header("Товары для продажи")]
    public List<string> sellableItems;

    [Header("Текст денег")]
    public Text moneyText;

    private Inventory inventory;
    private bool isActive = false;

    void Start()
    {
        shopPanel.SetActive(false);

        GenerateShopItems();

        ShopButton.onClick.AddListener(OpenShop);
    }

    private void GenerateShopItems()
    {
        foreach (string itemID in sellableItems)
        {
            ItemsDatabase.ItemData itemData = ItemsDatabase.Instance.GetItemData(itemID);
            if (itemData == null) continue;

            GameObject itemGO = Instantiate(shopItemPrefab, shopItemsContainer);
            ShopItemUI itemUI = itemGO.GetComponent<ShopItemUI>();

            itemUI.Setup(itemData, () => OnItemPurchased(itemID));
        }
    }

    private void OnItemPurchased(string itemID)
    {
        Inventory inventory = FindObjectOfType<Inventory>();
        ItemsDatabase.ItemData itemData = ItemsDatabase.Instance.GetItemData(itemID);

        if (inventory.money >= itemData.cost)
        {
            inventory.money -= itemData.cost;
            inventory.AddItem(itemID, 1);
            Debug.Log($"Куплено: {itemData.displayName}");
        }
        else
        {
            Debug.Log("Недостаточно денег!");
        }
    }

    public void OpenShop()
    {
        if (isActive)
        {
            shopPanel.SetActive(false);
            isActive = false;
        }
        else
        {
            shopPanel.SetActive(true);
            isActive = true;
        }
    }

    public bool BuySeed(int index, Inventory inventory)
    {
        if (index < 0 || index >= availableSeedIDs.Length)
            return false;

        string seedID = availableSeedIDs[index];
        InventoryItem seed = ItemsDatabase.Instance.CreateItemInstance(seedID);

        if (!seed.IsSeed)
        {
            Debug.LogError($"Item {seed.displayName} is not a seed!");
            return false;
        }

        if (inventory.money >= seed.cost)
        {
            inventory.money -= seed.cost;
            inventory.AddItem(seedID, 1);
            return true;
        }
        return false;
    }

    public void SellAllCrops(Inventory inventory)
    {
        if (inventory == null) return;

        int totalValue = 0;
        var itemsCopy = inventory.GetAllItems();

        foreach (var item in itemsCopy)
        {
            if (item.type == ItemType.Crop)
            {
                totalValue += item.quantity * item.cost;
                inventory.RemoveItem(item.itemID, item.quantity);
            }
        }

        if (totalValue > 0)
        {
            inventory.money += totalValue;
            inventory.onInventoryChanged.Invoke();
        }
    }
}