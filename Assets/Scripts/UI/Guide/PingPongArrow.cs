using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongArrow : MonoBehaviour
{
    public float SwitchSpeed;
    public float Range;
    RectTransform rect;
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }
    
    void Update()
    {
        transform.localPosition = new Vector3(rect.anchoredPosition.x, rect.anchoredPosition.y + Mathf.PingPong(Time.unscaledTime * SwitchSpeed, Range) - Range / 2, 0);
    }

}
