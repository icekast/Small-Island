using UnityEngine;

public class Field : MonoBehaviour
{
    private Plant currentPlant;
    private bool isPlanted = false;

    public bool PlantSeed(string seedItemID)
    {
        // Проверяем условия посадки
        if (isPlanted) return false;

        Inventory inventory = FindObjectOfType<Inventory>();
        if (inventory == null) return false;

        // Проверяем наличие семян в инвентаре
        if (!inventory.HasItem(seedItemID)) return false;

        // Получаем данные из базы
        ItemsDatabase.ItemData plantData = ItemsDatabase.Instance.GetItemData(seedItemID);
        if (plantData == null || plantData.type != ItemType.Seed || plantData.plantPrefab == null)
        {
            Debug.LogError($"Invalid seed data for {seedItemID}");
            return false;
        }

        // Создаем растение
        GameObject plantObj = Instantiate(
            plantData.plantPrefab,
            transform.position,
            Quaternion.identity,
            transform
        );

        currentPlant = plantObj.GetComponent<Plant>();
        if (currentPlant != null)
        {
            // Инициализируем растение
            currentPlant.Initialize(
                field: this,
                initialSprite: plantData.plantSprite,
                plantData.grownSprite,
                growTime: plantData.growTime,
                harvestItemID: plantData.harvestItemID,
                harvestAmount: plantData.harvestAmount
            );

            // Удаляем семя из инвентаря
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

    // Для взаимодействия через клик
    private void OnMouseDown()
    {
        Inventory inventory = FindObjectOfType<Inventory>();
        if (inventory != null && inventory.GetSelectedItem() != null)
        {
            PlantSeed(inventory.GetSelectedItem().itemID);
        }
    }
}
