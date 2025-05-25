using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ItemPickup2D : MonoBehaviour
{
    public string itemID = "tomato_seed";
    public int amount = 1;

    private void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Inventory inventory = FindObjectOfType<Inventory>();
        if (inventory != null)
        {
            if (inventory.AddItem(itemID, amount))
            {
                Destroy(gameObject); 
            }
        }
    }
}