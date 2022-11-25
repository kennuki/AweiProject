using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hedgehog2 : Monster2
{
    public float detectdistance = 13;
    public ParticleSystem Sasu;
    public CapsuleCollider TriggerCollider;
    public override void AIFunction()
    {
        Vector3 playerPos = PlayerTransform.transform.position;
        Vector3 MyPos = transform.position;
        if (State == 0)
        {
            Patrol(2);
            if (Vector3.Distance(playerPos, MyPos) < detectdistance)
            {
                State = 1;
            }
        }
        if (State == 1)
        {
            if (Vector3.Distance(playerPos, MyPos) >= detectdistance)
            {
                ran_CD = 0;
                State = 0;
            }
            else if (Vector3.Distance(playerPos, MyPos) < detectdistance && Vector3.Distance(playerPos, MyPos) > 2.5f)
            {
                Chasing(ChasingSpeed);
            }
            else if(Vector3.Distance(playerPos, MyPos) <= 2.5f)
            {
                State = 2;
            }
        }
        if(State==2)
        {
            Attack1();
            if (anim.GetBool("AttackEnd") == true)
            {
                State = 3;
            }
            if(Vector3.Distance(playerPos,MyPos)>2.8f)
            {
                Sasu.Stop();
                State =1;
            }
        }
        if (State == 3)
        {
            AttackFunction(1.5f);
        }
    }
    public override void Attack1()
    {
        agent.speed =0.05f;
        agent.angularSpeed = 100000f;
        agent.SetDestination(PlayerTransform.transform.position);
        Sasu.Play();
        anim.SetInteger("State", 2);
        anim.SetBool("AttackEnd", true);
    }
    float idle_CD = 0;
    public void AttackFunction(float idletime)
    {
        idle_CD += Time.deltaTime;
        if (idle_CD > 0.39f && idle_CD<0.42f)
        {
            idle_CD += 0.02f;
            TriggerCollider.enabled = true;
        }
        agent.SetDestination(PlayerTransform.transform.position);
        if (idle_CD > idletime)
        {
            TriggerCollider.enabled = false;
            agent.angularSpeed = 250f;
            State = 2;
            idle_CD = 0;
            anim.SetBool("AttackEnd", false);
            
        }
    }

}
