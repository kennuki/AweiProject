using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagAppear : MonoBehaviour
{
    RectTransform recttransform;
    void Update()
    {
        recttransform = this.GetComponent<RectTransform>();
    }
    public void OnBagIconHit()
    {
        if(recttransform.anchoredPosition.x==0)
        {
            recttransform.anchoredPosition = new Vector2(960, -540);
        }
        else
        {
            recttransform.anchoredPosition = new Vector2(0, -540);
        }
    }
}
