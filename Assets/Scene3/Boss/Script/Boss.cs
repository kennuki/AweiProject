using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;
using UnityEngine.Audio;

public class Boss : MonoBehaviour
{
    private GameObject CM1;
    private CameraRotation cameraRotation;
    protected virtual void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(StartAIFunction());
        CM1 = GameObject.Find("CM vcam1");
        cameraRotation = CM1.GetComponent<CameraRotation>();
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = false;
        cd = this.GetComponent<Collider>();
        bossAblity = GetComponentInChildren<BossAblity>();
        PlayerTransform = GameObject.Find("Character").transform;
        StartCoroutine(TimedelayDEbug());
    }
    public BossAblity bossAblity;
    public bool StartAI = false;
    private IEnumerator StartAIFunction()
    {
        yield return new WaitForSeconds(7.4f);
        StartAI = true;
        anim.SetInteger("State", 0);
    }
    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            StartCoroutine(cameraRotation.CameraShake());
        }
        if (StartAI == true)
        {
            AIFunction();
        }
        AS = anim.GetFloat("AS");
        SkillCoolDownFunction();
    }
    private IEnumerator TimedelayDEbug()
    {
        while (true)
        {
            //Debug.Log(agent.speed);
            //Debug.Log(agent.angularSpeed);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private AudioSource audioSource;
    public AudioClip[] Action;
    [System.Serializable]
    public class Skill
    {
        public string skillname;
        public float cooldown;
        public float ActiveTimeLeft = 0;
        public int skillstate = 0;      // skillstate 0 = inactive      1 = active can use  2 = active cannot use
        public float skilltime;
    }
    public Skill[] skills;
    public void SkillCoolDownFunction()
    {
        foreach (Skill skill in skills)
        {
            if (skill.skillstate > 0)
            {
                skill.ActiveTimeLeft -= Time.deltaTime;
                if (skill.ActiveTimeLeft <= 0)
                {
                    skill.skillstate = 1;
                }
                else if (skill.ActiveTimeLeft > 0)
                {
                    skill.skillstate = 2;
                }
            }
        }
    }
    public void SkillUseFunction(string skillname_, float cooldown_)
    {
        foreach (Skill skill in skills)
        {
            if (skill.skillname == skillname_)
            {
                skill.ActiveTimeLeft = cooldown_;
                skill.skillstate = 2;
            }

        }
    }
    public GameObject QuakeCarrot;
    public IEnumerator QuakeSkill()
    {
        int CarrotCount;
        if (bossAblity.HP / bossAblity.MaxHP > 0.5f)
        {
            CarrotCount = 15;
        }
        else
        {
            CarrotCount = 25;
        }
        yield return new WaitForSeconds(1.5f);
        audioSource.PlayOneShot(Action[1], 0.36f);
        audioSource.PlayOneShot(Action[2], 0.55f);
        StartCoroutine(cameraRotation.CameraShake());
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < 3; i++)
        {
            for(int j =0;j< CarrotCount; j++)
            {
                float Dis = Random.Range(0, 23);    //‹——£’†SˆÊ’u
                Dis += Random.Range(0, 23 - Dis);   //Œ¸­’†Sêy—…”gÉ
                float Angle = Random.Range(0, 360.0f);
                Vector3 AppearPos = new Vector3(Dis * Mathf.Sin(Angle), QuakeCarrot.transform.position.y, Dis * Mathf.Cos(Angle));
                Instantiate(QuakeCarrot, AppearPos, Quaternion.identity);
            }
            yield return new WaitForSeconds(1f);
        }
    }
    public GameObject Monster1;
    public GameObject Monster2;
    public IEnumerator MonsterSummon()
    {
        int Count = Random.Range(3, 6);
        yield return new WaitForSeconds(1.5f);
        audioSource.PlayOneShot(Action[1], 0.3f);
        StartCoroutine(cameraRotation.CameraShake());
        yield return new WaitForSeconds(0.7f);

        for (int j = 0; j < Count; j++)
        {
            float Dis = Random.Range(0, 6);
            Dis += Random.Range(0, 6 - Dis);
            float Angle = Random.Range(0, 360.0f);
            Vector3 AppearPos = new Vector3(Dis * Mathf.Sin(Angle), 2.1f, Dis * Mathf.Cos(Angle));
            int a = Random.Range(1, 5);
            if (a == 1)
            {
                Instantiate(Monster1, AppearPos, Quaternion.identity);
            }
            else
            {
                Instantiate(Monster2, AppearPos, Quaternion.identity);
            }
        }


    }
    public void JumpToCenter()
    {
        anim.SetInteger("State", 3);
        float a = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        float b = anim.GetCurrentAnimatorStateInfo(0).length;

        if (agent.isOnNavMesh)
        {
            agent.SetDestination(Center.transform.position);
        }
        else
        {
            agent.velocity = transform.rotation * new Vector3(0, 4, 0);
        }
        agent.acceleration = 200;
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Quake") == true)
        {
            if (((a % b) / b) % 0.5f < 0.1f || ((a % b) / b) % 0.5f > 0.35f)
            {
                agent.speed = 0.1f;
            }
            else if (((a % b) / b) % 0.5f >= 0.1f && ((a % b) / b) % 0.5f <= 0.35f)
            {
                agent.speed = (0.35f - ((a % b) / b) % 0.5f) * ChasingSpeed * Random.Range(150, 200);
            }
        }
        if (((a % b) / b) % 0.5f >=0.21f && ((a % b) / b) % 0.5f <= 0.211f)
        {
            audioSource.PlayOneShot(Action[1], 0.35f);
        }
    }
    public GameObject Cotton;
    public IEnumerator CottonSkill()
    {
        Vector3 Pos = transform.position - new Vector3(0, 3.2f, 0);
        audioSource.PlayOneShot(Action[4], 0.2f);
        yield return new WaitForSeconds(1.5f);
        for (int j = 0; j < 45; j++)
        {
            float Angle = Random.Range(0, 360.0f);
            Instantiate(Cotton, Pos, Quaternion.Euler(0, Angle, 0));
            yield return new WaitForSeconds(0.01f);
        }
    }
    public GameObject Center;
    float AS;
    protected Animator anim;
    public Transform PlayerTransform;
    protected int State = 0;
    protected NavMeshAgent agent;
    public float detectdistance = 13;
    public BoxCollider TriggerCollider;
    public CapsuleCollider TriggerCollider2;
    public BoxCollider TriggerCollider3;
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
        float a = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        float b = anim.GetCurrentAnimatorStateInfo(0).length;
        ran_CD += Time.deltaTime;
        if (ran_CD >= 6)
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
            anim.SetFloat("IdleRun", 0);
            if (anim.GetCurrentAnimatorStateInfo(1).IsName("Jump_Chase"))
            {
                if (((a % b) / b)%0.5f < 0.1f || ((a % b) / b) % 0.5f > 0.35f)
                {
                    agent.speed = 0.1f;
                }
                else if (((a % b) / b) % 0.5f >= 0.1f && ((a % b) / b) % 0.5f <= 0.35f)
                {
                    agent.speed =  (0.35f - ((a % b) / b) % 0.5f) * Random.Range(80,110);
                }
                if (((a % b) / b) % 0.5f >= 0.33f && ((a % b) / b) % 0.5f <= 0.34f)
                {
                    audioSource.PlayOneShot(Action[3], 0.04f);
                }
            }
            else
            {
                agent.speed = 0;
            }

        }
        else
        {
            anim.SetInteger("State", 1);
            anim.SetFloat("IdleRun", 0);
            if (anim.GetCurrentAnimatorStateInfo(1).IsName("Jump_Chase"))
            {
                if (((a % b) / b) % 0.5f < 0.1f || ((a % b) / b) % 0.5f > 0.35f)
                {
                    agent.speed = 0.1f;
                }
                else if (((a % b) / b) % 0.5f >= 0.1f && ((a % b) / b) % 0.5f <= 0.35f)
                {
                    agent.speed = (0.35f - ((a % b) / b) % 0.5f) * Random.Range(80, 110);
                }
                if (((a % b) / b) % 0.5f >= 0.33f && ((a % b) / b) % 0.5f <= 0.34f)
                {
                    audioSource.PlayOneShot(Action[3], 0.04f);
                }
            }

        }
        if (agent.isOnNavMesh)
        {
            agent.SetDestination(ran);
        }
        else
        {
            agent.velocity = transform.rotation * new Vector3(0, 4, 0);
        }
    }
    public virtual void Chasing(float v)
    {
        float a = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        float b = anim.GetCurrentAnimatorStateInfo(0).length;
        anim.SetFloat("IdleRun", 1);
        anim.SetInteger("State", 1);
        if (((a % b) / b) % 0.5f < 0.1f || ((a % b) / b) % 0.5f > 0.35f)
        {
            agent.speed = 0.1f;
        }
        else if (((a % b) / b) % 0.5f >= 0.1f && ((a % b) / b) % 0.5f <= 0.35f)
        {
            agent.speed = (0.35f- ((a % b) / b) % 0.5f) *v *Random.Range(150, 200);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Jump_Chase") == true)
        {
            if (((a % b) / b) % 0.5f >= 0.33f && ((a % b) / b) % 0.5f <= 0.34f)
            {
                audioSource.PlayOneShot(Action[3], 0.06f);
            }
        }
        if (agent.isOnNavMesh)
        {
            agent.SetDestination(PlayerTransform.transform.position);
        }
        else
        {
            agent.velocity = transform.rotation * new Vector3(0, 4, 0);
        }
    }
    public virtual void Stop()
    {
        anim.SetInteger("State", 0);
        agent.speed = 0;
    }
    public virtual void Escape()
    {

        Vector3 dir = transform.position - PlayerTransform.transform.position;
        agent.speed = 4f;
        agent.SetDestination(transform.position + dir * 3f);
    }
    float SkillIdle = 0;
    bool TwoStage = false;
    public GameObject carrot;
    public virtual void AIFunction()
    {
        Vector3 playerPos = PlayerTransform.transform.position;
        Vector3 MyPos = transform.position;
        Vector3 CenterPos = Center.transform.position;
        if (State == 0)
        {
            if (bossAblity.HP != bossAblity.ImaHP)
            {
                detectdistance += 30;
            }
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
            else if (Vector3.Distance(playerPos, MyPos) < detectdistance && Vector3.Distance(playerPos, MyPos) > 10f)
            {
                Chasing(ChasingSpeed);
            }
            else if (Vector3.Distance(playerPos, MyPos) <= 10f)
            {
                if (TwoStage == true)
                {
                    int AttackState = Random.Range(1, 11);
                    if (AttackState < 10)
                    {
                        State = 9;
                        idle_CD = 0;
                    }
                    else
                    {
                        State = 2;
                        idle_CD = 0;
                    }
                }
                else
                {
                    State = 2;
                    idle_CD = 0;
                }
            }
        }          
        if (State == 2)
        {
            if (agent.speed > 5)
            {
                agent.velocity = transform.rotation * new Vector3(0, 0, 3);
            }
             agent.speed = 0;
            if (bossAblity.HP / bossAblity.MaxHP < 0.5f && TwoStage == false)
            {
                State = 7;
             
            }
            else
            {
                if (Vector3.Distance(playerPos, MyPos) < 8)
                {
                    Attack1(-2f * (10 - Vector3.Distance(playerPos, MyPos)));

                }
                else if (Vector3.Distance(playerPos, MyPos) >= 8 && Vector3.Distance(playerPos, MyPos) < 15f)
                {
                    Attack1(1.1f * (Vector3.Distance(playerPos, MyPos)-15));
                }

                if (anim.GetBool("AttackEnd") == true)
                {
                    AttackRandom = Random.Range(1, 11);
                    idle_CD = 0;
                    State = 3;
                }
                if (Vector3.Distance(playerPos, MyPos) >= 15f)
                {
                    State = 1;
                }
            }
           

        }
        if (State == 3)
        {
            AttackFunction(0.1f,0.4f,0.9f,1.2f,1.8f);
        }
        if (State == 4)
        {
            SkillIdle += Time.deltaTime;
            if (skills[0].skillstate == 1)
            {
                StartCoroutine(QuakeSkill());
                SkillUseFunction(skills[0].skillname, skills[0].cooldown);

            }
            else if(skills[0].cooldown - skills[0].ActiveTimeLeft < skills[0].skilltime)
            {
                if (SkillIdle > 1.7f)
                {
                    RotateFunction();
                }
                anim.SetInteger("State", 3);
            }
            else if(anim.GetInteger("State")==3)
            {
                anim.SetInteger("State", 0);
                RotateFunction();
                agent.angularSpeed = 500f;
                if (SkillIdle > 4)
                {

                    if (TwoStage == true)
                    {
                        int AttackState = Random.Range(1, 11);
                        if (AttackState < 10)
                        {
                            State = 9;
                            idle_CD = 0;
                        }
                        else
                        {
                            State = 2;
                            idle_CD = 0;
                        }
                    }
                    else
                    {
                        State = 2;
                        idle_CD = 0;
                    }
                }
            }
            else
            {
                RotateFunction();
                anim.SetInteger("State", 0);
                if (SkillIdle > 2f)
                {
                    if (TwoStage == true)
                    {
                        int AttackState = Random.Range(1, 11);
                        if (AttackState < 10)
                        {
                            State = 9;
                            idle_CD = 0;
                        }
                        else
                        {
                            State = 2;
                            idle_CD = 0;
                        }
                    }
                    else
                    {
                        State = 2;
                        idle_CD = 0;
                    }
                }
            }
        }
        if (State == 5)
        {
            SkillIdle += Time.deltaTime;
            if (skills[2].skillstate == 1)
            {
                StartCoroutine(MonsterSummon());
                SkillUseFunction(skills[2].skillname, skills[2].cooldown);
            }
            else if (skills[2].cooldown - skills[2].ActiveTimeLeft < skills[2].skilltime)
            {
                JumpToCenter();
                anim.SetInteger("State", 3);
                RotateFunction();
            }
            else if(anim.GetInteger("State") == 3)
            {
                RotateFunction();
                anim.SetInteger("State", 0);
                agent.angularSpeed = 500f;
                if (SkillIdle > 1.5f&& SkillIdle < 3f)
                {

                    agent.acceleration = 20f;
                }
                else if (SkillIdle > 4f)
                {
                    if (TwoStage == true)
                    {
                        int AttackState = Random.Range(1, 11);
                        if (AttackState < 10)
                        {
                            State = 9;
                            idle_CD = 0;
                            agent.acceleration = 20f;
                        }
                        else
                        {
                            State = 2;
                            idle_CD = 0;
                            agent.acceleration = 20f;
                        }
                    }
                    else
                    {
                        State = 2;
                        idle_CD = 0;
                        agent.acceleration = 20f;
                    }

                }
            }
            else
            {
                RotateFunction();
                anim.SetInteger("State", 0);
                if (SkillIdle > 2f)
                {
                    if (TwoStage == true)
                    {
                        int AttackState = Random.Range(1, 11);
                        if (AttackState < 10)
                        {
                            State = 9;
                            idle_CD = 0;
                            agent.acceleration = 20f;
                        }
                        else
                        {
                            State = 2;
                            idle_CD = 0;
                            agent.acceleration = 20f;
                        }
                    }
                    else
                    {
                        State = 2;
                        idle_CD = 0;
                        agent.acceleration = 20f;
                    }
                }
            }
        }
        if (State == 6)
        {
            SkillIdle += Time.deltaTime;
            if (skills[1].skillstate == 1)
            {
                StartCoroutine(CottonSkill());
                SkillUseFunction(skills[1].skillname, skills[1].cooldown);
            }
            else if (skills[1].cooldown - skills[1].ActiveTimeLeft < skills[1].skilltime)
            {
                anim.SetInteger("State", 4);
                RotateFunction();
            }
            else if(anim.GetInteger("State") == 4)
            {
                RotateFunction();
                anim.SetInteger("State", 0);
                if (SkillIdle > 4f)
                {

                    if (TwoStage == true)
                    {
                        int AttackState = Random.Range(1, 11);
                        if (AttackState < 10)
                        {
                            State = 9;
                            idle_CD = 0;
                        }
                        else
                        {
                            State = 2;
                            idle_CD = 0;
                        }
                    }
                    else
                    {
                        State = 2;
                        idle_CD = 0;
                    }
                }
            }
            else
            {
                RotateFunction();
                anim.SetInteger("State", 0);
                if (SkillIdle > 2f)
                {
                    if (TwoStage == true)
                    {
                        int AttackState = Random.Range(1, 11);
                        if (AttackState < 10)
                        {
                            State = 9;
                            idle_CD = 0;
                        }
                        else
                        {
                            State = 2;
                            idle_CD = 0;
                        }
                    }
                    else
                    {
                        State = 2;
                        idle_CD = 0;
                    }
                }
            }
        }
        if (State == 7)
        {
            StartCoroutine(TwoStageFunction());
            State = 8;
        }
        if(State == 8)
        {
            JumpToCenter();
        }
        if (State == 9)
        {
            if (agent.speed > 5)
            {
                agent.velocity = transform.rotation * new Vector3(0, 0, 3);
            }
            agent.speed = 0;


            if (Vector3.Distance(playerPos, MyPos) < 6)
            {
                Attack2(-1f * (10 - Vector3.Distance(playerPos, MyPos)));

            }
            else if (Vector3.Distance(playerPos, MyPos) >= 6 && Vector3.Distance(playerPos, MyPos) < 15f)
            {
                Attack2(3f * (Vector3.Distance(playerPos, MyPos)-6));
            }

            if (anim.GetBool("AttackEnd") == true)
            {
                AttackRandom = Random.Range(1, 11);
                idle_CD = 0;
                State = 10;
            }
            if (Vector3.Distance(playerPos, MyPos) >= 15f)
            {
                State = 1;
            }



        }
        if (State == 10)
        {
            Attack2Function(0.1f, 0.4f, 0.8f, 1.26f, 1.72f,2.05f,2.83f,3.25f,1.5f);
        }
    }
    private IEnumerator TwoStageFunction()
    {
        anim.SetFloat("State", 0);
        yield return new WaitForSeconds(0.4f);
        TwoStage = true;
        agent.acceleration = 20;
        float t=0;
        bossAblity.AS = 1.4f;
        yield return new WaitForSeconds(0.4f);
        carrot.SetActive(true);
        while (true)
        {
            RotateFunction();
            t += Time.deltaTime;
            if (t > 2.4)
            {
                agent.acceleration = 20;
                State = 2;
                anim.SetFloat("State", 2);
                yield break;
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
    public void RotateFunction()
    {
        float faceAngle;
        faceAngle = Mathf.Atan2(PlayerTransform.transform.position.x - transform.position.x, PlayerTransform.transform.position.z - transform.position.z) * Mathf.Rad2Deg;
        Quaternion targetrotatoin = Quaternion.Euler(transform.eulerAngles.x,faceAngle,transform.eulerAngles.z);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetrotatoin, 0.1f);
    }
    public virtual void Attack1(float PosAdjust)
    {

        agent.angularSpeed = 0f;
        agent.velocity = Quaternion.Euler(transform.eulerAngles) * Vector3.forward*PosAdjust;
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
    public virtual void Attack2(float PosAdjust)
    {

        agent.angularSpeed = 0f;
        agent.velocity = Quaternion.Euler(transform.eulerAngles) * Vector3.forward * PosAdjust;
        if (agent.isOnNavMesh)
        {
            agent.SetDestination(PlayerTransform.transform.position);
        }
        else
        {
            agent.velocity = transform.rotation * new Vector3(0, 4, 0);
        }

        anim.SetInteger("State", 5);
        anim.SetBool("AttackEnd", true);
    }
    float idle_CD = 0;
    int AttackRandom;
    public void AttackFunction(float AttackDelay,float CdActiveTime,float CdInactiveTime,float skilltime,float idletime)
    {
        Vector3 playerPos = PlayerTransform.transform.position;
        Vector3 MyPos = transform.position;
        Vector3 CenterPos = Center.transform.position;
        idle_CD += Time.deltaTime;
        if (idle_CD > AttackDelay / AS && idle_CD < AttackDelay / AS + 0.02f)
        {
            anim.SetBool("AttackEnd", false);
        }
        else if (idle_CD > CdActiveTime / AS && idle_CD < CdActiveTime / AS + 0.02f)
        {
            TriggerCollider.enabled = true;
        }
        else if (idle_CD > CdActiveTime / AS+0.2f && idle_CD < CdActiveTime / AS + 0.22f)
        {
            audioSource.PlayOneShot(Action[0], 0.22f);
            idle_CD += 0.02f;
        }
        else if (idle_CD > CdInactiveTime / AS && idle_CD < CdInactiveTime / AS + 0.02f)
        {
            idle_CD += 0.02f;
            anim.SetBool("AttackEnd", true);
            TriggerCollider.enabled = false;

        }
        else if (idle_CD > skilltime / AS && idle_CD < (skilltime + idletime) / AS)
        {
            //Debug.Log("123");
            RotateFunction();
            if (Vector3.Distance(MyPos, CenterPos) < 14 && Vector3.Distance(playerPos, MyPos) < 12)
            {
                if (AttackRandom <= 2)
                {
                    agent.angularSpeed = 500f;
                    State = 5;
                    SkillIdle = 0;
                }
                else if (AttackRandom <= 5 && AttackRandom > 2)
                {
                    agent.angularSpeed = 500f;
                    State = 4;
                    SkillIdle = 0;
                }
                else if(AttackRandom <= 9 && AttackRandom > 5)
                {
                    agent.angularSpeed = 500f;
                    State = 6;
                    SkillIdle = 0;
                }
                else
                {
                    Stop();
                }
            }
            else if (Vector3.Distance(MyPos, CenterPos) < 14 && Vector3.Distance(playerPos, MyPos) >= 12)
            {
                {
                    if (AttackRandom <= 2)
                    {
                        agent.angularSpeed = 500f;
                        State = 5;
                        SkillIdle = 0;
                    }
                    else if (AttackRandom <= 7 && AttackRandom > 2)
                    {
                        agent.angularSpeed = 500f;
                        State = 4;
                        SkillIdle = 0;
                    }
                    else if (AttackRandom <= 9 && AttackRandom > 7)
                    {
                        agent.angularSpeed = 500f;
                        State = 6;
                        SkillIdle = 0;
                    }
                    else
                    {
                        Stop();
                    }
                }
            }
            else if (Vector3.Distance(MyPos, CenterPos) > 14 && Vector3.Distance(playerPos, MyPos) >= 12)
            {
                {
                    if (AttackRandom <= 3)
                    {
                        agent.angularSpeed = 500f;
                        State = 5;
                        SkillIdle = 0;
                    }
                    else if (AttackRandom <= 7 && AttackRandom > 3)
                    {
                        agent.angularSpeed = 500f;
                        State = 4;
                        SkillIdle = 0;
                    }
                    else
                    {
                        Stop();
                    }
                }
            }
            else
            {
                Stop();
            }
        }
        else if (idle_CD > (skilltime + idletime) / AS)
        {
            agent.angularSpeed = 500f;
            if (TwoStage == true)
            {
                int AttackState = Random.Range(1, 11);
                if (AttackState < 10)
                {
                    State = 9;
                    idle_CD = 0;
                }
                else
                {
                    State = 2;
                    idle_CD = 0;
                }
            }
            else
            {
                State = 2;
                idle_CD = 0;
            }
        }

    }
    public ParticleSystem CarrotStrike3;
    public GameObject[] CarrotStrike12;
    public void Attack2Function(float AttackDelay, float CdActiveTime1, float CdInactiveTime1, float CdActiveTime2, float CdInactiveTime2, float CdActiveTime3, float CdInactiveTime3, float skilltime, float idletime)
    {
        Vector3 playerPos = PlayerTransform.transform.position;
        Vector3 MyPos = transform.position;
        Vector3 CenterPos = Center.transform.position;
        idle_CD += Time.deltaTime;
        if (idle_CD > AttackDelay / AS && idle_CD < AttackDelay / AS + 0.02f)
        {
            anim.SetBool("AttackEnd", false);
        }
        else if (idle_CD > CdActiveTime1 / AS && idle_CD < CdActiveTime1 / AS + 0.02f)
        {
            audioSource.PlayOneShot(Action[5], 0.12f);
            GameObject Strike1 = Instantiate(CarrotStrike12[0], carrot.transform, false);
            Strike1.transform.SetParent(null, true);
            Destroy(Strike1, 1f);
            TriggerCollider2.enabled = true;
            TriggerCollider3.enabled = true;
        }
        else if (idle_CD > CdInactiveTime1 / AS && idle_CD < CdInactiveTime1 / AS + 0.02f)
        {
            idle_CD += 0.02f;
            TriggerCollider2.enabled = false;
            TriggerCollider3.enabled = false;
        }
        else if (idle_CD > CdActiveTime2 / AS && idle_CD < CdActiveTime2 / AS + 0.02f)
        {
            audioSource.PlayOneShot(Action[5], 0.12f);
            GameObject Strike1 = Instantiate(CarrotStrike12[1], carrot.transform,false);
            Strike1.transform.SetParent(null,true);
            Destroy(Strike1, 1f);
            TriggerCollider2.enabled = true;
        }
        else if (idle_CD > CdInactiveTime2 / AS && idle_CD < CdInactiveTime2 / AS + 0.02f)
        {
            idle_CD += 0.02f;
            TriggerCollider2.enabled = false;
        }
        else if (idle_CD > CdInactiveTime2 / AS && idle_CD < CdActiveTime3 / AS )
        {
            RotateFunction();

        }
        else if (idle_CD > CdActiveTime3 / AS && idle_CD < CdActiveTime3 / AS + 0.02f)
        {
            audioSource.PlayOneShot(Action[6], 0.12f);
            CarrotStrike3.Play();
            agent.velocity = Quaternion.Euler(transform.eulerAngles) * Vector3.forward * (1.8f* Vector3.Distance(playerPos, MyPos)+3);
            TriggerCollider2.enabled = true;
        }
        else if (idle_CD > CdInactiveTime3 / AS && idle_CD < CdInactiveTime3 / AS + 0.02f)
        {
            idle_CD += 0.02f;
            anim.SetBool("AttackEnd", true);
            TriggerCollider2.enabled = false;

        }
        else if (idle_CD > skilltime / AS && idle_CD < (skilltime + idletime) / AS)
        {
            RotateFunction();
            if (Vector3.Distance(MyPos, CenterPos) < 14 && Vector3.Distance(playerPos, MyPos) < 12)
            {
                if (AttackRandom <= 2)
                {
                    agent.angularSpeed = 500f;
                    State = 5;
                    SkillIdle = 0;
                }
                else if (AttackRandom <= 5 && AttackRandom > 2)
                {
                    agent.angularSpeed = 500f;
                    State = 4;
                    SkillIdle = 0;
                }
                else if (AttackRandom <= 9 && AttackRandom > 5)
                {
                    agent.angularSpeed = 500f;
                    State = 6;
                    SkillIdle = 0;
                }
                else
                {
                    Stop();
                }
            }
            else if (Vector3.Distance(MyPos, CenterPos) < 14 && Vector3.Distance(playerPos, MyPos) >= 12)
            {
                {
                    if (AttackRandom <= 2)
                    {
                        agent.angularSpeed = 500f;
                        State = 5;
                        SkillIdle = 0;
                    }
                    else if (AttackRandom <= 7 && AttackRandom > 2)
                    {
                        agent.angularSpeed = 500f;
                        State = 4;
                        SkillIdle = 0;
                    }
                    else if (AttackRandom <= 9 && AttackRandom > 7)
                    {
                        agent.angularSpeed = 500f;
                        State = 6;
                        SkillIdle = 0;
                    }
                    else
                    {
                        Stop();
                    }
                }
            }
            else if (Vector3.Distance(MyPos, CenterPos) > 14 && Vector3.Distance(playerPos, MyPos) >= 12)
            {
                {
                    if (AttackRandom <= 3)
                    {
                        agent.angularSpeed = 500f;
                        State = 5;
                        SkillIdle = 0;
                    }
                    else if (AttackRandom <= 7 && AttackRandom > 3)
                    {
                        agent.angularSpeed = 500f;
                        State = 4;
                        SkillIdle = 0;
                    }
                    else
                    {
                        Stop();
                    }
                }
            }
            else
            {
                Stop();
            }
        }
        else if (idle_CD > (skilltime + idletime) / AS)
        {
            agent.angularSpeed = 500f;
            if (TwoStage == true)
            {
                int AttackState = Random.Range(1, 11);
                if (AttackState < 10)
                {
                    State = 9;
                    idle_CD = 0;
                }
                else
                {
                    State = 2;
                    idle_CD = 0;
                }
            }          
            else
            {
                State = 2;
                idle_CD = 0;
            }
        }

    }
}
