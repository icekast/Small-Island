using UnityEngine;
using System.Collections;

public class Plant : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Growth Settings")]
    [SerializeField] private Sprite grownSprite;

    private string harvestItemID;
    private int harvestAmount;
    private Field parentField;

    public void Initialize(Field field, Sprite initialSprite, float growTime, string harvestItemID, int harvestAmount)
    {
        this.parentField = field;
        this.harvestItemID = harvestItemID;
        this.harvestAmount = harvestAmount;

        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = initialSprite;
        }

        StartCoroutine(GrowToMaturity(growTime));
    }

    private IEnumerator GrowToMaturity(float growTime)
    {
        yield return new WaitForSeconds(growTime);

        if (spriteRenderer != null && grownSprite != null)
        {
            spriteRenderer.sprite = grownSprite;
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
        return spriteRenderer != null && spriteRenderer.sprite == grownSprite;
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
