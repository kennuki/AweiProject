using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform mask;
    private Transform ItemSlotContainer;
    private Transform ItemSlotTemplate;
    private RectTransform ItemSlotContainerRect;
    private void Start()
    {
        mask = transform.Find("mask");
        ItemSlotContainer = mask.Find("ItemSlotContainer");
        ItemSlotTemplate = ItemSlotContainer.Find("ItemSlotTemplate");
        ItemSlotContainerRect = ItemSlotContainer.GetComponent<RectTransform>();
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
        float ItemSlotCellSize = 150;
        Vector2 Offset = new Vector2(-1325, 400);
        Vector2 ContainerScale = new Vector2(612.4f, -197.818181f);
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
            ItemSlotContainerRect.sizeDelta = ContainerScale + new Vector2(0, 151.272727f);
            ContainerScale = ItemSlotContainerRect.sizeDelta;
            RectTransform ItemSlotRectTransform = Instantiate(ItemSlotTemplate, ItemSlotContainer).GetComponent<RectTransform>();
            ItemSlotRectTransform.gameObject.SetActive(true);
            ItemSlotRectTransform.anchoredPosition = new Vector2(x * ItemSlotCellSize, y * ItemSlotCellSize) + Offset;
            Image image = ItemSlotRectTransform.Find("Image").GetComponent<Image>();
            Image image2 = ItemSlotRectTransform.Find("ImageKumo").GetComponent<Image>();
            image.sprite = item.GetSprite();
            TextMeshProUGUI uitext = ItemSlotRectTransform.Find("Count").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI CountChinese = ItemSlotRectTransform.Find("CountChinese").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI ObjectName = ItemSlotRectTransform.Find("ObjectName").GetComponent<TextMeshProUGUI>();
            ObjectName.text = item.GetName();
            ObjectName.color = item.GetNameColor();
            if (item.amount > 0)
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
            if (x > 0)
            {
                x = 0;
                y--;
            }

        }
    }

}
