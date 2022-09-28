using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vineglow : MonoBehaviour
{
    Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }
    float x = 1.8f;
    float i;
    float rate = 0.02f;
    // Update is called once per frame
    void Update()  
    {
        rend.material.SetFloat("_emission", x);
        i = (Mathf.Abs(Mathf.Abs(x - 1) - 0.1f)) * rate + 0.01f * rate;
        x += i;
        if (x < 1.5f)
        {
            rate = Random.Range(rate + 0.3f * rate, rate - 0.3f * rate);
            rate = Mathf.Clamp(rate, -1.5f, 1.5f);
            rate = 0.02f;
            
        }
        else if (x > 4f)
        {
            rate = -0.02f;
        }
    }
}
