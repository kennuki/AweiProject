using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glow : MonoBehaviour
{
    Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }
    float x = 1.6f;
    float i;
    float rate = 0.005f;
    // Update is called once per frame
    void Update()
    {
        rend.material.SetColor("_EmissionColor", new Vector4(x,x,x));
        i = (Mathf.Abs(Mathf.Abs(x - 1) - 0.1f)) * rate + 0.01f * rate;
        x += i;
        if (x < 1.5f)
        {
            rate = Random.Range(rate + 0.3f * rate, rate - 0.3f * rate);
            rate = Mathf.Clamp(rate, -1.5f, 1.5f);
            rate = 0.005f;

        }
        else if (x > 2f)
        {
            rate = -0.005f;
        }
    }
}
