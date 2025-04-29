using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapPlacerWithDistanceCheck : MonoBehaviour
{
    public GameObject prefabToPlace;
    public Tilemap tilemap;
    public Transform player;
    public int maxDistance = 2;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
            if (player == null)
                Debug.LogError("Player not found!");
        }
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
        Vector3Int playerCellPosition = tilemap.WorldToCell(player.position);

        if (IsWithinDistance(cellPosition, playerCellPosition, maxDistance))
        {
            if (tilemap.HasTile(cellPosition))
            {
                Vector3 cellCenter = tilemap.GetCellCenterWorld(cellPosition);

                if (CanPlaceObject(cellCenter))
                {
                    Inventory inventory = FindObjectOfType<Inventory>();
                    if (inventory.GetSelectedItem().itemID == "hoe")
                    {
                        Instantiate(prefabToPlace, cellCenter, Quaternion.identity);
                    }
                }
                else
                {
                    Debug.Log("Cannot place here - collision detected");
                }
            }
        }
        else
        {
            Debug.Log($"Too far from player! Max distance is {maxDistance} cells");
        }
    }

    bool IsWithinDistance(Vector3Int pos1, Vector3Int pos2, int maxDist)
    {
        int dx = Mathf.Abs(pos1.x - pos2.x);
        int dy = Mathf.Abs(pos1.y - pos2.y);
        return dx <= maxDist && dy <= maxDist;
    }

    bool CanPlaceObject(Vector3 position)
    {
        Collider2D collider = Physics2D.OverlapPoint(position);
        return collider == null;
    }
}