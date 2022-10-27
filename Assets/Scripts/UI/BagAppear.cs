using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagAppear : MonoBehaviour
{
    RectTransform recttransform;
    private void Start()
    {
        recttransform = this.GetComponent<RectTransform>();
        recttransform.anchoredPosition = new Vector2(-998, -213);
    }
    void Update()
    {
        
    }
    public void OnBagIconHit()
    {
        if(recttransform.anchoredPosition.x== -998)
        {
            Debug.Log("");
            recttransform.anchoredPosition = new Vector2(922, -213);
        }
        else
        {
            recttransform.anchoredPosition = new Vector2(-998, -213);
        }
    }
}
