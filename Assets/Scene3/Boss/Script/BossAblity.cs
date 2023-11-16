using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BossAblity : MonoBehaviour
{
    public float MaxHP;
    public float HP;
    public float AttackDamage;
    public static float AD;
    public float AS=1;
    public bool StaticMonster = false;
    public bool Bump = false;
    private NavMeshAgent agent;
    public Animator anim;
    public GameObject parent;
    protected Rigidbody rig;
    public GameObject Teleporter;
    public GameObject OwariTrigger;
    protected void Start()
    {

        HP = MaxHP;
        CA = GameObject.Find("Character").GetComponent<CharacterAbility>();
        agent = GetComponentInParent<NavMeshAgent>();
        ImaHP = HP;
        rig = parent.GetComponent<Rigidbody>();
    }
    protected int i = 0;
    protected void Update()
    {
        anim.SetFloat("AS", AS);
        AD = AttackDamage;
        if (HP <= 0)
        {
            Teleporter.SetActive(true);
            OwariTrigger.SetActive(true);

            i++;
            if (i == 1)
            {
                StartCoroutine(SpawnItem(itemTypes, ItemAmount, amount));
                i++;
                Destroy(HPBoard);
                Destroy(parent);
            }

        }
        DistanceToPlayer();
        HitFunction();
        HPBarFunction();
    }

    public CharacterAbility CA;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            HP -= CA.AD * (Mathf.Clamp(Random.Range(CA.CritRate + 0.1f, CA.CritRate + 1.1f) - 1, 0, 1) + 1);
            Destroy(other.gameObject);
            anim.SetBool("Hit", true);

        }
        else if (other.tag == "Bullet2")
        {
            HP -= CA.AD * (Mathf.Clamp(Random.Range(CA.CritRate + 0.1f, CA.CritRate + 1.1f) - 1, 0, 1) + 1) * 1.3f;
            Destroy(other.gameObject);
            anim.SetBool("Hit", true);
        }
        else if (other.tag == "SkillBullet")
        {
            HP -= CA.AD * (Mathf.Clamp(Random.Range(CA.CritRate + 0.1f, CA.CritRate + 1.1f) - 1, 0, 1) + 1) * 1.4f;
            Destroy(other.gameObject);
            anim.SetBool("Hit", true);
        }
        else if (other.tag == "SkillBullet2" && other.GetType()==typeof(CapsuleCollider)&& ExplodeAllow == true)
        {
            StartCoroutine(ExplodeHit());
            HP -= CA.AD * (Mathf.Clamp(Random.Range(CA.CritRate + 0.1f, CA.CritRate + 1.1f) - 1, 0, 1) + 1) * 7.5f;
            Destroy(other.gameObject, 0.1f);
            anim.SetBool("Hit", true);
        }
        else if (other.tag == "Kick" && KickAllow == true)
        {
            StartCoroutine(HitAllow());
            if (StaticMonster == false)
            {
                HP -= CA.AD * (Mathf.Clamp(Random.Range(CA.CritRate + 0.1f, CA.CritRate + 1.1f) - 1, 0, 1) + 1) * 2.2f;
                anim.SetBool("Hit", true);
            }
            else if (StaticMonster == true)
            {
                HP -= CA.AD * (Mathf.Clamp(Random.Range(CA.CritRate + 0.1f, CA.CritRate + 1.1f) - 1, 0, 1) + 1) * 4f;
            }
        }
    }
    private bool KickAllow = true;
    private bool ExplodeAllow = true;
    private IEnumerator HitAllow()
    {
        KickAllow = false;
        yield return new WaitForSeconds(1f);
        KickAllow = true;
    }
    private IEnumerator ExplodeHit()
    {
        ExplodeAllow = false;
        yield return new WaitForSeconds(1f);
        ExplodeAllow = true;
    }
    public GameObject HPBoard;
    public float ShowHPDistance = 18;
    protected void DistanceToPlayer()
    {
        float dist = Vector3.Distance(CA.transform.position, transform.position);

        if (dist < ShowHPDistance)
        {
            HPBoard.SetActive(true);
        }
        else
        {
            HPBoard.SetActive(false);
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
    public Image HP1;
    public Image HP1Slow;
    public Image HP2;
    public Image HP2Slow;
    private void HPBarFunction()
    {
        HP1.fillAmount = (HP - (MaxHP*0.5f)) / (MaxHP - (MaxHP*0.5f));
        HP2.fillAmount = HP / (MaxHP * 0.5f);
        HP1Slow.fillAmount = Mathf.Lerp((HP - (MaxHP * 0.5f)) / (MaxHP - (MaxHP * 0.5f)), (ImaHP - (MaxHP * 0.5f)) / (MaxHP - (MaxHP * 0.5f)),Time.deltaTime);
        HP2Slow.fillAmount = Mathf.Lerp(HP / (MaxHP * 0.5f), ImaHP / (MaxHP * 0.5f), Time.deltaTime);

    }



    public Item.ItemType[] itemTypes;
    public int[] ItemAmount;
    public int amount;
    public virtual IEnumerator SpawnItem(Item.ItemType[] itemTypes_, int[] ItemAmount_, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            for (int j = 0; j < ItemAmount_[i]; j++)
            {
                ItemWorld.SpawnItemWorld(transform.position + new Vector3(1f * Mathf.Sin(Random.Range(0f, 360f) * Mathf.Deg2Rad) * Random.Range(0f, 1f), 0.5f, 1f * Mathf.Sin(Random.Range(0f, 360f) * Mathf.Deg2Rad) * Random.Range(0f, 1f)), new Item { itemType = itemTypes_[i], amount = 1 });
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
