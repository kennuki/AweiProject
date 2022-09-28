using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public static int IsDialoged = 0;
    public Animator anim1;
    CharacterController controller;
    public Shoot shoot;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        StartCoroutine(RandomIdle());
        StartCoroutine(NormalAttackFirstStrike());
        StartCoroutine(DashFunction());
    }
    void FixedUpdate()
    {

        AccelerateFunction();
        if (Cursor.visible == false)
        {
            MoveFunction();
        }
        else
        {
            move = Vector3.zero;
            anim1.SetBool("Run", false);
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
        //GunShotRotation();
    }

    float speed = 6f;
    public static float imaangle;
    Vector3 move;
    public Vector3 dir,DirCache;

    float h;
    float j;
    private void MoveFunction()
    {
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

            if (Input.GetKey(KeyCode.Mouse0)&&Cursor.visible==false)
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
                    anim1.SetBool("Run", true);
                    anim1.SetBool("Idle", false);
                    anim1.SetInteger("IdleState", 1);
                }
                else
                {
                    anim1.SetBool("Run", false);
                }
            }

        }
        if(anim1.GetBool("Dash") == false)
        {
            if (anim1.GetBool("Attack") == false && anim1.GetBool("Attack2") == false && anim1.GetBool("Attack3") == false && anim1.GetBool("Dash") == false)
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
                if (Input.GetKey(KeyCode.LeftShift)&&Cursor.visible==false)
                {
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
                    move = dir * speed * 1.3f*AS;
                    yield return new WaitForSeconds(0.45f/AS);
                    move = dir * speed;
                    anim1.SetBool("Dash", false);
                    if (dir.magnitude != 0)
                    {
                        anim1.SetBool("Run", true);
                    }

                    yield return new WaitForSeconds(2f/AS);
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
    private void AccelerateFunction()
    {
        if (anim1.GetLayerWeight(1) < 0.5)
        {
            speed = 6f;
            anim1.SetFloat("RunSpeed", 1f);
        }
        if (anim1.GetLayerWeight(1) >= 0.5)
        {
            speed = 4.8f;
            anim1.SetFloat("RunSpeed", 0.8f);
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
            if (anim1.GetBool("Dash") == true)
            {
                yield return new WaitForSeconds(0.6f/AS);
            }
            SecondStrikeTrigger = false;
            if (Input.GetKey(KeyCode.Mouse0) && anim1.GetBool("Attack2") == false && anim1.GetBool("Attack3") == false && Cursor.visible == false)
            {

                ShootAllow = 1;
                anim1.SetBool("Attack", true);
                anim1.SetInteger("IdleState", 1);
                anim1.SetBool("Run", false);
                StartCoroutine(GunSparkle(0));
                SecondStrikeTrigger = true;
                anim1.SetLayerWeight(2, 1);
                StartCoroutine(Attack1Move(10/AS,4.5f*AS, 25f/AS));
                StartCoroutine(NormalAttackSecondStrike(1,0.7f/AS));
                for(float i=0; i<=0.9f/AS; i += 0.1f/AS)
                {
                    if (anim1.GetBool("Dash") == true)
                    {
                        i = 0.9f / AS;
                    }
                    yield return new WaitForSeconds(0.1f/AS);
                }
            }
            else if (anim1.GetBool("Attack") == true)
            {
                anim1.SetBool("Attack", false);
            }
            yield return new WaitForEndOfFrame();
        }

    }
    private IEnumerator NormalAttackSecondStrike(int a,float waittime)
    {
        if(true)
        {
            if (anim1.GetBool("Dash") == true)
            {
                yield break;
            }
            yield return new WaitForSeconds(waittime);
        }
        while (true)
        {
            if (anim1.GetBool("Dash") == true)
            {
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
                    //StartCoroutine(Attack1Move(15f/AS,4f*AS, 0.6f/AS));
                    StartCoroutine(NormalAttackThirdStrike(1,0.5f/AS));
                    ThirdStrikeTrigger = true;
                    for (float i = 0; i <= 0.9f / AS; i += 0.1f / AS)
                    {
                        if (anim1.GetBool("Dash") == true)
                        {
                            i = 0.9f / AS;
                        }
                        yield return new WaitForSeconds(0.1f / AS);
                    }
                }
            }
            else if (anim1.GetBool("Attack2") == true)
            {
                anim1.SetBool("Attack2", false);
                ThirdStrikeTrigger = false;
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
        if (true)
        {
            if (anim1.GetBool("Dash") == true)
            {
                yield break;
            }
            yield return new WaitForSeconds(waittime);
        }
        while (true)
        {
            if (anim1.GetBool("Dash") == true)
            {
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
                    for (float i = 0; i <= 1.1f / AS; i += 0.1f / AS)
                    {
                        if (anim1.GetBool("Dash") == true)
                        {
                            i = 1.1f / AS;
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
        for(int i =0;i<=frame;i++)
        {
            if (dir.magnitude > 0)
            DirCache = dir.normalized;
            if(i>offsetframe)
            controller.Move(DirCache * speed *Time.deltaTime);
            yield return new WaitForSeconds(0.01f);
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
                yield return new WaitForEndOfFrame();
            }
            else if (t > 0.2f && t <= duration)
            {
                yield return new WaitForEndOfFrame();
            }
            else if (t > duration)
            {
                alphaclip -= 0.005f * AS;
                gun_m[i].SetFloat("Alpha", alphaclip);
                if (alphaclip <= 0)
                {
                    yield break;
                }
                yield return new WaitForEndOfFrame();
            }
        }
    }
    public ParticleSystem BulletShell;
    public ParticleSystem DashTrail;
    public GameObject Leftgun;



}
