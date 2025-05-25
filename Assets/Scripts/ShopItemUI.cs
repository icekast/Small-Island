using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    [Header("Компоненты")]
    [SerializeField] private Image iconImage;
    [SerializeField] private Text nameText;
    [SerializeField] private Text priceText;
    [SerializeField] private Button buyButton;

    public void Setup(ItemsDatabase.ItemData itemData, System.Action onPurchaseCallback)
    {
        iconImage.sprite = itemData.icon;
        nameText.text = itemData.displayName;
        priceText.text = $"{itemData.cost}";

        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(() => onPurchaseCallback?.Invoke());
    }
}
