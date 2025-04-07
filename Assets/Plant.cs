using UnityEngine;
using System.Collections;

public class Plant : MonoBehaviour
{
    private float growTime;
    private Field parentField;
    private bool isReady = false;

    // Метод для инициализации растения
    public void Init(float growTime, Field field)
    {
        this.growTime = growTime;
        parentField = field;
        StartCoroutine(Grow());
    }

    // Корутин для роста растения
    IEnumerator Grow()
    {
        yield return new WaitForSeconds(growTime);
        isReady = true;
        // Здесь можно сменить спрайт растения, чтобы показать, что оно созрело
        // Например: GetComponent<SpriteRenderer>().sprite = newSprite;
    }

    // Метод для сбора урожая
    public bool Harvest()
    {
        if (!isReady) return false;  // Если растение не готово — не собираем

        // Ищем инвентарь и добавляем предмет (например, "Tomato")
        Inventory inventory = FindObjectOfType<Inventory>();
        inventory.AddItem("Tomato", ItemType.Crop, null, 1); // Пример

        // Уничтожаем растение и очищаем поле
        Destroy(gameObject);
        parentField.ClearField();

        return true;
    }
}
