using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFollow : MonoBehaviour
{
    Rigidbody rb;
    public GameObject Target;
    public Vector3 fly;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        fly = new Vector3(Random.Range(-5f, 5f), 2, Random.Range(-5f, 5f));
        rb.velocity = fly * 0.1f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 wp = Target.transform.position;
        Vector3 sp = rb.transform.position;
        Vector3 p = (wp - sp) * 2f;
        if (p.x != 0 && p.y != 0)
        {
            rb.velocity = new Vector3(p.x, p.y, p.z);
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, Target.transform.rotation, 0.04f);
    }
}
