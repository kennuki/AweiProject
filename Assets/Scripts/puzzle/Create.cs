using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create : MonoBehaviour
{
    public GameObject[] Monster;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            foreach(GameObject monster in Monster)
            {
                monster.SetActive(true);
            }
            Destroy(this.gameObject);
        }
    }
}
