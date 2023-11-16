using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class BGM : MonoBehaviour
{
    public AudioMixer BGMMixer;
    public void ChangeValue(Slider slider)
    {
        BGMMixer.SetFloat("BGM", slider.value);
    }
}
