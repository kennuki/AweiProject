using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBag : MonoBehaviour
{
    public Animator anim;
    [SerializeField] private UI_Inventory uI_Inventory;
    public Inventory inventory;
    public Character character;
    public CharacterAbility characterAbility;
    private float HPReduce=0;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(TDelay());
       // ItemWorld.SpawnItemWorld(new Vector3(0,0,0), new Item { itemType = Item.ItemType.HealthPotion, amount = 1 });
    }
    private AudioSource audioSource;
    public AudioClip drink;
    // Update is called once per frame
    void Update()
    {
        if (HPReduce >= 0)
        {
            HPReduce -= Time.deltaTime;
        }
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
                CA.HP += CA.MaxHP * (0.3f-HPReduce*0.01f);
                HPReduce = 15;
                audioSource.PlayOneShot(drink,0.15f);
                uI_Inventory.RefreshHPMP();
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
                audioSource.PlayOneShot(drink, 0.15f);
                uI_Inventory.RefreshHPMP();
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
                    ItemAsset.Instance.SpeedMask1.SetActive(false);
                    ItemAsset.Instance.SpeedMask2.SetActive(false);
                    ItemAsset.Instance.UpgradeAccelerate.SetActive(false);
                    ItemAsset.Instance.AccelerateIcon.SetActive(true);
                    character.skills[1].skillstate = 1;
                    inventory.UseItem(new Item { itemType = Item.ItemType.Crystal, amount = 15 });
                    uI_Inventory.RefreshInventoryItem();
                }
            }

        }

    }
    public void OnClickSkill2()
    {
        
        if (character.skills[3].skillstate == 0)
        {
            foreach (Item item in inventory.GetItemList())
            {
                if (item.itemType == Item.ItemType.Crystal && item.amount >= 40)
                {
                    ItemAsset.Instance.ShootSkill1Mask1.SetActive(false);
                    ItemAsset.Instance.ShootSkill1Mask2.SetActive(false);
                    ItemAsset.Instance.UpgradeSkill1.SetActive(false);
                    ItemAsset.Instance.Skill1Icon.SetActive(true);
                    character.skills[3].skillstate = 1;
                    inventory.UseItem(new Item { itemType = Item.ItemType.Crystal, amount = 40 });
                    uI_Inventory.RefreshInventoryItem();
                }
            }

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
                    CA.HP += CA.MaxHP * (0.3f - HPReduce * 0.01f);
                    HPReduce = 15;
                    audioSource.PlayOneShot(drink, 0.15f);
                    uI_Inventory.RefreshHPMP();
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
                    audioSource.PlayOneShot(drink, 0.15f);
                    uI_Inventory.RefreshHPMP();
                }
            }
        }
    }
    public void OnADButtonHit()
    {
        foreach (Item item in inventory.GetItemList())
        {
            if (item.itemType == Item.ItemType.Crystal && item.amount >= 10)
            {

                inventory.UseItem(new Item { itemType = Item.ItemType.Crystal, amount = 10 });
                characterAbility.AD += 1;
                uI_Inventory.RefreshInventoryItem();
            }
        }
    }
    public void OnHPButtonHit()
    {

        foreach (Item item in inventory.GetItemList())
        {
            if (item.itemType == Item.ItemType.Crystal && item.amount >= 10)
            {

                inventory.UseItem(new Item { itemType = Item.ItemType.Crystal, amount = 10 });
                characterAbility.HP += 20;
                characterAbility.MaxHP += 20;
                uI_Inventory.RefreshInventoryItem();
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
