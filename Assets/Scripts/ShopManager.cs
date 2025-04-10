using UnityEngine;

public class ShopManager : MonoBehaviour
{
    // �������� ������ Seed �� InventoryItem
    public InventoryItem[] availableSeeds;
    public int cropSellPrice = 20;

    public bool BuySeed(int index, Inventory inventory)
    {
        // ��������� ���������� �������
        if (index < 0 || index >= availableSeeds.Length)
            return false;

        InventoryItem seed = availableSeeds[index];

        // ���������, ��� ��� ������������� ����
        if (!seed.IsSeed)
        {
            Debug.LogError($"Item {seed.itemName} is not a seed!");
            return false;
        }

        if (inventory.money >= seed.cost)
        {
            inventory.money -= seed.cost;
            // ���������� ����� ����� AddItem
            inventory.AddItem(seed, 1);
            return true;
        }
        return false;
    }

    public void SellAllCrops(Inventory inventory)
    {
        if (inventory == null) return;

        int totalValue = 0;

        // ���������� �������� ����� �������� ����
        for (int i = inventory.items.Count - 1; i >= 0; i--)
        {
            var item = inventory.items[i];
            if (item.type == ItemType.Crop)
            {
                totalValue += item.quantity * cropSellPrice;
                inventory.RemoveItem(item.itemName, item.quantity);
            }
        }

        if (totalValue > 0)
        {
            inventory.money += totalValue;
            FindObjectOfType<InventoryUI>()?.RefreshUI();
        }
    }
}