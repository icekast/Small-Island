using UnityEngine;
using UnityEngine.UI; // ƒл€ стандартного Text

public class MoneyDisplay : MonoBehaviour
{
    [SerializeField] private Inventory inventory; // —сылка на Inventory
    [SerializeField] private Text moneyText;      // —сылка на стандартный UI Text

    private void Start()
    {
        if (inventory == null)
        {
            Debug.LogError("Inventory не назначен в MoneyDisplay!");
            return;
        }

        if (moneyText == null)
        {
            Debug.LogError("UI Text не назначен в MoneyDisplay!");
            return;
        }

        // ќбновл€ем текст при старте
        UpdateMoneyText();
    }

    public void UpdateMoneyText()
    {
        moneyText.text = $"Money: {inventory.money}"; // ‘ормат текста
    }
}
