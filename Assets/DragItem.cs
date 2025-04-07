using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public InventoryItem item;
    public Inventory inventory;
    public InventoryUI ui;

    private Transform originalParent;
    private Image icon;

    public void Setup(InventoryItem item, Inventory inventory, InventoryUI ui)
    {
        this.item = item;
        this.inventory = inventory;
        this.ui = ui;
        icon = GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        transform.SetParent(transform.root);
        icon.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(originalParent);
        transform.localPosition = Vector3.zero;
        icon.raycastTarget = true;
    }

    public void OnDrop(PointerEventData eventData)
    {
        DragItem other = eventData.pointerDrag?.GetComponent<DragItem>();
        if (other != null && other.item.itemName != item.itemName)
        {
            var temp = item;
            item = other.item;
            other.item = temp;

            ui.RefreshUI();
        }
    }
}