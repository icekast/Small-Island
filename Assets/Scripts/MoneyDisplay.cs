using UnityEngine;
using UnityEngine.UI;

public class MoneyDisplay : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private Text moneyText;

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

        UpdateMoneyText();
    }

    public void UpdateMoneyText()
    {
        moneyText.text = $"Money: {inventory.money}";
    }
}
