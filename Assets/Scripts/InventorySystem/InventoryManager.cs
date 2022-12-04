using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();

    public Transform ItemContent;
    public GameObject InventoryItem;
    public Toggle EnableRemove;

    public InventoryItemController[] InventoryItems;

    private void Awake()
    {
        Instance = this;
    }

    public void Add(Item item)
    {
        Items.Add(item);
    }

    public void Remove(Item item)
    {
        Items.Remove(item);
    }

    public void ListItem()
    {
        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TMP_Text>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var removeButton = obj.transform.Find("RemoveButton").GetComponent<Button>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;

            if (EnableRemove.isOn)
            {
                removeButton.gameObject.SetActive(true);
            }

        }
        SetInventoryItems();

    }

    public void CleanList()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
    }

    public void EnableItemsRemove()
    {
        if (EnableRemove.isOn)
        {
            foreach (Transform item in ItemContent)
            {
                var itemType = item.GetComponent<InventoryItemController>().item.itemType;

                if (itemType == Item.ItemType.Potion || itemType == Item.ItemType.Food)
                    item.Find("RemoveButton").gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (Transform item in ItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(false);
            }
        }
    }

    public void SetInventoryItems()
    {
        InventoryItems = new InventoryItemController[Items.Count];
        InventoryItems = ItemContent.GetComponentsInChildren<InventoryItemController>();

        for (int i = 0; i < InventoryItems.Length; i++)
        {
            InventoryItems[i].AddItem(Items[i]);
        }
    }
}
