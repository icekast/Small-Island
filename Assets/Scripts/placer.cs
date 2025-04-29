using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapObjectPlacer : MonoBehaviour
{
    public GameObject prefabToPlace;
    public Tilemap tilemap; // ������ �� Tilemap

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlaceObjectOnTilemap();
        }
    }

    void PlaceObjectOnTilemap()
    {
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPosition = tilemap.WorldToCell(mouseWorldPos);

        // ���������, ���� �� ���� � ���� ������� (���� �����)
        if (tilemap.HasTile(cellPosition))
        {
            // �������� ����� ������
            Vector3 cellCenter = tilemap.GetCellCenterWorld(cellPosition);

            // ���������, ����� �� ���������� ������
            if (CanPlaceObject(cellCenter))
            {
                Inventory inventory = FindObjectOfType<Inventory>();
                if (inventory.GetSelectedItem().itemID == "hoe")
                {
                    Instantiate(prefabToPlace, cellCenter, Quaternion.identity);
                }
            }
        }
    }

    bool CanPlaceObject(Vector3 position)
    {
        Collider2D collider = Physics2D.OverlapPoint(position);
        return collider == null;
    }
}
