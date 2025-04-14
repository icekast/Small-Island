using UnityEngine;

public class Field : MonoBehaviour
{
    private Plant currentPlant;
    private bool isPlanted = false;
    private Inventory inventory; // Ссылка на инвентарь

    private void Start()
    {
        // Находим инвентарь автоматически (или назначаем в инспекторе)
        inventory = FindObjectOfType<Inventory>();
    }

    public bool PlantSeed(InventoryItem seedItem)
    {
        // Проверяем условия посадки
        if (isPlanted || !seedItem.IsSeed || seedItem.plantPrefab == null)
            return false;

        // Проверяем, есть ли семена в инвентаре
        if (!inventory.HasItem(seedItem.itemID, 1))
        {
            Debug.Log($"Нет семян {seedItem.itemID} в инвентаре!");
            return false;
        }

        // Создаем растение
        GameObject plantObj = Instantiate(seedItem.plantPrefab, transform.position, Quaternion.identity);
        currentPlant = plantObj.GetComponent<Plant>();

        if (currentPlant != null)
        {
            // Убираем 1 семя из инвентаря
            inventory.RemoveItem(seedItem.itemID, 1);
            
            currentPlant.Init(seedItem.growTime, this);
            isPlanted = true;
            return true;
        }

        Destroy(plantObj);
        return false;
    }

    // Обработчик клика (добавьте коллайдер на поле!)
    private void OnMouseDown()
    {
        if (isPlanted) return;

        // Получаем выбранный предмет из инвентаря
        InventoryItem selectedItem = inventory.GetSelectedItem();
        if (selectedItem != null)
        {
            PlantSeed(selectedItem);
        }
        else
        {
            Debug.Log("Сначала выберите семя в инвентаре!");
        }
    }

    public void ClearField()
    {
        isPlanted = false;
        currentPlant = null;
    }

    public bool IsPlanted => isPlanted;
}