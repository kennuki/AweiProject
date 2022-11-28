using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public static int OnMission = 0;
    public static int IsDialoged = 0;
    public static bool ActionProhibit = false;
    public static bool ActionProhibitWithoutMove = false;
    public Animator anim1;
    public static bool AttackGet = false;
    public bool ThreeAttackGet = false;
    CharacterController controller;
    public Shoot shoot;
    public SkillShoot skillShoot;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        StartCoroutine(RandomIdle());
        StartCoroutine(NormalAttackFirstStrike());
        StartCoroutine(DashFunction());
        StartCoroutine(AccelerateSkill());
        StartCoroutine(KickSkill(skills[2].skilltime));
        StartCoroutine(Skill1(skills[3].skilltime));
        gun_m[3].SetFloat("Alpha", 0);
        SkillMaskImageFind();
    }
    void FixedUpdate()
    {
        SkillCoolDownFunction();
        AccelerateFunction();
        if (Cursor.visible == false && ActionProhibit == false)
        {
            MoveFunction();
        }
        else if(ActionProhibitWithoutMove == true)
        {
            MoveFunction();
        }
        else
        {
            move = Vector3.zero;
            anim1.SetBool("Run", false);
        }
        if (anim1.GetInteger("Skill") == 3)
        {
            MoveFunction();
        }
        JumpAndGravityFunction();
        if (transform.position.y < -50)
        {
            transform.position = new Vector3(47.5f, 10, -22);
        }
        move.y = -v1 * 15 * Time.deltaTime;
        controller.Move(move * Time.deltaTime);
    }
    private void Update()
    {
        AS = anim1.GetFloat("AS");
        if (Input.GetKey(KeyCode.Mouse0)&&AttackGet==true)
        {
            anim1.SetInteger("AttackRepeat", 1);
        }
        else
        {
            anim1.SetInteger("AttackRepeat", 0);
        }
    }
    public Vector3[] SceneStartPosition;

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        controller.enabled = false;
        transform.position = SceneStartPosition[SceneManager.GetActiveScene().buildIndex];
        controller.enabled = true;
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
    }
    public static float speed = 6f;
    public static float imaangle;
    Vector3 move;
    public Vector3 dir,DirCache;
    float h;
    float j;
    float a = 0;
    private void MoveFunction()
    {
        a += Time.deltaTime;
        if (a > 0 && a < 1)
        {
            anim1.SetBool("RunToIdle", false);
        }
        h = Input.GetAxis("Horizontal");
        j = Input.GetAxis("Vertical");
        imaangle = transform.eulerAngles.y * Mathf.Deg2Rad;
        float jx = j * Mathf.Sin(imaangle);
        float jz = j * Mathf.Cos(imaangle);
        float hx = h * Mathf.Sin(imaangle + 0.5f * Mathf.PI);
        float hz = h * Mathf.Cos(imaangle + 0.5f * Mathf.PI);
        dir = new Vector3(jx + hx, dir.y, jz + hz);
        if (j != 0 && h != 0)
        {
            dir /= Mathf.Sqrt(2);
        }
        if (anim1.GetBool("Dash") == true)
        {
            dir = Vector3.Normalize(dir);
        }
        else if (anim1.GetBool("Dash") == false)
        {

            if (Input.GetKey(KeyCode.Mouse0) && Cursor.visible == false && AttackGet == true || anim1.GetInteger("SkillState") == 2)
            {
                j = 1;
                jx = j * Mathf.Sin(imaangle);
                jz = j * Mathf.Cos(imaangle);
                dir = new Vector3(jx, dir.y, jz);
                h = 0;
                j = 0;
            }
            else
            {
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
                {
                    anim1.SetBool("RunToIdle", false);
                    a = 1;
                    anim1.SetBool("Run", true);
                    anim1.SetBool("Idle", false);
                    anim1.SetInteger("IdleState", 1);
                }
                else
                {
                    if (a >= 1)
                    {
                        anim1.SetBool("RunToIdle", true);
                        a = -0.2f;
                    }
                    else
                    {
                        anim1.SetBool("Run", false);
                    }

                }
            }

        }
        if(anim1.GetBool("Dash") == false)
        {
            if (anim1.GetBool("Attack") == false && anim1.GetBool("Attack2") == false && anim1.GetBool("Attack3") == false && anim1.GetBool("Dash") == false&&anim1.GetInteger("SkillState") != 2)
            {
                move = dir * speed;
            }
            else
            {
                move = dir * 0;
            }
        }

    }

    private IEnumerator DashFunction()
    {
        while (true)
        {
            if (j != 0 || h != 0|| Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            {
                if (Input.GetKey(KeyCode.LeftShift)&&Cursor.visible==false&&skills[0].skillstate==1)
                {
                    SkillUseFunction(skills[0].skillname, skills[0].cooldown);
                    ActionProhibit = false;
                    StartCoroutine(BodySparkle(2, 0.55f));
                    DashTrail.Play();
                    anim1.SetBool("Dash", true);
                    anim1.SetBool("Attack", false);
                    anim1.SetBool("Attack2", false);
                    anim1.SetBool("Attack3", false);
                    anim1.SetBool("Run", false);
                    shoot.ShootBreak = true;
                    SecondStrikeTrigger = false;
                    ThirdStrikeTrigger = false;
                    ShootAllow = 0;
                    anim1.SetLayerWeight(2, 0);
                    h = Input.GetAxis("Horizontal");
                    j = Input.GetAxis("Vertical");
                    imaangle = transform.eulerAngles.y * Mathf.Deg2Rad;
                    float jx = j * Mathf.Sin(imaangle);
                    float jz = j * Mathf.Cos(imaangle);
                    float hx = h * Mathf.Sin(imaangle + 0.5f * Mathf.PI);
                    float hz = h * Mathf.Cos(imaangle + 0.5f * Mathf.PI);
                    dir = new Vector3(jx + hx, dir.y, jz + hz);
                    dir = Vector3.Normalize(dir);
                    move = dir * (5 + speed * 0.2f * BonusSpeed) * 1.7f;
                    yield return new WaitForSeconds(0.45f/AS);
                    move = dir * speed * BonusSpeed;
                    anim1.SetBool("Dash", false);
                    if (dir.magnitude != 0)
                    {
                        anim1.SetBool("Run", true);
                    }
                    yield return new WaitForSeconds(skills[0].cooldown/AS);
                    skills[0].skillstate = 1;
                }
            }
            yield return new WaitForEndOfFrame();
        }

    }

    
    float g = 0.8f, v1 = 0, v2 = 0, up = 15.5f;
    private void JumpAndGravityFunction()
    {
        if (!controller.isGrounded)
        {
            v2 = 0;
            v1 = v1 + g;
            move.y = -v1 * 15 * Time.deltaTime;

        }
      
    }
    private IEnumerator RandomIdle()
    {
        int random = 6;
        while (true)
        {
            float waitsecond = 2;           
            if (true)
            {
                anim1.SetInteger("IdleState", 1);
                if (anim1.GetInteger("IdleState") == 1)
                {
                    int x = Random.Range(random, random+3);
                    if (x == 4)
                    {
                        anim1.SetInteger("IdleState", 2);
                        waitsecond = 4f;
                        random = 6;
                    }
                    else
                    {
                        random--;
                        random = Mathf.Clamp(random, 2, 6);
                    }
                }
            }
            yield return new WaitForSeconds(waitsecond);
        }
    }

    public float BonusSpeed = 1;
    private void AccelerateFunction()
    {
        if (skills[1].cooldown - skills[1].ActiveTimeLeft < skills[1].skilltime)
        {
            BonusSpeed = 1.7f;
        }
        else if (skills[1].cooldown - skills[1].ActiveTimeLeft >= skills[1].skilltime)
        {
            BonusSpeed = 1;
        }
        anim1.SetFloat("AS", 1.2f * BonusSpeed);
        if (anim1.GetLayerWeight(1) < 0.5)
        {
            speed = 6.2f * BonusSpeed;
            anim1.SetFloat("RunSpeed", 1.1f*BonusSpeed);
        }
        if (anim1.GetLayerWeight(1) >= 0.5)
        {
            speed = 4.8f * BonusSpeed;
            anim1.SetFloat("RunSpeed", 0.8f*BonusSpeed);
        }
    }
    private IEnumerator AccelerateSkill()
    {
        while (true)
        {
            int state = 0;
            if (Input.GetKey(KeyCode.E)&&skills[1].skillstate == 1)
            {
                StartCoroutine(BodyAccelerateSparkle(3, skills[1].skilltime));
                state = 1;
            }
            while(state == 1)
            {
                SkillUseFunction(skills[1].skillname, skills[1].cooldown);
                state = 0;
            }
            yield return new WaitForEndOfFrame();
        }
    }
    public GameObject FootCollider;
    public bool KickAdvance = true;
    private IEnumerator KickSkill(float skilltime)
    {
        while (true)
        {
            if (skills[2].skillstate == 1 && Input.GetKey(KeyCode.Mouse1)&&anim1.GetInteger("SkillState")<3)
            {
                AttackGet = false;
                SkillUseFunction(skills[2].skillname, skills[2].cooldown);
                anim1.SetInteger("SkillState", 2);
                anim1.SetInteger("Skill", 2);
                FootCollider.SetActive(true);
                StartCoroutine(Attack1Move(0, 1f * AS, 20f / AS));
                StartCoroutine(Attack1Move(20/ AS, -5f * AS, 80f / AS));
                if(KickAdvance == true)
                {
                    anim1.SetLayerWeight(1, 1);
                    anim1.SetFloat("KickBlend", 1);
                    yield return new WaitForSeconds(skilltime / AS*0.6f);
                    skillShoot.ShootFunction(skillShoot.ShootRight, skillShoot.bullet2, skillShoot.ShootEffectRight, skillShoot.ShootEffect);
                    yield return new WaitForSeconds(skilltime / AS * 0.4f);
                }
                else
                {
                    yield return new WaitForSeconds(skilltime / AS);
                }
                FootCollider.SetActive(false);
                AttackGet = true;
                if (anim1.GetInteger("Skill") == 2)
                {
                    anim1.SetInteger("SkillState", 0);
                    anim1.SetInteger("Skill", 0);
                }

            }
            yield return new WaitForEndOfFrame();
        }

    }
    private IEnumerator Skill1(float skilltime)
    {
        while (true)
        {
            if(Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W))
            {
                anim1.SetBool("Run", true);
            }
            if (skills[3].skillstate == 1 && Input.GetKey(KeyCode.Q))
            {
                AttackGet = false;
                SkillUseFunction(skills[3].skillname, skills[3].cooldown);
                anim1.SetInteger("SkillState", 3);
                anim1.SetInteger("Skill", 3);
                anim1.SetLayerWeight(1, 1);
                yield return new WaitForSeconds((skilltime*2 / 13)/AS);
                for (int i = 0; i < 5; i++)
                {
                    if (anim1.GetBool("Dash") == false)
                    {
                        skillShoot.ShootFunction(skillShoot.ShootRight, skillShoot.bullet,skillShoot.ShootEffectRight,skillShoot.ShootEffect);
                        yield return new WaitForSeconds((skilltime / 13) / AS);
                        skillShoot.ShootFunction(skillShoot.ShootLeft, skillShoot.bullet,skillShoot.ShootEffectLeft, skillShoot.ShootEffect);
                        yield return new WaitForSeconds((skilltime / 13) / AS);
                    }
                }
                AttackGet = true;
                anim1.SetInteger("SkillState", 0);
                anim1.SetInteger("Skill", 0);
            }
            yield return new WaitForEndOfFrame();
        }

    }
    [System.Serializable]
    public class Skill
    {
        public string skillname;
        public float cooldown;
        public float ActiveTimeLeft = 0;
        public int skillstate = 0;      // skillstate 0 = inactive      1 = active can use  2 = active cannot use
        public float skilltime;
        public GameObject CoolDowmMask;
        public Image CoolDownMaskImage { get; set; }
        public float CoolDowmMaskFillAmount { get; set; }
    }
    public Skill[] skills;
    private void SkillMaskImageFind()
    {
        foreach(Skill skill in skills)
        {
            skill.CoolDownMaskImage = skill.CoolDowmMask.GetComponent<Image>();
        }
    }
    public void SkillCoolDownFunction()
    {
        if (anim1.GetBool("Dash") == true)
        {
            anim1.SetInteger("SkillState", 0);
        }
        foreach(Skill skill in skills)
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
                    skill.CoolDowmMaskFillAmount = Mathf.Clamp(skill.ActiveTimeLeft / skill.cooldown, 0, 1);
                    skill.CoolDownMaskImage.fillAmount = skill.CoolDowmMaskFillAmount;
                    skill.skillstate = 2;
                }
            }
        }
    }
    public void SkillUseFunction(string skillname_,float cooldown_)
    {
        if(Cursor.visible == false)
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
    }






    public Material[] gun_m;
    public float AS = 1.5f;
    bool SecondStrikeTrigger = false;
    bool ThirdStrikeTrigger = false;
    public int ShootAllow = 0;
    private IEnumerator NormalAttackFirstStrike()
    {
        while (true)
        {
            while (AttackGet == true)
            {
                if (anim1.GetBool("Dash") == true)
                {
                    yield return new WaitForSeconds(0.6f / AS);
                }
                SecondStrikeTrigger = false;
                if (Input.GetKey(KeyCode.Mouse0) && anim1.GetBool("Attack2") == false && anim1.GetBool("Attack3") == false && Cursor.visible == false && ActionProhibit == false && anim1.GetInteger("SkillState")<2)
                {
                    MemoryItemManage.TakeItemState = 1;
                    ShootAllow = 1;
                    anim1.SetBool("Attack", true);
                    anim1.SetInteger("IdleState", 1);
                    anim1.SetBool("Run", false);
                    StartCoroutine(GunSparkle(0));
                    SecondStrikeTrigger = true;
                    anim1.SetLayerWeight(2, 1);
                    StartCoroutine(Attack1Move(2 / AS, 1f * AS, 30f / AS));
                    StartCoroutine(NormalAttackSecondStrike(1, 0.7f / AS));
                    for (float i = 0; i <= 0.75f / AS; i += 0.1f / AS)
                    {
                        if (anim1.GetBool("Dash") == true)
                        {
                            i = 1f / AS;
                        }
                        yield return new WaitForSeconds(0.1f / AS);
                    }
                }
                else if (anim1.GetBool("Attack") == true)
                {
                    anim1.SetBool("Attack", false);
                    if (Input.GetKey(KeyCode.Mouse0) == false)
                    {
                        AttackGet = false;
                        yield return new WaitForSeconds(0.2f);
                        AttackGet = true;
                    }
                }
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForEndOfFrame();
        }
    }
    private IEnumerator NormalAttackSecondStrike(float a,float waittime)
    {
        if(true)
        {
            if (anim1.GetBool("Dash") == true|| anim1.GetInteger("SkillState") >= 2)
            {
                anim1.SetBool("Attack", false);
                anim1.SetBool("Attack2", false);
                anim1.SetLayerWeight(2, 0);
                yield break;
            }
            yield return new WaitForSeconds(waittime);
        }
        while (true)
        {
            if (anim1.GetBool("Dash") == true || anim1.GetInteger("SkillState") >= 2)
            {
                anim1.SetBool("Attack", false);
                anim1.SetBool("Attack2", false);
                anim1.SetLayerWeight(2, 0);
                yield break;
            }
            if (a<=0&& SecondStrikeTrigger==true)
            {
                ThirdStrikeTrigger = false;
                if (Input.GetKey(KeyCode.Mouse0)&&anim1.GetBool("Attack3") == false)
                {
                    ShootAllow = 2;
                    anim1.SetBool("Attack2", true);
                    anim1.SetLayerWeight(2, 1);
                    StartCoroutine(GunSparkle(1));
                    StartCoroutine(Attack1Move(0,2f * AS, 20f / AS));
                    StartCoroutine(NormalAttackThirdStrike(1,0.5f/AS));
                    ThirdStrikeTrigger = true;
                    for (float i = 0; i <= 0.85f / AS; i += 0.1f / AS)
                    {
                        if (anim1.GetBool("Dash") == true)
                        {
                            i = 0.8f / AS;
                        }
                        yield return new WaitForSeconds(0.1f / AS);
                    }
                }
            }
            else if (anim1.GetBool("Attack2") == true)
            {
                anim1.SetBool("Attack2", false);
                ThirdStrikeTrigger = false;
                anim1.SetLayerWeight(2, 0);
                if (Input.GetKey(KeyCode.Mouse0) == false)
                {
                    AttackGet = false;
                    yield return new WaitForSeconds(0.2f);
                    AttackGet = true;
                }
                yield break;
            }
            else if(SecondStrikeTrigger == false)
            {
                anim1.SetLayerWeight(2, 0);
                yield break;
            }
            a--;
            yield return null;
        }

    }
    private IEnumerator NormalAttackThirdStrike(int b, float waittime)
    {
        if (ThreeAttackGet == true)
        {
            if (anim1.GetBool("Dash") == true|| anim1.GetInteger("SkillState") >= 2)
            {
                anim1.SetBool("Attack2", false);
                anim1.SetBool("Attack3", false);
                anim1.SetLayerWeight(2, 0);
                yield break;
            }
            yield return new WaitForSeconds(waittime);
        }
        while (ThreeAttackGet == true)
        {
            if (anim1.GetBool("Dash") == true|| anim1.GetInteger("SkillState") >= 2)
            {
                anim1.SetBool("Attack2", false);
                anim1.SetBool("Attack3", false);
                anim1.SetLayerWeight(2, 0);
                yield break;
            }
            if (b<= 0 && ThirdStrikeTrigger == true)
            {

                if (Input.GetKey(KeyCode.Mouse0))
                {
                    ShootAllow = 3;
                    StartCoroutine(GunSparkle2(0));
                    StartCoroutine(GunSparkle2(1));                 
                    anim1.SetBool("Attack3", true);
                    anim1.SetLayerWeight(2, 1);
                    Instantiate(BulletShell, Leftgun.transform.position, Quaternion.Euler(0, 0, 0));
                    for (float i = 0; i <= 0.8f / AS; i += 0.1f / AS)
                    {
                        if (anim1.GetBool("Dash") == true)
                        {
                            i = 0.6f / AS;
                        }
                        yield return new WaitForSeconds(0.1f / AS);
                    }
                }
            }
            else if (anim1.GetBool("Attack3") == true)
            {
                anim1.SetBool("Attack3", false);
                anim1.SetLayerWeight(2, 0);
                yield break;
            }
            else if(ThirdStrikeTrigger == false&&b<1)
            {
                anim1.SetLayerWeight(2, 0);
                yield break;
            }
            b--;
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator Attack1Move(float offsetframe,float speed,float frame)
    {
        while(AttackGet == true || ActionProhibitWithoutMove == false)
        {
            for (int i = 0; i <= frame; i++)
            {
                if (dir.magnitude > 0)
                    DirCache = dir.normalized;
                if (i > offsetframe)
                    controller.Move(DirCache * speed * Time.fixedDeltaTime);
                yield return new WaitForSeconds(0.01f);
            }
            yield break;
        }
        yield break;
    }
    private IEnumerator GunSparkle(int i)
    {
        float t = 0;
        float emission = 1;
        while (true)
        {
            t+= Time.deltaTime*33*AS;
            if (t <= 8)
            {
                emission = t; 
                gun_m[i].SetColor("_EmissionColor", Vector4.Lerp(gun_m[i].GetVector("_EmissionColor"), new Vector4(0.1f, 0.5f, 0.745f, 1), 0.5f));
                gun_m[i].SetFloat("_emission", emission);
                yield return new WaitForEndOfFrame();
            }
            else if (t > 8&&t <= 12)
            {
                yield return new WaitForEndOfFrame();
            }
            else if (t>12)
            {
                emission -= 0.4f*AS;
                gun_m[i].SetColor("_EmissionColor", Vector4.Lerp(gun_m[i].GetVector("_EmissionColor"), new Vector4(0.1802243f, 0.3863149f, 0.509434f, 1), 0.5f));
                gun_m[i].SetFloat("_emission", emission);
                if(emission <= 1)
                {
                    yield break;
                }
                yield return new WaitForEndOfFrame();
            }
        }
 
    }
    private IEnumerator GunSparkle2(int i)
    {
        float t = 0;
        float emission = 1;
        while (true)
        {
            t += Time.deltaTime * 20 * AS;
            if (t <= 10)
            {
                emission = t;
                gun_m[i].SetColor("_EmissionColor", Vector4.Lerp(gun_m[i].GetVector("_EmissionColor"), new Vector4(0.1f, 0.5f, 0.745f, 1), 0.5f));
                gun_m[i].SetFloat("_emission", emission);
                yield return new WaitForEndOfFrame();
            }
            else if (t > 10 && t <= 15)
            {
                yield return new WaitForEndOfFrame();
            }   
            else if (t > 10)
            {
                emission -= 0.4f * AS;
                gun_m[i].SetColor("_EmissionColor", Vector4.Lerp(gun_m[i].GetVector("_EmissionColor"), new Vector4(0.1802243f, 0.3863149f, 0.509434f, 1), 0.5f));
                gun_m[i].SetFloat("_emission", emission);
                if (emission <= 1)
                {
                    yield break;
                }
                yield return new WaitForEndOfFrame();
            }
        }

    }
    private IEnumerator BodySparkle(int i,float duration)
    {
        float t = 0;
        float alphaclip = 0;
        yield return new WaitForSeconds(0.09f*AS);
        while (true)
        {
            t += Time.deltaTime*8 * AS;
            if (t <= 0.2f)
            {
                alphaclip = t*0.8f;              
                gun_m[i].SetFloat("Alpha", alphaclip);
                yield return new WaitForFixedUpdate();
            }
            else if (t > 0.2f && t <= duration)
            {
                yield return new WaitForFixedUpdate();
            }
            else if (t > duration)
            {
                alphaclip -= 0.005f * AS;
                gun_m[i].SetFloat("Alpha", alphaclip);
                if (alphaclip <= 0)
                {
                    yield break;
                }
                yield return new WaitForFixedUpdate();
            }
        }
    }
    private IEnumerator BodyAccelerateSparkle(int i,float duration)
    {
        float t = 0;
        float alphaclip = 0;
        while (true)
        {
            t += Time.deltaTime;
            if (t <= 0.1f)
            {
                alphaclip = t * 5f;
                gun_m[i].SetFloat("Alpha", alphaclip);
                yield return new WaitForFixedUpdate();
            }
            else if(t > 0.1f && t <= 0.15f)
            {
                yield return new WaitForFixedUpdate();
            }
            else if(t > 0.15f && t <= 0.35f)
            {
                alphaclip -= Time.deltaTime * 1.7f;
                gun_m[i].SetFloat("Alpha", alphaclip);
            }
            else if (t > duration)
            {
                alphaclip -= 4f * Time.deltaTime;
                gun_m[i].SetFloat("Alpha", alphaclip);
                if (alphaclip <= 0)
                {
                    yield break;
                }
                yield return new WaitForFixedUpdate();
            }
            yield return new WaitForFixedUpdate();


        }

    }
    public ParticleSystem BulletShell;
    public ParticleSystem DashTrail;
    public GameObject Leftgun;



}
