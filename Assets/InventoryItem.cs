using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public string itemName;
    public ItemType type;
    public Sprite icon;
    public int quantity = 1;

    // ����������� ��� ����� ���������
    public GameObject plantPrefab;  // ��� �����
    public float growTime;          // ��� �����

    public bool IsSeed => plantPrefab != null;
}