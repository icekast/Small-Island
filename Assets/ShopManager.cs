using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public Seed[] availableSeeds;
    public int cropSellPrice = 20;

    public bool BuySeed(int index, Inventory inventory)
    {
        Seed seed = availableSeeds[index];
        if (inventory.money >= seed.cost)
        {
            inventory.money -= seed.cost;
            inventory.AddItem(seed.seedName, ItemType.Seed, seed.icon, 1);
            return true;
        }
        return false;
    }

    public void SellAllCrops(Inventory inventory)
    {
        for (int i = inventory.items.Count - 1; i >= 0; i--)
        {
            var item = inventory.items[i];
            if (item.type == ItemType.Crop)
            {
                inventory.money += item.quantity * cropSellPrice;
                inventory.items.RemoveAt(i);
            }
        }

        FindObjectOfType<InventoryUI>().RefreshUI();
    }
}
