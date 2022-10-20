using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagShine : MonoBehaviour
{
    public Image bag;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bag.color=new Color(0.88f + Mathf.Sin(Time.time * 270 * Mathf.Deg2Rad) * 0.12f, 0.88f + Mathf.Sin(Time.time * 270 * Mathf.Deg2Rad) * 0.12f, 0.88f + Mathf.Sin(Time.time * 270 * Mathf.Deg2Rad) * 0.12f, 1);
    }
}
