using UnityEngine;
using System.Collections;

public class Plant : MonoBehaviour
{
    [Header("Settings")]
    public string harvestItemID; // ID ��������, ������� �������� ��� �����
    public int harvestAmount = 1;
    public float growTime = 10f;
    public SpriteRenderer spriteRenderer;
    public Sprite grownSprite; // ������ ���������� ��������

    private bool isReady = false;
    private Field parentField;

    public void Init(Field field)
    {
        parentField = field;
        StartCoroutine(Grow());
    }

    IEnumerator Grow()
    {
        yield return new WaitForSeconds(growTime);
        isReady = true;
        spriteRenderer.sprite = grownSprite; // ������ ������ �� "���������"
    }

    // ���������� ��� ����� �� ��������
    private void OnMouseDown()
    {
        if (isReady)
        {
            Harvest();
        }
    }

    public void Harvest()
    {
        if (!isReady) return;

        // ��������� ������ � ���������
        Inventory inventory = FindObjectOfType<Inventory>();
        if (inventory != null)
        {
            inventory.AddItem(harvestItemID, harvestAmount);
        }

        // ������� ����
        parentField.ClearField();
        Destroy(gameObject);
    }
}