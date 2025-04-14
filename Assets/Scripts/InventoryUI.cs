using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    public GameObject slotPrefab; // Префаб должен содержать: Image (иконка), Text (название и количество)
    public Transform slotParent;
    public Inventory inventory;
    public Color selectionColor = new Color(1, 0.8f, 0, 0.3f); // Полупрозрачный оранжевый

    private List<GameObject> slotInstances = new List<GameObject>();
    private int selectedSlotIndex = -1;

    void OnEnable()
    {
        inventory.onInventoryChanged.AddListener(RefreshUI);
        RefreshUI();
    }

    void OnDisable()
    {
        inventory.onInventoryChanged.RemoveListener(RefreshUI);
    }

    public void RefreshUI()
    {
        // Очистка старых слотов
        foreach (var slot in slotInstances)
        {
            Destroy(slot);
        }
        slotInstances.Clear();

        // Создание новых слотов
        var items = inventory.GetAllItems();
        for (int i = 0; i < items.Count; i++)
        {
            GameObject slotGO = Instantiate(slotPrefab, slotParent);
            InventoryItem item = items[i];

            // Находим компоненты
            Image iconImage = slotGO.transform.Find("Icon")?.GetComponent<Image>();
            Text itemText = slotGO.GetComponentInChildren<Text>(); // Основной текст слота
            Image background = slotGO.GetComponent<Image>(); // Фон для выделения

            // Заполняем данные
            if (iconImage != null)
            {
                iconImage.sprite = item.icon;
                iconImage.color = item.icon != null ? Color.white : Color.clear;
            }

            if (itemText != null)
            {
                itemText.text = item.IsStackable ?
                    $"{item.displayName} ({item.quantity})" :
                    item.displayName;
            }

            // Выделение
            if (background != null)
            {
                background.color = (i == selectedSlotIndex) ? selectionColor : Color.white;
            }

            // Настройка кнопки
            Button button = slotGO.GetComponent<Button>();
            if (button != null)
            {
                int index = i;
                button.onClick.AddListener(() => SelectSlot(index));
            }

            slotInstances.Add(slotGO);
        }
    }

    private void SelectSlot(int index)
    {
        // Снимаем выделение
        if (selectedSlotIndex >= 0 && selectedSlotIndex < slotInstances.Count)
        {
            var oldBg = slotInstances[selectedSlotIndex].GetComponent<Image>();
            if (oldBg != null) oldBg.color = Color.white;
        }

        // Устанавливаем новое выделение
        selectedSlotIndex = index;
        var newBg = slotInstances[index].GetComponent<Image>();
        if (newBg != null) newBg.color = selectionColor;

        // Выбираем предмет
        var items = inventory.GetAllItems();
        if (index < items.Count)
        {
            inventory.SetSelectedItem(items[index]);
        }
    }
}