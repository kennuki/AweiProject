using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pintriger : MonoBehaviour
{
    Collider cd;
    // Start is called before the first frame update
    void Start()
    {
        cd = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Time.timeScale = 1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Pin")
        {
            Time.timeScale = 0.05f;
            Destroy(cd);
        }
    }
}
