using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public ParticleSystem ExplodeMonster;
    public ParticleSystem ExplodeWall;
    private BoxCollider cd;
    public Collider ExplodeCd;
    public float time = 0.5f;
    void Start()
    {
        Vector3 dir = Shoot.ShootDir;
        cd = GetComponent<BoxCollider>();
        StartCoroutine(CdDelayActive());
        Destroy(gameObject, time);
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
            if (ExplodeCd != null)
            {
                ExplodeCd.enabled = true;
            }
            ExplodeMonster.gameObject.transform.SetParent(null);
            ExplodeMonster.gameObject.SetActive(true);
            ExplodeMonster.Play();
            Destroy(cd);
            Destroy(this.gameObject,0.1f);
            Destroy(ExplodeMonster.gameObject, 2f);
        }
        else if (other.tag == "Wall"|| other.tag == "Floor")
        {
            if (ExplodeCd != null)
            {
                ExplodeCd.enabled = true;
            }
            ExplodeWall.gameObject.transform.SetParent(null);
            ExplodeWall.gameObject.SetActive(true);
            ExplodeWall.Play();
            Destroy(this.gameObject, 0.1f);
            Destroy(ExplodeWall.gameObject, 2f);
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
            Destroy(ExplodeWall.gameObject, 2f);
        }
    }
    private IEnumerator CdDelayActive()
    {
        yield return new WaitForSeconds(0.05f);
        if(cd!=null)
        cd.enabled = true;
    }
}
