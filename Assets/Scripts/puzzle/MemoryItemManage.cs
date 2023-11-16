using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MemoryItemManage : MonoBehaviour
{
    private Inventory inventory;
    public UI_Inventory ui_Inventory;
    public PlayerBag bag;
    public static bool TeddyMission = false;
    public static bool OldLetterMission = false;
    public static bool HariMission = false;
    public static bool InvitationMission = false;
    public static bool Candy1Mission = false;
    public static bool Candy3Mission = false;
    public static bool PeaCandyMission = false;
    public static bool HamburgerCandyMission = false;
    public Animator anim;
    private GameObject MemoryItem;
    private GameObject MemoryItemRightPlace;
    private TextMeshProUGUI MemoryObjectName;
    public TextMeshProUGUI MemoryAdvanced;
    //private TextMeshProUGUI MemoryAdvance;
    public Character character;
    private void Start()
    {
        MemoryObjectName = GetComponentInChildren<TextMeshProUGUI>();

    }
    public virtual void MemoryItemSetActive()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.name != "TargetItemName")
                transform.GetChild(i).gameObject.SetActive(false);
        }
        if (MemoryObjectName.text == "InvitationCard")
        {
            MemoryItem = ItemAsset.Instance.MemoryOldLetter3D;
            MemoryItemRightPlace = ItemAsset.Instance.MemoryOldLetterRightPlace;
        }
        if (MemoryObjectName.text == "Teddy")
        {
            MemoryItem = ItemAsset.Instance.MemoryTeddy3D;
            MemoryItemRightPlace = ItemAsset.Instance.MemoryTeddyRightPlace;
        }
        if (MemoryObjectName.text == "Hari")
        {
            MemoryItem = ItemAsset.Instance.MemoryHari3D;
            MemoryItemRightPlace = ItemAsset.Instance.MemoryHariRightPlace;
        }
        if (MemoryObjectName.text == "Invitation")
        {
            MemoryItem = ItemAsset.Instance.MemoryInvitation3D;
            MemoryItemRightPlace = ItemAsset.Instance.MemoryInvitationRightPlace;
        }
        if (MemoryObjectName.text == "Candy1")
        {
            MemoryItem = ItemAsset.Instance.MemoryCandy13D;
            MemoryItemRightPlace = ItemAsset.Instance.MemoryCandy1RightPlace;
        }
        if (MemoryObjectName.text == "Candy3")
        {
            MemoryItem = ItemAsset.Instance.MemoryCandy33D;
            MemoryItemRightPlace = ItemAsset.Instance.MemoryCandy3RightPlace;
        }
        if (MemoryObjectName.text == "PeaCandy")
        {
            MemoryItem = ItemAsset.Instance.MemoryPeaCandy3D;
            MemoryItemRightPlace = ItemAsset.Instance.MemoryPeaCandyRightPlace;
        }
        if (MemoryObjectName.text == "HamburgerCandy")
        {
            MemoryItem = ItemAsset.Instance.MemoryHamburgerCandy3D;
            MemoryItemRightPlace = ItemAsset.Instance.MemoryHamburgerCandyRightPlace;
        }
        else if(Character.OnMission == 1)
        {
            MemoryObjectName.text = "Teddy";
            MemoryItem = ItemAsset.Instance.MemoryTeddy3D;
            MemoryItemRightPlace = ItemAsset.Instance.MemoryTeddyRightPlace;
        }
        MemoryItem.SetActive(true);
        TakeItemState = 2;
    }
    public static int TakeItemState = 0;    //0 = idle    1 = gunmode   2 = memory item mode
    float t = 0;
    float MemoryDistance = 0;
    private ItemFollow follow;
    renderfade renderfade;
    public DoorRotate[] Door;
    public GameObject Scene2TriggerBox;
    Collider Scene2Cd;
    private void Update()
    {
        if(TakeItemState != 2)
        {
            t = 0;
           for(int i = 0; i < transform.childCount; i++)
            {
                if(transform.GetChild(i).gameObject.name != "TargetItemName")
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        else if(TakeItemState == 2)
        {
            if (Character.ActionProhibit==false&&Input.GetKeyDown(KeyCode.Mouse0))
            {
                MemoryObjectName.text = "";
            }
            MemoryDistance = Vector3.Distance(MemoryItem.transform.position, MemoryItemRightPlace.transform.position);
            t += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.F)&& anim.GetBool("Put")==false)
            {
                t = 0;
                anim.SetBool("Put", true);
                Character.ActionProhibit = true;
            }
            else if (MemoryDistance > 3)
            {
                if (t > 2)
                {
                    anim.SetBool("Put", false);
                }
                if (t > 2.2f)
                {
                    t = 0;
                    Character.ActionProhibit = false;
                }
            }
            else if(MemoryDistance<=3&& anim.GetBool("Put")==true )
            {
                if (t > 1.5f)
                {
                    if (MemoryObjectName.text == "InvitationCard")
                    {
                        KickUnlock();
                        OldLetterMission = true;
                        StartCoroutine(TimeDelayActionOn2(2));
                    }
                    else if (MemoryObjectName.text == "Teddy"|| Character.OnMission == 1)
                    {
                        DashUnlock();
                        TeddyMission = true;
                    }
                    else if (MemoryObjectName.text == "Hari")
                    {
                        character.KickAdvance = true;
                        HariMission = true;
                    }
                    else if (MemoryObjectName.text == "Invitation")
                    {                       
                        InvitationMission = true;
                    }
                    else if (MemoryObjectName.text == "Candy1")
                    {
                        Candy1Mission = true;
                    }
                    else if (MemoryObjectName.text == "Candy3")
                    {
                        Candy3Mission = true;
                    }
                    else if (MemoryObjectName.text == "PeaCandy")
                    {
                        PeaCandyMission = true;
                    }
                    else if (MemoryObjectName.text == "HamburgerCandy")
                    {
                        HamburgerCandyMission = true;
                    }
                    MemoryItem.transform.SetParent(MemoryItemRightPlace.transform.parent.transform);
                    follow = MemoryItem.GetComponent<ItemFollow>();
                    follow.enabled = true;
                } 
                if (t > 3)
                {
                    if (MemoryObjectName.text == "Candy3")
                    {
                        for(int i = 0; i < 3; i++)
                        {
                            renderfade = MemoryItem.GetComponentInChildren<renderfade>();
                            if (renderfade != null)
                                renderfade.enabled = true;
                            renderfade.gameObject.transform.SetAsLastSibling();
                        }

                    }
                    else
                    {
                        renderfade = MemoryItem.GetComponentInChildren<renderfade>();
                        if (renderfade != null)
                            renderfade.enabled = true;
                        renderfade = MemoryItem.GetComponent<renderfade>();
                        if (renderfade != null)
                            renderfade.enabled = true;
                    }
                    MemoryItemRightPlace.SetActive(false);
                    anim.SetBool("Put", false);
                    StartCoroutine(TimeDelayActionOn(0.2f));
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            MemoryAdvanced.text = "";
            MemoryObjectName.text = "";
            TakeItemState = 0;
            anim.SetBool("Put", false);
            Character.ActionProhibit = false;
            inventory = bag.inventory;
            ui_Inventory.SetInventory(inventory);
        }

    }
    private void KickUnlock()
    {
        character.skills[2].skillstate = 1;
        ItemAsset.Instance.KickMask1.SetActive(false);
        ItemAsset.Instance.KickMask2.SetActive(false);
        ItemAsset.Instance.KickIcon.SetActive(true);
    }
    private void DashUnlock()
    {
        character.skills[0].skillstate = 1;
        ItemAsset.Instance.DashMask1.SetActive(false);
        ItemAsset.Instance.DashMask2.SetActive(false);
        ItemAsset.Instance.DashIcon.SetActive(true);
    }
    private IEnumerator TimeDelayActionOn(float t)
    {
        yield return new WaitForSeconds(t);
        inventory = bag.inventory;
        ui_Inventory.SetInventory(inventory);
        Character.ActionProhibit = false;
        MemoryObjectName.text = "";
        yield break;
    }
    private IEnumerator TimeDelayActionOn2(float t)
    {
        yield return new WaitForSeconds(t);
        foreach (DoorRotate door in Door)
        {
            if(door!=null)
            door.enabled = true;
        }
        Scene2Cd = Scene2TriggerBox.GetComponent<Collider>();
        Scene2Cd.enabled = true;
        yield break;
    }
}
