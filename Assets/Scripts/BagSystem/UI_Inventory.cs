using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UI_Inventory : MonoBehaviour
{

    private Inventory inventory;
    private Transform mask;
    private Transform ItemSlotContainer;
    private Transform ItemSlotTemplate;
    private RectTransform ItemSlotContainerRect;
    private Image image;
    private TextMeshProUGUI uitext;
    private TextMeshProUGUI ObjectName;
    private TextMeshProUGUI ObjectInfo;
    private TextMeshProUGUI ObjectAdvanceInfo;
    private TextMeshProUGUI TakeButton;
    private TextMeshProUGUI MemoryObject3DName;
    private void Start()
    {
        mask = transform.Find("mask");
        ItemSlotContainer = mask.Find("ItemSlotContainer");
        ItemSlotTemplate = ItemSlotContainer.Find("ItemSlotTemplate");
        ItemSlotContainerRect = ItemSlotContainer.GetComponent<RectTransform>();
        image = ItemSlotTemplate.Find("Image").GetComponent<Image>();
        uitext = ItemSlotTemplate.Find("Count").GetComponent<TextMeshProUGUI>();
        ObjectName = ItemSlotTemplate.Find("ObjectName").GetComponent<TextMeshProUGUI>();
        ObjectInfo = image.GetComponentInChildren<TextMeshProUGUI>();
        ObjectAdvanceInfo = image.transform.Find("ObjectAdvanceInfo").GetComponent<TextMeshProUGUI>();
        TakeButton = image.transform.Find("ObjectUse").GetComponent<TextMeshProUGUI>();
        MemoryObject3DName = image.transform.Find("3DObjectName").GetComponent<TextMeshProUGUI>();
    }
    private int ImaHPPotion = 0;
    private int ImaMPPotion = 0;
    private int ImaCrystal = 0;
    private void Update()
    {

    }
    public void OnSceneChange()
    {
        foreach (Item item in inventory.GetItemList())
        {
            if (item.itemType == Item.ItemType.HealthPotion)
            {
                ImaHPPotion = item.amount;
            }
            if (item.itemType == Item.ItemType.ManaPotion)
            {
                ImaMPPotion = item.amount;
            }
            if (item.itemType == Item.ItemType.Crystal)
            {
                ImaCrystal = item.amount;
            }
        }
        RefreshInventoryItem();
    }
    public void OnSceneReloaded()
    {
        foreach (Item item in inventory.GetItemList())
        {
            if (item.itemType == Item.ItemType.HealthPotion)
            {
                item.amount = ImaHPPotion;
            }
            if (item.itemType == Item.ItemType.ManaPotion)
            {
                item.amount = ImaMPPotion;
            }
            if (item.itemType == Item.ItemType.Crystal)
            {
                item.amount = ImaCrystal;
            }
            RefreshInventoryItem();
        }
    }
    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItem();
    }

    private void Inventory_OnItemListChanged(object sender,System.EventArgs e)
    {
        RefreshHPMP();
    }

    public TextMeshProUGUI HPtext;
    public TextMeshProUGUI MPtext;
    public void RefreshInventoryItem()
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
            image.sprite = item.GetSprite();
            ObjectName.text = item.GetName();
            ObjectName.color = item.GetNameColor();
            ObjectInfo.text = item.GetInfo();
            MemoryObject3DName.text = item.Get3DMemoryObjectName();
            if (item.MemoryItem() == true)
            {
                if (item.GetMissionComplete() == true)
                {
                    ObjectAdvanceInfo.text = item.GetAdvamceInfo();
                    TakeButton.text = "false";
                }
                else
                {
                    ObjectAdvanceInfo.text = "";
                    TakeButton.text = "true";
                }
            }
            else
            {
                ObjectAdvanceInfo.text = item.GetAdvamceInfo();
                TakeButton.text = "false";
            }
            if (item.amount > 0)
            {
                uitext.SetText(item.amount.ToString());
            }
            else
            {
                uitext.SetText("");
            }
            RectTransform ItemSlotRectTransform = Instantiate(ItemSlotTemplate, ItemSlotContainer).GetComponent<RectTransform>();
            ItemSlotRectTransform.gameObject.SetActive(true);
            ItemSlotRectTransform.anchoredPosition = new Vector2(x * ItemSlotCellSize, y * ItemSlotCellSize) + Offset;
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
    public void RefreshHPMP()
    {
        foreach (Item item in inventory.GetItemList())
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
            }
            else
            {
                if (item.itemType == Item.ItemType.HealthPotion)
                {
                    HPtext.SetText(item.amount.ToString());
                }
                if (item.itemType == Item.ItemType.ManaPotion)
                {
                    MPtext.SetText(item.amount.ToString());
                }
            }

        }
    }
}
