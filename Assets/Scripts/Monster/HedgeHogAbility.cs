using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HedgeHogAbility : MonsterAbility
{
    public Animator animator;
    public GameObject parent;
    int i = 0;

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            i++;
            if (i == 1)
            {
                StartCoroutine(SpawnCrystal());
                i++;
            }
            Destroy(parent,0.1f);
        }
        DistanceToPlayer();
        HitFunction();
    }

    public CharacterAbility CA;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            Debug.Log(CA.AD * (Mathf.Clamp(Random.Range(CA.CritRate + 0.1f, CA.CritRate + 1.1f) - 1, 0, 1) + 1));
            HP -= CA.AD * (Mathf.Clamp(Random.Range(CA.CritRate + 0.1f, CA.CritRate + 1.1f) - 1, 0, 1) + 1);
            Destroy(other.gameObject);
            animator.SetBool("Hit", true);
            animator.SetBool("Idle", false);
            StartCoroutine(TimeCount(0.2f));

        }
        if (other.tag == "Bullet2")
        {
            HP -= CA.AD * (Mathf.Clamp(Random.Range(CA.CritRate + 0.1f, CA.CritRate + 1.1f) - 1, 0, 1) + 1)*1.5f;
            Destroy(other.gameObject);
            animator.SetBool("Hit", true);
            animator.SetBool("Idle", false);
            StartCoroutine(TimeCount(0.2f));
        }
    }

    private IEnumerator TimeCount(float WaitTime)
    {
        yield return new WaitForSeconds(WaitTime);
        HitAnime();

    }

    public void HitAnime()
    {
        animator.SetBool("Hit", false);
        animator.SetBool("Idle", true);
    }

    public bool deadbydaylight = false;
    void DistanceToPlayer()
    {
        float dist = Vector3.Distance(CA.transform.position, transform.position);
        if (dist < 20)
        {
            deadbydaylight = true;
        }
        else
        {
            deadbydaylight = false;
        }
    }

    public static float HitNum;
    public float ImaHP;
    public GameObject ShowHit;
    void Start()
    {
        ImaHP = HP;

    }
    void HitFunction()
    {
        if (ImaHP != HP)
        {
            HitNum = HP - ImaHP;
            Instantiate(ShowHit, transform.position, Quaternion.identity);
            ImaHP = HP;

        }
    }
    private IEnumerator SpawnCrystal()
    {
        for (int i = 0; i < Random.Range(5,10); i++)
        {
            ItemWorld.SpawnItemWorld(transform.position, new Item { itemType = Item.ItemType.Crystal, amount = 1 });
            yield return new WaitForFixedUpdate();
        }

    }
}
