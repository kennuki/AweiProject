using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform ItemSlotContainer;
    private Transform ItemSlotTemplate;
    private void Start()
    {
        ItemSlotContainer = transform.Find("ItemSlotContainer");
        ItemSlotTemplate = ItemSlotContainer.Find("ItemSlotTemplate");
    }


    public void SetInventory(Inventory inventory)
    {

        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItem();
    }

    private void Inventory_OnItemListChanged(object sender,System.EventArgs e)
    {
        RefreshInventoryItem();
    }

    public TextMeshProUGUI HPtext;
    public TextMeshProUGUI MPtext;
    private void RefreshInventoryItem()
    {
        foreach (Transform child in ItemSlotContainer)
        {
            if (child.name == "ItemSlotTemplate") continue;
            Destroy(child.gameObject);
        }
        int x = 0;
        int y = 0;
        float ItemSlotCellSize = 130;
        Vector2 Offset = new Vector2(8, -5);
        foreach(Item item in inventory.GetItemList())
        {
            if (item.amount <= 0) 
            {
                item.amount = 0;
                if (item.itemType == Item.ItemType.HealthPotion)
                {
                    HPtext.SetText(item.amount.ToString());
                }
                if (item.itemType == Item.ItemType.ManaPotion)
                {
                    MPtext.SetText(item.amount.ToString());
                }
                continue;
            } 
            RectTransform ItemSlotRectTransform = Instantiate(ItemSlotTemplate, ItemSlotContainer).GetComponent<RectTransform>();
            ItemSlotRectTransform.gameObject.SetActive(true);
            ItemSlotRectTransform.anchoredPosition = new Vector2(x * ItemSlotCellSize, y * ItemSlotCellSize) + Offset;
            Image image = ItemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();

            TextMeshProUGUI uitext = ItemSlotRectTransform.Find("Text").GetComponent<TextMeshProUGUI>();
            if (item.amount > 1)
            {
                uitext.SetText(item.amount.ToString());
            }
            else
            {
                uitext.SetText("");
            }
            if (item.itemType == Item.ItemType.HealthPotion)
            {
                HPtext.SetText(item.amount.ToString());
            }
            if (item.itemType == Item.ItemType.ManaPotion)
            {
                MPtext.SetText(item.amount.ToString());
            }



            x++;
            if (x > 4)
            {
                x = 0;
                y--;
            }

        }
    }

}
