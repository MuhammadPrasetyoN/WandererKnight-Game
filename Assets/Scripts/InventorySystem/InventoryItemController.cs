using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    public Item item;

    public Button RemoveButton;

    public void RemoveItem()
    {
        if (item.itemType == Item.ItemType.Potion || item.itemType == Item.ItemType.Food)
        {
            InventoryManager.Instance.Remove(item);

            Destroy(gameObject);
        }
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
    }

    public void UseItem()
    {
        switch (item.itemType)
        {
            case Item.ItemType.Potion:
                PlayerStat.Instance.ConsumePotion(item.value);
                RemoveItem();
                break;
        }

    }
}
