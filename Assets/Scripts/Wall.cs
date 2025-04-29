using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))] // Гарантируем, что у объекта есть спрайт
public class SpriteBounds : MonoBehaviour
{
    [Header("Настройки границ")]
    [SerializeField] private float wallThickness = 0.2f;
    [SerializeField] private Color wallColor = new Color(1, 0, 0, 0.2f);
    [SerializeField] private bool drawDebug = true;

    void Start()
    {
        CreateWallsAroundSprite();
    }

    void CreateWallsAroundSprite()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null || spriteRenderer.sprite == null)
        {
            Debug.LogError("SpriteRenderer или спрайт не найден!");
            return;
        }

        // Получаем реальные размеры спрайта с учётом масштаба
        Bounds bounds = spriteRenderer.bounds;
        Vector2 size = bounds.size;

        // Создаём стены вокруг спрайта
        CreateWall("TopWall", new Vector2(0, size.y / 2), new Vector2(size.x, wallThickness));
        CreateWall("BottomWall", new Vector2(0, -size.y / 2), new Vector2(size.x, wallThickness));
        CreateWall("LeftWall", new Vector2(-size.x / 2, 0), new Vector2(wallThickness, size.y));
        CreateWall("RightWall", new Vector2(size.x / 2, 0), new Vector2(wallThickness, size.y));
    }

    void CreateWall(string name, Vector2 localPosition, Vector2 size)
    {
        GameObject wall = new GameObject(name);
        wall.transform.SetParent(transform);
        wall.transform.localPosition = localPosition;

        // Настройка коллайдера
        BoxCollider2D collider = wall.AddComponent<BoxCollider2D>();
        collider.size = size;

        // Визуализация (опционально)
        if (drawDebug)
        {
            SpriteRenderer sr = wall.AddComponent<SpriteRenderer>();
            sr.sprite = Sprite.Create(
                Texture2D.whiteTexture,
                new Rect(0, 0, 1, 1),
                new Vector2(0.5f, 0.5f)
            );
            sr.color = wallColor;
            sr.drawMode = SpriteDrawMode.Sliced;
            sr.size = size;
            sr.sortingOrder = -1; // Чтобы стены были под основным спрайтом
        }
    }

    // Для визуализации в редакторе
    void OnDrawGizmosSelected()
    {
        if (!drawDebug) return;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null || sr.sprite == null) return;

        Bounds bounds = sr.bounds;
        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Gizmos.DrawWireCube(bounds.center, bounds.size + new Vector3(wallThickness * 2, wallThickness * 2, 0));
    }
}