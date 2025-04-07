using UnityEngine;
using UnityEngine.UI;

public class TooltipUI : MonoBehaviour
{
    public Text itemNameText;
    public Text itemDescText;

    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        gameObject.SetActive(false);
    }

    public void ShowTooltip(InventoryItem item)
    {
        itemNameText.text = item.itemName;
        itemDescText.text = GetDescription(item);
        gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        Vector2 pos = Input.mousePosition;
        rectTransform.position = pos + new Vector2(10f, -10f);
    }

    private string GetDescription(InventoryItem item)
    {
        string desc = item.type == ItemType.Seed ? "Семена для посадки" : "Собранный урожай";
        desc += $"\nКол-во: {item.quantity}";
        desc += $"\nЦена продажи: {item.quantity * 20} монет";
        return desc;
    }
}