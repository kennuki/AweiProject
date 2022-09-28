using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionRotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    float VibrateRate = 0.05f;
    float c = 0.00003f;
    void Update()
    {
        transform.Rotate(0, 0.5f, 0,Space.World);
        VibrateRate -= c;
        if (VibrateRate > 0)
        {
            c+=0.00003f;
        }
        if (VibrateRate <= 0)
        {
            c-=0.00003f;
        }

        transform.Translate(0, c, 0);
    }
}
