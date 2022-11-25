using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRotate : MonoBehaviour
{
    public GameObject[] monster;
    void Start()
    {
        StartCoroutine(RotateFunction());
    }
    public float MoveSP = 0.06f;
    public float RotateSP = 10f;
    private IEnumerator RotateFunction()
    {
        foreach(GameObject monster in monster)
        {
            monster.SetActive(true);
        }
        for (int i = 0; i < 9; i++)
        {
            transform.Rotate(0, 0, -RotateSP);
            transform.Translate(MoveSP, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
