using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapObjectPlacer : MonoBehaviour
{
    public GameObject prefabToPlace;
    public Tilemap tilemap; // Ссылка на Tilemap

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

        // Проверяем, есть ли тайл в этой позиции (если нужно)
        if (tilemap.HasTile(cellPosition))
        {
            // Получаем центр клетки
            Vector3 cellCenter = tilemap.GetCellCenterWorld(cellPosition);

            // Проверяем, можно ли разместить объект
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
