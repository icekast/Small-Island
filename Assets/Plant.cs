using UnityEngine;
using System.Collections;

public class Plant : MonoBehaviour
{
    private float growTime;
    private Field parentField;
    private bool isReady = false;

    // ����� ��� ������������� ��������
    public void Init(float growTime, Field field)
    {
        this.growTime = growTime;
        parentField = field;
        StartCoroutine(Grow());
    }

    // ������� ��� ����� ��������
    IEnumerator Grow()
    {
        yield return new WaitForSeconds(growTime);
        isReady = true;
        // ����� ����� ������� ������ ��������, ����� ��������, ��� ��� �������
        // ��������: GetComponent<SpriteRenderer>().sprite = newSprite;
    }

    // ����� ��� ����� ������
    public bool Harvest()
    {
        if (!isReady) return false;  // ���� �������� �� ������ � �� ��������

        // ���� ��������� � ��������� ������� (��������, "Tomato")
        Inventory inventory = FindObjectOfType<Inventory>();
        inventory.AddItem("Tomato", ItemType.Crop, null, 1); // ������

        // ���������� �������� � ������� ����
        Destroy(gameObject);
        parentField.ClearField();

        return true;
    }
}
