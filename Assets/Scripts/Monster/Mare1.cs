using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mare1 : Monster2
{
    private AudioSource audioSource;
    public float detectdistance = 13;
    public CapsuleCollider TriggerCollider;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public override void AIFunction()
    {
        Vector3 playerPos = PlayerTransform.transform.position;
        Vector3 MyPos = transform.position;
        if (State == 0)
        {
            if (HedgeHogAbility.HP != HedgeHogAbility.ImaHP)
            {
                detectdistance += 3;
            }
            Patrol(2);
            if (Vector3.Distance(playerPos, MyPos) < detectdistance&&angle<45)
            {
                audioSource.Play();
                State = 1;
            }
        }
        if (State == 1)
        {
            if (Vector3.Distance(playerPos, MyPos) >= detectdistance)
            {
                if (HedgeHogAbility.HP != HedgeHogAbility.ImaHP)
                {
                    detectdistance += 3;
                }
                ran_CD = 0;
                State = 0;
            }
            else if (Vector3.Distance(playerPos, MyPos) < detectdistance && Vector3.Distance(playerPos, MyPos) > 2.2f)
            {
                Chasing(ChasingSpeed);
            }
            else if (Vector3.Distance(playerPos, MyPos) <= 2.2f)
            {
                State = 2;
            }
        }
        if (State == 2)
        {
            if (agent.speed > 5)
            {
                agent.velocity = transform.rotation * new Vector3(0, 0, 8);
            }
            agent.speed = 0;
            Attack1();
            if (anim.GetBool("AttackEnd") == true)
            {
                State = 3;
            }
            if (Vector3.Distance(playerPos, MyPos) > 2.2f)
            {
                State = 1;
            }
        }
        if (State == 3)
        {
            RotateFunction();
            AttackFunction(1f);
        }
    }
    public void RotateFunction()
    {
        float faceAngle;
        faceAngle = Mathf.Atan2(PlayerTransform.transform.position.x - transform.position.x, PlayerTransform.transform.position.z - transform.position.z) * Mathf.Rad2Deg;
        Quaternion targetrotatoin = Quaternion.Euler(transform.eulerAngles.x, faceAngle, transform.eulerAngles.z);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetrotatoin, 0.1f);
    }
    public override void Attack1()
    {
        agent.speed = 0.01f;
        agent.angularSpeed = 100000f;
        if (agent.isOnNavMesh)
        {
            agent.SetDestination(PlayerTransform.transform.position);
        }
        else
        {
            agent.velocity = transform.rotation * new Vector3(0, 4, 0);
        }

        anim.SetInteger("State", 2);
        anim.SetBool("AttackEnd", true);
    }
    float idle_CD = 0;
    public void AttackFunction(float idletime)
    {
        
        idle_CD += Time.deltaTime;
        if (idle_CD > 0.1f && idle_CD < 0.12f)
        {
            anim.SetBool("AttackEnd", false);
        }
        if (idle_CD > 0.3f && idle_CD < 0.32f)
        {
            TriggerCollider.enabled = true;
        }
        if (idle_CD > 0.8f && idle_CD < 0.82f)
        {
            
            idle_CD += 0.02f;
            TriggerCollider.enabled = false;

            
        }
        if (idle_CD > idletime)
        {

            agent.angularSpeed = 250f;
            State = 2;
            idle_CD = 0;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Character")
        {
            TriggerCollider.enabled = false;
        }
    }
}
