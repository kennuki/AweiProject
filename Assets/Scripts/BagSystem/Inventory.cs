using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory
{
    public event EventHandler OnItemListChanged;

    private List<Item> itemlist;
    public Inventory()
    {
        itemlist = new List<Item>();
    }
    public void AddItem(Item item)
    {
        if (item.IsStackable())
        {
            bool ItemAlreadyInInventory = false;
            foreach(Item InventoryItem in itemlist)
            {
                if(InventoryItem.itemType == item.itemType)
                {
                    InventoryItem.amount += item.amount;
                    ItemAlreadyInInventory = true;
                }
            }
            if (!ItemAlreadyInInventory)
            {
                itemlist.Add(item);
            }
        }
        else
        {
            itemlist.Add(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }
    public void UseItem(Item item)
    {
        if (item.IsStackable())
        {
            Item ItemInInventory = null;
            foreach (Item InventoryItem in itemlist)
            {
                if (InventoryItem.itemType == item.itemType)
                {
                    InventoryItem.amount -= item.amount;
                    ItemInInventory = InventoryItem;
                }
            }
            if (ItemInInventory!=null&&ItemInInventory.amount<0)
            {
                itemlist.Remove(item);
                
            }
        }
        else
        {
            itemlist.Remove(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }
    public void SetItemToIndex0(Item item,int OriginIndex)
    {
        itemlist.Remove(itemlist[OriginIndex]);
        itemlist.Insert(0, item);
    }


    public List<Item> GetItemList()
    {
        return itemlist;
    }
}
