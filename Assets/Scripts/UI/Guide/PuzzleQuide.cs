using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PuzzleQuide : MonoBehaviour
{
    private Inventory inventory;
    public UI_Inventory ui_Inventory;
    public PlayerBag bag;
    public GameObject BlackPanel;
    public GameObject BagIcon;
    public Transform ItemSlotContainer;
    private Transform ItemSlotTemplate;
    public GameObject ItemInfo;
    public GameObject Button;
    public GameObject Description;
    public GameObject[] nexttrigger;
    public GameObject CanvaDialog;
    private TextMeshProUGUI text1, text2;
    public CameraRotation cameraRotation;
    void Start()
    {
        inventory = bag.inventory;
        bag.GetTargetItemOnTop(Item.ItemType.Teddy);
        ui_Inventory.SetInventory(inventory);
        StartCoroutine(PuzzleGuideFunction());
    }

    void Update()
    {
    }
    int step = 0;
    GameObject icon;
    GameObject dialog;
    public Scrollbar scrollbar;
    private IEnumerator PuzzleGuideFunction()
    {
        Character.OnMission = 1;
        step = 0;

        while (step == 0)
        {
            if (MemoryItemManage.TeddyMission == true)
            {
                step = 6;
                Character.ActionProhibit = true;
            }
            else
            {
                cameraRotation.HideBag();
                scrollbar.value = 1;
                Time.timeScale = 0f;
                BlackPanel.SetActive(true);
                Character.ActionProhibit = true;
                icon = Instantiate(BagIcon, BlackPanel.transform, true);
                dialog = Instantiate(CanvaDialog, BlackPanel.transform, true);
                dialog.SetActive(true);
                text1 = dialog.GetComponentInChildren<TextMeshProUGUI>();
                text1.color = new Vector4(0.73f, 0.74f, 0.96f, 1);
                text1.text = "按B點開背包後並點擊\"拿取\"鈕!";
                step = 1;
            }
            while (step == 1)
            {
                if (Input.GetKey(KeyCode.B))
                {
                    Destroy(icon);
                    Destroy(dialog);
                    ItemSlotTemplate = ItemSlotContainer.Find("ItemSlotTemplate(Clone)");
                    if (ItemSlotTemplate != null)
                    {
                        Instantiate(ItemSlotTemplate, BlackPanel.transform, true);
                    }
                    step = 2;
                }
                yield return new WaitForEndOfFrame();
            }
            while (step == 2)
            {
                yield return new WaitForEndOfFrame();
            }
            while (step == 3)
            {
                Instantiate(ItemInfo, BlackPanel.transform, true);
                Instantiate(Button, BlackPanel.transform, true);
                Description.SetActive(false);
                step = 4;
            }
            while (step <= 4)
            {
                Character.ActionProhibit = true;
                yield return new WaitForEndOfFrame();
            }
            while (step <= 5)
            {
                foreach (Transform child in BlackPanel.transform)
                {
                    Destroy(child.gameObject);
                }
                Time.timeScale = 1;
                Description.SetActive(true);
                GameObject dialog2 = Instantiate(CanvaDialog, BlackPanel.transform, true);
                dialog2.SetActive(true);
                text2 = dialog2.GetComponentInChildren<TextMeshProUGUI>();
                text2.color = new Vector4(0.73f, 0.74f, 0.96f, 1);
                text2.text = "按住\"F\"將回憶道具歸位!";
                Character.ActionProhibit = true;
                Character.ActionProhibitWithoutMove = true;
                if (Input.GetKey(KeyCode.F))
                {
                    foreach (Transform child in BlackPanel.transform)
                    {
                        Destroy(child.gameObject);
                    }
                    Destroy(dialog2);
                    CanvaDialog.SetActive(false);
                    BlackPanel.SetActive(false);
                    step = 6;
                    Character.ActionProhibitWithoutMove = false;
                    Character.ActionProhibit = true;
                }
                yield return new WaitForEndOfFrame();
            }
            while(step == 6)
            {
                if (MemoryItemManage.TeddyMission == true)
                {
                    step = 7;
                }
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(2f);
            Description.SetActive(true);
            foreach (GameObject trigger in nexttrigger)
            {
                trigger.SetActive(true);
            }
            Character.OnMission = 0;
        }
    }

    public void OnImageEnter()
    {
        step = 3;
    }
    public void OnTakeButtonHit()
    {
        step = 5;
    }

}
