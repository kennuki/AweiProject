using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HedgeHogAbility : MonsterAbility
{
    public bool StaticMonster = false;
    public bool Bump = false;
    private NavMeshAgent agent;
    public Animator animator;
    public GameObject parent;
    protected Rigidbody rig;
    protected void Start()
    {
        CA = GameObject.Find("Character").GetComponent<CharacterAbility>();
        agent = GetComponentInParent<NavMeshAgent>();
        ImaHP = HP;
        rig = parent.GetComponent<Rigidbody>();
    }
    protected int i = 0;
    protected void Update()
    {
        if (HP <= 0)
        {
            i++;
            if (i == 1)
            {
                StartCoroutine(SpawnItem(itemTypes,ItemAmount,amount));
                i++;
            }
            Destroy(parent,0.1f);
        }
        DistanceToPlayer();
        HitFunction();
    }
    private bool DropAllow = false;
    public CharacterAbility CA;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Floor")
        {
            if (agent != null)
                DropAllow = false;
        }
        if (other.tag == "Bullet")
        {
            StartCoroutine(DecayedForceZ(80, 150));
            HP -= CA.AD * (Mathf.Clamp(Random.Range(CA.CritRate + 0.1f, CA.CritRate + 1.1f) - 1, 0, 1) + 1);
            Destroy(other.gameObject);
            animator.SetBool("Hit", true);

        }
        else if (other.tag == "Bullet2")
        {
            StartCoroutine(DecayedForceZ(80, 200));
            HP -= CA.AD * (Mathf.Clamp(Random.Range(CA.CritRate + 0.1f, CA.CritRate + 1.1f) - 1, 0, 1) + 1)*1.5f;
            Destroy(other.gameObject);
            animator.SetBool("Hit", true);
        }
        else if (other.tag == "SkillBullet")
        {
            StartCoroutine(DecayedForceZ(80, 200));
            HP -= CA.AD * (Mathf.Clamp(Random.Range(CA.CritRate + 0.1f, CA.CritRate + 1.1f) - 1, 0, 1) + 1) * 1.4f;
            Destroy(other.gameObject);
            animator.SetBool("Hit", true);
        }
        else if (other.tag == "SkillBullet2" && other.GetType() == typeof(CapsuleCollider) && ExplodeAllow == true)
        {
            StartCoroutine(DecayedForceZ(80, 200));
            HP -= CA.AD * (Mathf.Clamp(Random.Range(CA.CritRate + 0.1f, CA.CritRate + 1.1f) - 1, 0, 1) + 1) * 9f;
            Destroy(other.gameObject,0.1f);
            animator.SetBool("Hit", true);
        }
        else if (other.tag == "Kick"&&KickAllow == true)
        {
            if(StaticMonster == false)
            {
                KickAllow = false;
                StartCoroutine(DecayedForceZ(150, 150));
                StartCoroutine(DecayedForceY(200, 500, 50));
                HP -= CA.AD * (Mathf.Clamp(Random.Range(CA.CritRate + 0.1f, CA.CritRate + 1.1f) - 1, 0, 1) + 1) * 2.2f;
                animator.SetBool("Hit", true);
            }
            else if(StaticMonster == true)
            {
                HP -= CA.AD * (Mathf.Clamp(Random.Range(CA.CritRate + 0.1f, CA.CritRate + 1.1f) - 1, 0, 1) + 1) * 0.5f;
            }
        }
    }
    private bool ExplodeAllow = true;
    private bool KickAllow = true;
    private IEnumerator ExplodeHit()
    {
        ExplodeAllow = false;
        yield return new WaitForSeconds(1f);
        ExplodeAllow = true;
    }

    public IEnumerator DecayedForceZ(float force,float decayedspeed)
    {
        Quaternion q = Quaternion.Euler(transform.eulerAngles);
        Vector3 f = new Vector3(0, 0, -force);
        Vector3 TargetForce = q * f;
        rig.AddForce(TargetForce);
        for(float i = 0; i < force; i += Time.deltaTime* decayedspeed)
        {
            rig.AddForce(TargetForce.normalized * -Time.deltaTime * decayedspeed);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        rig.velocity = Vector3.zero;
        yield break;
    }
    public IEnumerator DecayedForceY(float force, float decayedspeed,float timerate)
    {
        agent.enabled = false;
        Bump = true;
        Quaternion q = Quaternion.Euler(transform.eulerAngles);
        Vector3 f = new Vector3(0, force, 0);
        Vector3 TargetForce = q * f;
        float y = TargetForce.y;
        rig.AddForce(TargetForce);
        DropAllow = true;
        for (float i = 0; i < force; i += Time.deltaTime * timerate)
        {
            y += TargetForce.normalized.y * -Time.deltaTime * decayedspeed;
            rig.AddForce(TargetForce.normalized * -Time.deltaTime * decayedspeed);
            if (y < -TargetForce.y||DropAllow == false)
            {
                rig.velocity = Vector3.zero;
                Bump = false;
                agent.enabled = true;
                KickAllow = true;
                yield break;
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
        rig.velocity = Vector3.zero;
        Bump = false;
        agent.enabled = true;
        KickAllow = true;
        yield break;
    }
    public void HitAnime()
    {
        animator.SetBool("Hit", false);
    }

    public bool deadbydaylight = false;
    public float ShowHPDistance=18;
    protected void DistanceToPlayer()
    {
        float dist = Vector3.Distance(CA.transform.position, transform.position);
        
        if (dist < ShowHPDistance)
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
    protected void HitFunction()
    {
        if (ImaHP != HP)
        {
            HitNum = HP - ImaHP;
            Instantiate(ShowHit, transform.position, Quaternion.identity);
            ImaHP = HP;
            CharacterAbility.HPSteal += 1;
        }
    }
    public Item.ItemType[] itemTypes;
    public int[] ItemAmount;
    public int amount;
    public virtual IEnumerator SpawnItem(Item.ItemType[] itemTypes_, int[] ItemAmount_,int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            for (int j = 0; j < ItemAmount_[i]; j++)
            {
                ItemWorld.SpawnItemWorld(transform.position+new Vector3(1f*Mathf.Sin(Random.Range(0f,360f)*Mathf.Deg2Rad)*Random.Range(0f,1f),0.5f, 1f * Mathf.Sin(Random.Range(0f, 360f) * Mathf.Deg2Rad) * Random.Range(0f, 1f)), new Item { itemType = itemTypes_[i], amount = 1 });
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
