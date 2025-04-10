using UnityEngine;
using System.Collections;

public class Plant : MonoBehaviour
{
    public string cropItemID;
    public int harvestAmount = 1;
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

        inventory.AddItem(cropItemID, harvestAmount);
        Destroy(gameObject);
        parentField.ClearField();
        return true;
    }
}