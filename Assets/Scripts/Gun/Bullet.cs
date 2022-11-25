using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public ParticleSystem ExplodeMonster;
    public ParticleSystem ExplodeWall;
    private Collider cd;
    void Start()
    {
        Vector3 dir = Shoot.ShootDir;
        cd = GetComponent<Collider>();
        Destroy(gameObject, 0.5f);
    }
    float speed = -45f;
    void Update()
    {
        transform.Translate(speed*Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Monster")
        {
            ExplodeMonster.gameObject.transform.SetParent(null);
            ExplodeMonster.gameObject.SetActive(true);
            ExplodeMonster.Play();
            Destroy(cd);
            Destroy(this.gameObject,0.1f);
            Destroy(ExplodeMonster.gameObject, 0.2f);
        }
        else if (other.tag == "Wall"|| other.tag == "Floor")
        {
            ExplodeWall.gameObject.transform.SetParent(null);
            ExplodeWall.gameObject.SetActive(true);
            ExplodeWall.Play();
            Destroy(this.gameObject, 0.1f);
            Destroy(ExplodeWall.gameObject, 0.2f);
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Wall")
        {
            ExplodeWall.gameObject.transform.DetachChildren();
            ExplodeWall.gameObject.SetActive(true);
            ExplodeWall.Play();
            Destroy(cd);
            Destroy(this.gameObject, 0.1f);
            Destroy(ExplodeWall.gameObject, 0.1f);
        }
    }
}
