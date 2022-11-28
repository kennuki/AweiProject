using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterAbility : MonoBehaviour
{
    public static float Damage = 0;
    public float MaxHP = 50;
    public float MaxMP = 50;
    public float HP;
    public float MP;
    float ImaHP;
    public float AD =10;
    public float CritRate = 0.2f;
    public float AS = 1.3f;
    public float HPRecover = 0.5f;
    public float MPRecover = 2f;
    public float gametime=1f;
    public GameObject DeadEffect;
    Animator anim;
    public AudioSource audioSource;


    void Start()
    {
        anim = transform.GetComponentInChildren<Animator>();
        HP = MaxHP;
        MP = MaxMP;
        ImaHP = HP;
        StartCoroutine(dead());
    }

    // Update is called once per frame
    void Update()
    {
        AS = anim.GetFloat("AS");
        if (Input.GetKeyDown(KeyCode.T))
        {
            Time.timeScale = gametime;
        }
        HP = Mathf.Clamp(HP + HPRecover*Time.deltaTime, -1, MaxHP);
        MP = Mathf.Clamp(MP + MPRecover * Time.deltaTime, -1, MaxMP);
        HitFunction();
        if (Damage > 0)
        {
            HP -= Damage;
            Damage = 0;
        }
    }
    public Scene2trigger Scene2Trigger;
    public GameObject GameOverPanel;
    private IEnumerator dead()
    {
        while (true)
        {
            if (HP <= 0)
            {
                HP = -10000;
                anim.SetBool("Dead", true);
                DeadEffect.SetActive(true);
                Character.ActionProhibit = true;
                audioSource.Play();
                yield return new WaitForSeconds(2.5f);
                GameOverPanel.SetActive(true);
                Time.timeScale = 0.01f;
                while (true)
                {
                    if (Input.GetKey(KeyCode.Mouse0))
                    {

                        MemoryItemManage.TeddyMission = false;
                        Character.ActionProhibit = false;
                        Character.IsDialoged = 0;
                        Time.timeScale = 1;
                        SceneManager.LoadScene(1);
                    }
                    yield return new WaitForEndOfFrame();
                }

            }
            yield return new WaitForEndOfFrame();
        }

    }
    public static float HitNumPlayer;
    public GameObject ShowHitPlayer;
    void HitFunction()
    {
        if (ImaHP > HP)
        {
            HitNumPlayer = HP - ImaHP;
            Instantiate(ShowHitPlayer,canvas);
            ImaHP = HP;

        }
        else
        {
            ImaHP = HP;
        }
    }
    public Transform canvas;
}
