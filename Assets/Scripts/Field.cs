using UnityEngine;

public class Field : MonoBehaviour
{
    private Plant currentPlant;
    private bool isPlanted = false;

    public bool PlantSeed(InventoryItem seedItem)
    {
        if (isPlanted || !seedItem.IsSeed || seedItem.plantPrefab == null)
            return false;

        GameObject plantObj = Instantiate(seedItem.plantPrefab, transform.position, Quaternion.identity);
        currentPlant = plantObj.GetComponent<Plant>();

        if (currentPlant != null)
        {
            currentPlant.Init(seedItem.growTime, this);
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

    public bool IsPlanted => isPlanted;
}