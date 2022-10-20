using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRotate : MonoBehaviour
{
    public GameObject monster;
    Collider cd;
    void Start()
    {
        cd = GetComponent<Collider>();
    }
    float MoveSP = 0.06f;
    float RotateSP = 10f;
    void Update()
    {
        
    }
    private IEnumerator RotateFunction()
    {
        for (int i = 0; i < 9; i++)
        {
            transform.Rotate(0, 0, -RotateSP);
            transform.Translate(MoveSP, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            monster.SetActive(true);    
            StartCoroutine(RotateFunction());
            Destroy(cd);
        }
    }
}
