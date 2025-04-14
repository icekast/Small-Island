using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public string[] availableSeedIDs;
    public int cropSellPrice = 20;

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