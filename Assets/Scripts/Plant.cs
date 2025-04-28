using UnityEngine;
using System.Collections;

public class Plant : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Vector2 fieldSize;

    private string harvestItemID;
    private int harvestAmount;
    private Field parentField;
    private Sprite grownSprite;
    private bool isGrown = false;

    public void Initialize(Field field, Sprite initialSprite, Sprite grownSprite, float growTime, string harvestItemID, int harvestAmount)
    {
        this.parentField = field;
        this.harvestItemID = harvestItemID;
        this.harvestAmount = harvestAmount;
        this.grownSprite = grownSprite;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = initialSprite;
        spriteRenderer.sortingOrder = 1;

        AdjustSpriteSizeToField(field);
        StartCoroutine(GrowToMaturity(growTime));
    }

    private IEnumerator GrowToMaturity(float growTime)
    {
        yield return new WaitForSeconds(growTime);

        if (spriteRenderer != null && grownSprite != null)
        {
            spriteRenderer.sprite = grownSprite;
            isGrown = true; // <-- отмечаем как зрелое
        }
    }

    private void OnMouseDown()
    {
        if (IsReadyToHarvest())
        {
            Harvest();
        }
    }

    private bool IsReadyToHarvest()
    {
        return isGrown;
    }

    private void Harvest()
    {
        Inventory inventory = FindObjectOfType<Inventory>();
        if (inventory != null)
        {
            inventory.AddItem(harvestItemID, harvestAmount);
        }

        parentField.ClearField();
        Destroy(gameObject);
    }
    private void AdjustSpriteSizeToField(Field field)
    {
        if (spriteRenderer == null || spriteRenderer.sprite == null) return;

        SpriteRenderer fieldRenderer = field.GetComponent<SpriteRenderer>();
        if (fieldRenderer == null || fieldRenderer.sprite == null) return;

        // Размеры спрайта поля и растения
        Vector2 fieldSize = fieldRenderer.sprite.bounds.size;
        Vector2 plantSize = spriteRenderer.sprite.bounds.size;

        // Вычисляем масштаб
        Vector3 newScale = new Vector3(
            fieldSize.x / plantSize.x,
            fieldSize.y / plantSize.y,
            1f
        );

        transform.localScale = newScale;

        // Обновляем BoxCollider2D
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider != null)
        {
            boxCollider.size = spriteRenderer.sprite.bounds.size;
            boxCollider.offset = spriteRenderer.sprite.bounds.center;
        }
    }
}
