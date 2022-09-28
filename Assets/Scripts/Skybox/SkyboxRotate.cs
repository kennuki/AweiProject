using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxRotate : MonoBehaviour
{
    void Start()
    {
        RenderSettings.skybox.SetFloat("_Rotation", -10f);
    }
    float rotateangleadjust=140;
    float x = 1;
    float i;
    float rate = 0.01f;
    void Update()
    {
        rotateangleadjust += Time.deltaTime*2;
        if (rotateangleadjust >= 360)
        {
            rotateangleadjust -= 360;
        }
        RenderSettings.skybox.SetFloat("_Rotation", rotateangleadjust);
        RenderSettings.skybox.SetFloat("_Exposure", x);
        i = (Mathf.Abs(Mathf.Abs(x - 1) - 0.1f)) * rate + 0.01f * rate;
        x += i;
        if (x < 0.9f)
        {
            rate = Random.Range(rate + 0.3f*rate, rate - 0.3f*rate);
            rate = Mathf.Clamp(rate, -0.015f, 0.015f);
            rate = 0.01f;
        }
        else if (x > 1.1f)
        {
            rate = -0.01f;
        }
    }
}
