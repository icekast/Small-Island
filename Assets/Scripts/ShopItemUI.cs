using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    // Ссылки на UI-компоненты
    [Header("Компоненты")]
    [SerializeField] private Image iconImage;    // Иконка предмета
    [SerializeField] private Text nameText;     // Название
    [SerializeField] private Text priceText;    // Цена
    [SerializeField] private Button buyButton;  // Кнопка покупки

    // Настройка карточки товара
    public void Setup(ItemsDatabase.ItemData itemData, System.Action onPurchaseCallback)
    {
        // Заполняем данные
        iconImage.sprite = itemData.icon;
        nameText.text = itemData.displayName;
        priceText.text = $"{itemData.cost} монет";

        // Назначаем действие при нажатии на кнопку
        buyButton.onClick.RemoveAllListeners(); // Очищаем старые подписки
        buyButton.onClick.AddListener(() => onPurchaseCallback?.Invoke());

        // Можно добавить дополнительные проверки:
        // - Достаточно ли денег (подсветить кнопку)
        // - Есть ли предмет в инвентаре
    }
}
