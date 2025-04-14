using UnityEngine;

public class Field : MonoBehaviour
{
    private Plant currentPlant;
    private bool isPlanted = false;
    private Inventory inventory; // ������ �� ���������

    private void Start()
    {
        // ������� ��������� ������������� (��� ��������� � ����������)
        inventory = FindObjectOfType<Inventory>();
    }

    public bool PlantSeed(InventoryItem seedItem)
    {
        // ��������� ������� �������
        if (isPlanted || !seedItem.IsSeed || seedItem.plantPrefab == null)
            return false;

        // ���������, ���� �� ������ � ���������
        if (!inventory.HasItem(seedItem.itemID, 1))
        {
            Debug.Log($"��� ����� {seedItem.itemID} � ���������!");
            return false;
        }

        // ������� ��������
        GameObject plantObj = Instantiate(seedItem.plantPrefab, transform.position, Quaternion.identity);
        currentPlant = plantObj.GetComponent<Plant>();

        if (currentPlant != null)
        {
            // ������� 1 ���� �� ���������
            inventory.RemoveItem(seedItem.itemID, 1);
            
            currentPlant.Init(seedItem.growTime, this);
            isPlanted = true;
            return true;
        }

        Destroy(plantObj);
        return false;
    }

    // ���������� ����� (�������� ��������� �� ����!)
    private void OnMouseDown()
    {
        if (isPlanted) return;

        // �������� ��������� ������� �� ���������
        InventoryItem selectedItem = inventory.GetSelectedItem();
        if (selectedItem != null)
        {
            PlantSeed(selectedItem);
        }
        else
        {
            Debug.Log("������� �������� ���� � ���������!");
        }
    }

    public void ClearField()
    {
        isPlanted = false;
        currentPlant = null;
    }

    public bool IsPlanted => isPlanted;
}