using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ItemsDatabase", menuName = "Inventory/Items Database")]
public class ItemsDatabase : ScriptableObject
{
    public List<InventoryItem> allGameItems = new List<InventoryItem>();
}