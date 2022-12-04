using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item Item;

    public void Pickup(float destroyDuration = 0)
    {
        InventoryManager.Instance.Add(Item);
        Destroy(gameObject, destroyDuration);
    }

}
