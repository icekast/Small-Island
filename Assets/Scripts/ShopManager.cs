using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    public string[] availableSeedIDs;
    public int cropSellPrice = 20;
    [Header("UI Elements")]
    public GameObject shopPanel; // ���� ������ ��������
    public Button ShopButton; // ������ �������� ��������
    public Button CloseShop; // ������ �������� �� ������

    [Header("�������� ����������")]
    public Transform shopItemsContainer;     // ������������ ������ ��� ������� (��������, VerticalLayoutGroup)
    public GameObject shopItemPrefab;        // ������ UI-�������� ������ (������ ��������� ShopItemUI)

    [Header("������ ��� �������")]
    public List<string> sellableItems;   // ID �������, ������� ����� ���������

    [Header("����� �����")]
    public Text moneyText;

    private Inventory inventory;

    void Start()
    {
        // �������� ������ ��� ������
        shopPanel.SetActive(false);

        GenerateShopItems();

        // ��������� ����������� ������
        ShopButton.onClick.AddListener(OpenShop);
        CloseShop.onClick.AddListener(closeShop);
    }

    // ������ �������� �������� �� ������ ���� ������
    private void GenerateShopItems()
    {
        foreach (string itemID in sellableItems)
        {
            ItemsDatabase.ItemData itemData = ItemsDatabase.Instance.GetItemData(itemID);
            if (itemData == null) continue;

            // ������ UI-������� ������
            GameObject itemGO = Instantiate(shopItemPrefab, shopItemsContainer);
            ShopItemUI itemUI = itemGO.GetComponent<ShopItemUI>();

            // ����������� �����������
            itemUI.Setup(itemData, () => OnItemPurchased(itemID));
        }
    }

    // ���������� �������
    private void OnItemPurchased(string itemID)
    {
        Inventory inventory = FindObjectOfType<Inventory>();
        ItemsDatabase.ItemData itemData = ItemsDatabase.Instance.GetItemData(itemID);

        if (inventory.money >= itemData.cost)
        {
            inventory.money -= itemData.cost;
            inventory.AddItem(itemID, 1);
            Debug.Log($"�������: {itemData.displayName}");
        }
        else
        {
            Debug.Log("������������ �����!");
        }
    }

    public void OpenShop()
    {
        shopPanel.SetActive(true);
        // �������������� ������ ��� �������� ��������
        Debug.Log("������� ������");
    }
    public void closeShop()
    {
        shopPanel.SetActive(false);
        // �������������� ������ ��� �������� ��������
        Debug.Log("������� ������");
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