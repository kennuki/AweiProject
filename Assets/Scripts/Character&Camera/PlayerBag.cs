using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBag : MonoBehaviour
{
    public Animator anim;
    [SerializeField] private UI_Inventory uI_Inventory;
    public Inventory inventory;
    public Character character;
    private void Awake()
    {
        StartCoroutine(TDelay());
       // ItemWorld.SpawnItemWorld(new Vector3(0,0,0), new Item { itemType = Item.ItemType.HealthPotion, amount = 1 });
    }

    // Update is called once per frame
    void Update()
    {
        UsePotion();
        if (Input.GetKeyDown(KeyCode.P))
        {
            foreach(Item item in inventory.GetItemList())
            {
                Debug.Log(item.amount);
            }
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        ItemWorld itemWorld = collider.GetComponentInParent<ItemWorld>();
        if (itemWorld != null)
        {
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }
    public CharacterAbility CA;
    public void OnClickHPPotion()
    {
        foreach (Item item in inventory.GetItemList())
        {
            if (item.itemType == Item.ItemType.HealthPotion && item.amount > 0)
            {
                inventory.UseItem(new Item { itemType = Item.ItemType.HealthPotion, amount = 1 });
                CA.HP += CA.MaxHP * 0.3f;
            }               
        }

    }
    public void OnClickMPPotion()
    {
        foreach (Item item in inventory.GetItemList())
        {
            if (item.itemType == Item.ItemType.ManaPotion && item.amount > 0)
            {
                inventory.UseItem(new Item { itemType = Item.ItemType.ManaPotion, amount = 1 });
                CA.MP += CA.MaxMP * 0.3f;
            }
        }
    }
    public void OnClickSkill1()
    {

        if (character.skills[1].skillstate == 0)
        {
            foreach (Item item in inventory.GetItemList())
            {
                if (item.itemType == Item.ItemType.Crystal && item.amount >= 15)
                {
                    inventory.UseItem(new Item { itemType = Item.ItemType.Crystal, amount = 15 });
                }
            }
            ItemAsset.Instance.SpeedMask1.SetActive(false);
            ItemAsset.Instance.SpeedMask2.SetActive(false);
            ItemAsset.Instance.UpgradeAccelerate.SetActive(false);
            ItemAsset.Instance.AccelerateIcon.SetActive(true);
            character.skills[1].skillstate = 1;
        }

    }
    private void UsePotion()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            foreach (Item item in inventory.GetItemList())
            {
                if (item.itemType == Item.ItemType.HealthPotion && item.amount > 0)
                {
                    inventory.UseItem(new Item { itemType = Item.ItemType.HealthPotion, amount = 1 });
                    CA.HP += CA.MaxHP * 0.3f;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            foreach (Item item in inventory.GetItemList())
            {
                if (item.itemType == Item.ItemType.ManaPotion && item.amount > 0)
                {
                    inventory.UseItem(new Item { itemType = Item.ItemType.ManaPotion, amount = 1 });
                    CA.MP += CA.MaxMP * 0.3f;
                }
            }
        }
    }
    int targetindex;
    Item targetitem;
    public void GetTargetItemOnTop(Item.ItemType type)
    {
        int listcount = inventory.GetItemList().Count;
        foreach (Item item in inventory.GetItemList())
        {
            if (item.itemType == type)
            {
                targetindex = inventory.GetItemList().IndexOf(item);
                targetitem = item;
            }
        }
        if (listcount != 0 && targetitem != null)
            inventory.SetItemToIndex0(targetitem, targetindex);
    }
    private IEnumerator TDelay()
    {
        yield return new WaitForSeconds(0.1f);
        inventory = new Inventory();
        uI_Inventory.SetInventory(inventory);
    }
}
