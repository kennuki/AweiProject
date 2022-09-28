using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterAbility : MonoBehaviour
{
    public float MaxHP = 50;
    public float MaxMP = 50;
    public float HP;
    public float MP;
    public float AD =10;
    public float CritRate = 0.2f;
    public float AS = 1.2f;
    public float HPRecover = 0.5f;
    public float MPRecover = 2f;



    void Start()
    {
        HP = MaxHP;
        MP = MaxMP;
    }

    // Update is called once per frame
    void Update()
    {
        HP = Mathf.Clamp(HP + HPRecover*Time.deltaTime, -1, MaxHP);
        MP = Mathf.Clamp(MP + MPRecover * Time.deltaTime, -1, MaxMP);
        dead();
    }


    public HedgeHogAbility HH;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pin")
        {
            HP -= HH.AD;
            Destroy(other.gameObject);
        }

    }
    public GameObject GameOverPanel;
    private void dead()
    {
        if (HP <= 0)
        {
            GameOverPanel.SetActive(true);
            Time.timeScale = 0;
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(1);
            }
        }
    }
}
