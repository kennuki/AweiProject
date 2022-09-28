using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = false;
        cd = this.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        AIFunction();
    }


    public Transform PlayerTransform;
    int State = 0;
    private void AIFunction()
    {
        Vector3 playerPos = PlayerTransform.transform.position;
        Vector3 MyPos = transform.position;
        if (State == 0)
        {
            Patrol();
            if (Vector3.Distance(playerPos, MyPos) < 10)
            {
                State = 1;
            }
        }
        if (State == 1)
        {
            if (Vector3.Distance(playerPos, MyPos) > 10)
            {
                State = 0;
            }
            else if (Vector3.Distance(playerPos, MyPos) < 2.5f)
            {
                Chasing(0);
            }
            else
            {
                Chasing(2);
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
    NavMeshAgent agent;
    Collider cd;
    public Vector3 ran;
    float theta;
    float r = 10;
    float ran_x;
    float ran_y;
    float ran_CD = 5;
    void Patrol()
    {

        ran_CD += Time.deltaTime;
        agent.speed = 2f;
        if (ran_CD >= 5)
        {
            theta = Random.Range(0, Mathf.PI * 2);
            ran_x = Mathf.Cos(theta) * r;
            ran_y = Mathf.Sin(theta) * r;
            ran_CD = 0;
            ran = transform.position + (new Vector3(ran_x, 0, ran_y));
        }
        agent.SetDestination(ran);
    }
    void Chasing(float v)
    {

        agent.speed = v;
        agent.SetDestination(PlayerTransform.transform.position);
    }
    void Chasing2(float v)
    {

        Vector3 dir = transform.position - PlayerTransform.transform.position;
        agent.speed = v;
        agent.SetDestination(transform.position + dir * 3f);
    }
    /*void Shooting()
    {
        //攻擊
        shoot_CD+=Time.deltaTime;
        agent.speed=1f;
        agent.SetDestination(player.transform.position);
        if(shoot_CD>=0.5f)
        {
        Instantiate(bullet,emission.transform.position,emission.transform.rotation);
        shoot_CD=0;
        }
    }*/
    void Escape()
    {

        Vector3 dir = transform.position - PlayerTransform.transform.position;
        agent.speed = 4f;
        agent.SetDestination(transform.position + dir * 3f);
    }
}
