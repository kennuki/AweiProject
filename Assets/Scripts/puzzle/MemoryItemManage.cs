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
    public Animator anim;
    private GameObject MemoryItem;
    private GameObject MemoryItemRightPlace;
    private TextMeshProUGUI MemoryObjectName;
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
        else if(Character.OnMission == 1)
        {
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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            MemoryObjectName.text = "";
        }
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
                Debug.Log("");
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
                        OldLetterMission = true;
                    }
                    else if (MemoryObjectName.text == "Teddy"|| Character.OnMission == 1)
                    {
                        TeddyMission = true;
                    }
                    MemoryItem.transform.SetParent(MemoryItemRightPlace.transform.parent.transform);
                    follow = MemoryItem.GetComponent<ItemFollow>();
                    follow.enabled = true;
                } 
                if (t > 3)
                {

                    renderfade = MemoryItem.GetComponentInChildren<renderfade>();
                    if(renderfade != null)
                    {
                        renderfade.enabled = true;
                    }
                    MemoryItemRightPlace.SetActive(false);
                    anim.SetBool("Put", false);
                    StartCoroutine(TimeDelayActionOn(0.2f));
                }
            }
        }
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
}
