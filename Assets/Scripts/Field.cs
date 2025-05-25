using UnityEngine;

public class Field : MonoBehaviour
{
    private Plant currentPlant;
    private bool isPlanted = false;

    public bool PlantSeed(string seedItemID)
    {
        if (isPlanted) return false;

        Inventory inventory = FindObjectOfType<Inventory>();
        if (inventory == null) return false;

        if (!inventory.HasItem(seedItemID)) return false;

        ItemsDatabase.ItemData plantData = ItemsDatabase.Instance.GetItemData(seedItemID);
        if (plantData == null || plantData.type != ItemType.Seed || plantData.plantPrefab == null)
        {
            Debug.LogError($"Invalid seed data for {seedItemID}");
            return false;
        }

        GameObject plantObj = Instantiate(
            plantData.plantPrefab,
            transform.position,
            Quaternion.identity,
            transform
        );

        currentPlant = plantObj.GetComponent<Plant>();
        if (currentPlant != null)
        {
            currentPlant.Initialize(
                field: this,
                initialSprite: plantData.plantSprite,
                plantData.grownSprite,
                growTime: plantData.growTime,
                harvestItemID: plantData.harvestItemID,
                harvestAmount: plantData.harvestAmount
            );
            inventory.RemoveItem(seedItemID, 1);

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
    private void OnMouseDown()
    {
        Inventory inventory = FindObjectOfType<Inventory>();
        if (inventory != null && inventory.GetSelectedItem() != null)
        {
            PlantSeed(inventory.GetSelectedItem().itemID);
        }
    }
}
