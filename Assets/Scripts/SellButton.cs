using UnityEngine;

public class SellButton : MonoBehaviour
{
    public ShopManager shopManager;
    public Inventory inventory;

    public void OnClick()
    {
        shopManager.SellAllCrops(inventory);
    }
}