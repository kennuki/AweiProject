using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRotate : MonoBehaviour
{
    public GameObject[] monster;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.Play();
        }
        StartCoroutine(RotateFunction());
    }
    public float MoveSP = 0.06f;
    public float RotateSP = 10f;
    public float time = 9;
    private IEnumerator RotateFunction()
    {
        foreach(GameObject monster in monster)
        {
            monster.SetActive(true);
        }
        for (int i = 0; i < time; i++)
        {
            transform.Rotate(0, 0, -RotateSP);
            transform.Translate(MoveSP, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
