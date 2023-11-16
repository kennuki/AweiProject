using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog2 : Dialog
{
    private GameObject canvasdialog;
    private void Awake()
    {
        canvasdialog = GameObject.Find("CanvasDialog");
        DialogImage = canvasdialog.transform.Find("Dialog1").gameObject;
        Dialog_T = DialogImage.transform.GetComponentInChildren<TextMeshProUGUI>();
    }

}
