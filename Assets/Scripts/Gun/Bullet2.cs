using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2 : MonoBehaviour
{
    public GameObject btt;
    public Renderer bt;
    void Start()
    {
        Destroy(gameObject, 1.4f);
    }
    float t;
    public static float pn=0;
    void Update()
    {
        t += Time.deltaTime;
        btt.transform.Rotate(pn, 0, 0);
        pn = -pn;
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
