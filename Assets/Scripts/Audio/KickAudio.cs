using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickAudio : MonoBehaviour
{
    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Monster")
        {
            audioSource.Play();
        }
    }
}
