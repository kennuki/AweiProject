using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemInfo : MonoBehaviour
{
    public TextMeshProUGUI ItemDescription;
    public TextMeshProUGUI ItemAdvanceDescription;
    public TextMeshProUGUI TakedObjectName;
    public GameObject TakeButton;
    bool takebutton_;
    public void OnMouseEnter()
    {

        TextMeshProUGUI Info = transform.Find("ObjectInfo").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI InfoAdvance = transform.Find("ObjectAdvanceInfo").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI takebutton = transform.Find("ObjectUse").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI takeobjectname = transform.Find("3DObjectName").GetComponent<TextMeshProUGUI>();
        ItemDescription.text = Info.text;
        ItemAdvanceDescription.text = InfoAdvance.text;
        TakedObjectName.text = takeobjectname.text;
        if (takebutton.text == "false")
        {
            takebutton_ = false;
        }
        else
        {
            takebutton_ = true;
        }
        TakeButton.SetActive(takebutton_);
    }
}
