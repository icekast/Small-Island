using UnityEngine;

public class Field : MonoBehaviour
{
    private bool isPlanted = false;
    private Plant currentPlant;

    public void PlantSeed(InventoryItem seedItem)
    {
        if (isPlanted || !seedItem.IsSeed) return;

        GameObject plantGO = Instantiate(seedItem.plantPrefab, transform.position, Quaternion.identity);
        currentPlant = plantGO.GetComponent<Plant>();
        currentPlant.Init(seedItem.growTime, this);
        isPlanted = true;
    }

    public void TryPlantFromInventory(string seedName, Inventory inventory)
    {
        if (!isPlanted && inventory.HasItem(seedName))
        {
            InventoryItem seedItem = inventory.GetSeed(seedName);
            if (seedItem != null)
            {
                inventory.RemoveItem(seedName);
                PlantSeed(seedItem);
            }
        }
    }

    public void ClearField()
    {
        isPlanted = false;
        currentPlant = null;
    }
}