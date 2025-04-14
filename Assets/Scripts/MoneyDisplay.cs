using UnityEngine;
using UnityEngine.UI; // ��� ������������ Text

public class MoneyDisplay : MonoBehaviour
{
    [SerializeField] private Inventory inventory; // ������ �� Inventory
    [SerializeField] private Text moneyText;      // ������ �� ����������� UI Text

    private void Start()
    {
        if (inventory == null)
        {
            Debug.LogError("Inventory �� �������� � MoneyDisplay!");
            return;
        }

        if (moneyText == null)
        {
            Debug.LogError("UI Text �� �������� � MoneyDisplay!");
            return;
        }

        // ��������� ����� ��� ������
        UpdateMoneyText();
    }

    public void UpdateMoneyText()
    {
        moneyText.text = $"Money: {inventory.money}"; // ������ ������
    }
}
