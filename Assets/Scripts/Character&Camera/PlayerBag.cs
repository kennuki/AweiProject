using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBag : MonoBehaviour
{
    public Animator anim;
    [SerializeField] private UI_Inventory uI_Inventory;
    private Inventory inventory;
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
        if(itemWorld != null)
        {
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }
    Item item;
    public CharacterAbility CA;
    public void OnClickHPPotion()
    {
        foreach (Item item in inventory.GetItemList())
        {
            if (item.itemType == Item.ItemType.HealthPotion && item.amount > 0)
            {
                inventory.UseItem(new Item { itemType = Item.ItemType.HealthPotion, amount = 1 });
                CA.HP += 10;
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
                CA.HP += 10;
            }
        }
    }
    public void OnClickSkill1()
    {
        foreach (Item item in inventory.GetItemList())
        {
            if (item.itemType == Item.ItemType.Crystal && item.amount >= 15)
            {
                inventory.UseItem(new Item { itemType = Item.ItemType.Crystal, amount = 15 });
                CA.AS = 2;
                anim.SetFloat("AS", 2);
            }
        }

    }
    private void UsePotion()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            foreach (Item item in inventory.GetItemList())
            {
                if (item.itemType == Item.ItemType.HealthPotion && item.amount > 0)
                {
                    inventory.UseItem(new Item { itemType = Item.ItemType.HealthPotion, amount = 1 });
                    CA.HP += 10;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            foreach (Item item in inventory.GetItemList())
            {
                if (item.itemType == Item.ItemType.ManaPotion && item.amount > 0)
                {
                    inventory.UseItem(new Item { itemType = Item.ItemType.ManaPotion, amount = 1 });
                    CA.HP += 10;
                }
            }
        }
    }
    private IEnumerator TDelay()
    {
        yield return new WaitForSeconds(0.1f);
        inventory = new Inventory();
        uI_Inventory.SetInventory(inventory);
    }
}
