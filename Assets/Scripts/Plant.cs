using UnityEngine;
using System.Collections;

public class Plant : MonoBehaviour
{
    private float growTime;
    private Field parentField;
    private bool isReady = false;
    private string harvestItemID;
    private int harvestAmount;
    private Sprite plantSprite;

    // Инициализация с передачей всех параметров
    public void Init(float growTime, Field field, string harvestItemID, int harvestAmount, Sprite sprite)
    {
        this.growTime = growTime;
        this.parentField = field;
        this.harvestItemID = harvestItemID;
        this.harvestAmount = harvestAmount;
        this.plantSprite = sprite;

        // Устанавливаем спрайт
        if (TryGetComponent<SpriteRenderer>(out var renderer))
        {
            renderer.sprite = sprite;
        }

        StartCoroutine(Grow());
    }

    IEnumerator Grow()
    {
        yield return new WaitForSeconds(growTime);
        isReady = true;
        // Можно добавить визуальные изменения созревшего растения
    }

    public bool Harvest(Inventory inventory)
    {
        if (!isReady || inventory == null) return false;

        inventory.AddItem(harvestItemID, harvestAmount);
        Destroy(gameObject);
        parentField.ClearField();
        return true;
    }
}