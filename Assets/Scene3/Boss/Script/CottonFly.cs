using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CottonFly : MonoBehaviour
{
    
    void Start()
    {
        float a = Random.Range(0.7f, 1.5f);
        force = Random.Range(0.05f, 1f);
        transform.localScale = new Vector3(a, a, a);
        Destroy(this.gameObject, 7f);
    }

    float force;
    void Update()
    {
        if (force > 0)
        {
            force -= Time.deltaTime;
            transform.Translate(new Vector3(0, 0, force));
        }
    }
    
}
