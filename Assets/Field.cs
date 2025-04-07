using UnityEngine;

public class Field : MonoBehaviour
{
    private bool isPlanted = false;
    private Plant currentPlant;

    public void PlantSeed(Seed seed)
    {
        if (isPlanted) return;

        GameObject plantGO = Instantiate(seed.plantPrefab, transform.position, Quaternion.identity);
        currentPlant = plantGO.GetComponent<Plant>();
        currentPlant.Init(seed.growTime, this);
        isPlanted = true;
    }

    public void ClearField()
    {
        isPlanted = false;
        currentPlant = null;
    }

    public void TryPlantFromInventory(string seedName, Inventory inventory)
    {
        if (!isPlanted && inventory.HasItem(seedName))
        {
            Seed seed = inventory.ownedSeeds.Find(s => s.seedName == seedName);
            if (seed != null)
            {
                inventory.RemoveItem(seedName);
                PlantSeed(seed);
            }
        }
    }
}
