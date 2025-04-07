using UnityEngine;
using UnityEngine.EventSystems;

public class ItemTooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public InventoryItem item;
    private TooltipUI tooltipUI;

    void Start()
    {
        tooltipUI = FindObjectOfType<TooltipUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltipUI.ShowTooltip(item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltipUI.HideTooltip();
    }

    public void Setup(InventoryItem item)
    {
        this.item = item;
    }
}