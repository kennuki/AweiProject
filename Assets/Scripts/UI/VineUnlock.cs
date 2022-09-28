using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineUnlock : MonoBehaviour
{
    Collider cd;
    public GameObject[] vines;
    void Start()
    {
        cd = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            foreach(GameObject vine in vines)
            {
                Destroy(vine.gameObject);
            }
        }
    }
}
