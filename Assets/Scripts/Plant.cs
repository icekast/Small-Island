using UnityEngine;
using System.Collections;

public class Plant : MonoBehaviour
{
    [System.Serializable]
    public class HarvestResult
    {
        public InventoryItem item;
        public int amount = 1;
    }

    public HarvestResult harvestResult;

    private float growTime;
    private Field parentField;
    private bool isReady = false;

    public void Init(float growTime, Field field)
    {
        this.growTime = growTime;
        parentField = field;
        StartCoroutine(Grow());
    }

    IEnumerator Grow()
    {
        yield return new WaitForSeconds(growTime);
        isReady = true;
        // Визуальные изменения созревшего растения
    }

    public bool Harvest(Inventory inventory)
    {
        if (!isReady || inventory == null) return false;

        inventory.AddItem(harvestResult.item, harvestResult.amount);
        Destroy(gameObject);
        parentField.ClearField();
        return true;
    }
}