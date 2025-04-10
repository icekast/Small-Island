using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ItemPickup2D : MonoBehaviour
{
    public string itemID = "tomato_seed";
    public int amount = 1;

    private void Awake()
    {
        // Гарантируем, что коллайдер - триггер
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, что столкнулись с игроком
        if (!other.CompareTag("Player")) return;

        // Получаем инвентарь у игрока
        Inventory inventory = FindObjectOfType<Inventory>();
        if (inventory != null)
        {
            // Пытаемся добавить предмет
            if (inventory.AddItem(itemID, amount))
            {
                Destroy(gameObject); // Уничтожаем предмет при успехе
            }
        }
    }
}