using UnityEngine;

public class Field : MonoBehaviour
{
    private Plant currentPlant;
    private bool isPlanted = false;

    public bool PlantSeed(InventoryItem seedItem)
    {
        if (isPlanted || !seedItem.IsSeed) return false;

        GameObject plantObj = Instantiate(seedItem.plantPrefab, transform.position, Quaternion.identity);
        currentPlant = plantObj.GetComponent<Plant>();

        if (currentPlant != null)
        {
            // Передаем параметры из семени в растение
            currentPlant.harvestItemID = seedItem.harvestItemID;
            currentPlant.harvestAmount = seedItem.harvestAmount;
            currentPlant.growTime = seedItem.growTime;
            currentPlant.Init(this);

            isPlanted = true;
            return true;
        }

        Destroy(plantObj);
        return false;
    }

    public void ClearField()
    {
        isPlanted = false;
        currentPlant = null;
    }
}