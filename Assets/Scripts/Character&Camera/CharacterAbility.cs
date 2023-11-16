using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterAbility : MonoBehaviour
{
    public static float HPSteal = 0;
    public static float Damage = 0;
    public static float SP=1;
    public float MaxHP = 50;
    public float MaxMP = 50;
    public float HP;
    public float MP;
    float ImaHP;
    public float AD =10;
    public float DF = 5;
    public float CritRate = 0.2f;
    public float AS = 1.3f;
    public float HPRecover = 0.5f;
    public float MPRecover = 2f;
    public float gametime=1f;
    public GameObject DeadEffect;
    Animator anim;
    public AudioSource audioSource;
    public SaveMeDestroyAllChild[] saveMeDestroyAllChildren;
    private Character character;
    public UI_Inventory ui_inventory;
    void Start()
    {
        ADTemp = AD;
        MaxHPTemp = MaxHP;
        character = this.gameObject.GetComponent<Character>();
        anim = transform.GetComponentInChildren<Animator>();
        HP = MaxHP;
        MP = MaxMP;
        ImaHP = HP;
        StartCoroutine(dead());
    }
    private float ADTemp;
    private float MaxHPTemp;
    public void OnSceneChange()
    {
        ADTemp = AD;
        MaxHPTemp = MaxHP;
    }
    public void OnSceneReloaded()
    {
        AD = ADTemp;
        MaxHP = MaxHPTemp;
    }
    void Update()
    {
        Debug.Log(ADTemp);
        if (Input.GetKey(KeyCode.O))
        {
            AD += 50;
        }
        ReduceBossDamage();
        SlowRecover();
        AS = anim.GetFloat("AS");
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (Time.timeScale != 1)
            {
                Time.timeScale = 1;
            }
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
        if (HPSteal != 0)
        {
            HP += ((MaxHP-HP) * 0.01f * HPSteal);
            MP += ((MaxMP - MP) * 0.01f * HPSteal);
            HPSteal = 0;
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
                while (anim.GetBool("Dead")==true)
                {
                    if (Input.GetKey(KeyCode.Mouse0))
                    {
                        ui_inventory.OnSceneReloaded();
                        MemoryItemManage.TeddyMission = false;
                        Character.ActionProhibit = false;
                        Character.IsDialoged = 0;
                        Time.timeScale = 1;
                        anim.SetBool("Dead", false);
                        GameOverPanel.SetActive(false);
                        DeadEffect.SetActive(false);
                        foreach (SaveMeDestroyAllChild saveMeDestroyAllChild in saveMeDestroyAllChildren)
                        {
                            saveMeDestroyAllChild.DestroyAllChild();
                        }
                        OnSceneReloaded();
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                        HP = MaxHP;
                        MP = MaxMP;
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
            StartCoroutine(character.BodyBeHitSparkle(4, 1.9f));
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
    public void OnTriggerStay(Collider other)
    {
        if(other.tag == "Cotton")
        {
            SP = 0.7f;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Cotton")
        {
            SP = 1f;
        }
    }
    private void SlowRecover()
    {
        if (SP < 1)
        {
            SP += Time.deltaTime;
        }
    }
    private void ReduceBossDamage()
    {
        if(MemoryItemManage.Candy3Mission == true)
        {
            DF = 10;
        }
    }
}
