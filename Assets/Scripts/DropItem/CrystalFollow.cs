using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalFollow : MonoBehaviour
{
    Rigidbody rb;
    float rsp = 0;
    public GameObject Player;
    public Character character;
    public Vector3 fly;
    bool eaeaea = false;
    public int CrystalDrop;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        Player = GameObject.Find("Character");
       character = Character.FindObjectOfType<Character>();
        fly = new Vector3(Random.Range(-5f, 5f), 2, Random.Range(-5f, 5f));
        rb.velocity = fly * 0.1f;
        StartCoroutine(TimeDelay());
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        rsp += Time.deltaTime * 250;
        rb.transform.rotation = Quaternion.Euler(0, rsp, 0);

        if (eaeaea == true)
        {
            if (Mathf.Sqrt((Mathf.Pow((Player.transform.position.x - transform.position.x), 2) + Mathf.Pow((Player.transform.position.z - transform.position.z), 2))) < 10)
            {
                Vector3 wp = Player.transform.position;
                Vector3 sp = rb.transform.position;
                Vector3 p = (wp - sp) * 1.8f;
                if (p.x != 0 && p.y != 0)
                {
                    rb.velocity=new Vector3(p.x,p.y,p.z);
                }
            }
        }
    }


    IEnumerator TimeDelay()
    {
        yield return new WaitForSeconds(1f);
        eaeaea = true;
    }
}
