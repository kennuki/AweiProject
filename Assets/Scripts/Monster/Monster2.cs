using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster2 : MonoBehaviour
{
    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = false;
        cd = this.GetComponent<Collider>();
        HedgeHogAbility = GetComponentInChildren<HedgeHogAbility>();

    }
    public HedgeHogAbility HedgeHogAbility;
    // Update is called once per frame
    protected virtual void Update()
    {
        if (HedgeHogAbility.Bump == false)
        {
            AIFunction();
        }

        
    }

    protected Animator anim;
    public Transform PlayerTransform;
    protected int State = 0;
    protected NavMeshAgent agent;
    protected Collider cd;
    public Vector3 ran;
    protected float theta;
    protected float r = 10;
    protected float ran_x;
    protected float ran_y;
    protected float ran_CD = 5;
    public float ChasingSpeed;
    public virtual void Patrol(float idletime)
    {
        ran_CD += Time.deltaTime;

        if (ran_CD >= 7)
        {
            theta = Random.Range(0, Mathf.PI * 2);
            ran_x = Mathf.Cos(theta) * r;
            ran_y = Mathf.Sin(theta) * r;
            ran_CD = Random.Range(0f, 3.0f);
            ran = transform.position + (new Vector3(ran_x, 0, ran_y));
        }
        else if (ran_CD < idletime)
        {
            anim.SetInteger("State", 0);
            agent.speed = 0;
        }
        else
        {
            anim.SetInteger("State", 1);
            agent.speed = 1.5f;
        }
        agent.SetDestination(ran);
    }
    public virtual void Chasing(float v)
    {
        anim.SetInteger("State", 1);
        agent.speed = v;
        agent.SetDestination(PlayerTransform.transform.position);
    }
    public virtual void Stop()
    {
        anim.SetInteger("State", 0);
        agent.speed = 0;
    }
    public virtual void Attack1()
    {
        anim.SetInteger("State", 2);
    }
    public virtual void Escape()
    {

        Vector3 dir = transform.position - PlayerTransform.transform.position;
        agent.speed = 4f;
        agent.SetDestination(transform.position + dir * 3f);
    }
    public virtual void AIFunction()
    {
        Vector3 playerPos = PlayerTransform.transform.position;
        Vector3 MyPos = transform.position;
        if (State == 0)
        {
            Patrol(2);
            if (Vector3.Distance(playerPos, MyPos) < 13)
            {
                State = 1;
            }
        }
        if (State == 1)
        {
            if (Vector3.Distance(playerPos, MyPos) >= 13)
            {
                ran_CD = 0;
                State = 0;
            }
            else if (Vector3.Distance(playerPos, MyPos) < 13f && Vector3.Distance(playerPos, MyPos) > 2.2f)
            {
                Chasing(ChasingSpeed);
            }
            else
            {
                Stop();
            }
            /*if(Vector3.Distance(playerPos,MyPos)<10)
            {
                State=2;
            }*/
        }
        /*if(State==2)
        {
            Shooting();
            if(Vector3.Distance(playerPos,MyPos)>13)
            {
                State=1;
            }
            if(Vector3.Distance(playerPos,MyPos)<5)
            {
                State=3;
            }
        }*/
        if (State == 3)
        {
            Escape();
            if (Vector3.Distance(playerPos, MyPos) > 10)
            {
                State = 0;
            }
        }
    }
}
