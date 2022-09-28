using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Renderer bt;

    void Start()
    {
        Vector3 dir = Shoot.ShootDir;
        bt = GetComponent<Renderer>();
        
        Destroy(gameObject, 1.4f);
    }
    float t;
    void Update()
    {
        t += Time.deltaTime;
        transform.Translate(-45f * Time.deltaTime, 0, 0);
        bt.material.color = new Color(bt.material.color.r, bt.material.color.g, bt.material.color.b, 1 - t);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Monster")
        {
            Destroy(this.gameObject);
        }

    }
}
