using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    public string[] availableSeedIDs;
    public int cropSellPrice = 20;
    [Header("UI Elements")]
    public GameObject shopPanel; // Ваша панель магазина
    public Button ShopButton; // Кнопка открытия магазина
    public Button CloseShop; // Кнопка закрытия на панели

    [Header("Основные компоненты")]
    public Transform shopItemsContainer;     // Родительский объект для товаров (например, VerticalLayoutGroup)
    public GameObject shopItemPrefab;        // Префаб UI-карточки товара (должен содержать ShopItemUI)

    [Header("Товары для продажи")]
    public List<string> sellableItems;   // ID товаров, которые можно продавать

    [Header("Текст денег")]
    public Text moneyText;

    private Inventory inventory;

    void Start()
    {
        // Скрываем панель при старте
        shopPanel.SetActive(false);

        GenerateShopItems();

        // Назначаем обработчики кликов
        ShopButton.onClick.AddListener(OpenShop);
        CloseShop.onClick.AddListener(closeShop);
    }

    // Создаёт элементы магазина на основе базы данных
    private void GenerateShopItems()
    {
        foreach (string itemID in sellableItems)
        {
            ItemsDatabase.ItemData itemData = ItemsDatabase.Instance.GetItemData(itemID);
            if (itemData == null) continue;

            // Создаём UI-элемент товара
            GameObject itemGO = Instantiate(shopItemPrefab, shopItemsContainer);
            ShopItemUI itemUI = itemGO.GetComponent<ShopItemUI>();

            // Настраиваем отображение
            itemUI.Setup(itemData, () => OnItemPurchased(itemID));
        }
    }

    // Обработчик покупки
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
        shopPanel.SetActive(true);
        // Дополнительная логика при открытии магазина
        Debug.Log("Магазин открыт");
    }
    public void closeShop()
    {
        shopPanel.SetActive(false);
        // Дополнительная логика при закрытии магазина
        Debug.Log("Магазин закрыт");
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
                totalValue += item.quantity * cropSellPrice;
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