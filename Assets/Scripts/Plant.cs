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

        StartCoroutine(GrowToMaturity(growTime));
    }

    private IEnumerator GrowToMaturity(float growTime)
    {
        yield return new WaitForSeconds(growTime);

        if (spriteRenderer != null && grownSprite != null)
        {
            spriteRenderer.sprite = grownSprite;
            isGrown = true;
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
}
