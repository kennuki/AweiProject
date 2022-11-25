using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstEnemy : Dialog
{
    public GameObject FirstMonster;
    Monster2 monster2;
    private CapsuleCollider monster_cd;
    private void Start()
    {
        cd = GetComponent<Collider>();
        monster2 = FirstMonster.GetComponent<Monster2>();
        monster_cd = monster2.GetComponentInChildren<CapsuleCollider>();
        monster_cd.enabled = false;
        monster2.enabled = false;
    }
    protected override IEnumerator ShowTextFunction()
    {
        for (int i = 0; i < Num; i++)
        {
            LeftClick = false;
            if (PlayerNum[i] == 1)
            {
                Dialog_T.color = new Vector4(0.77f, 0.98f, 0.917f, 1);
            }
            if (PlayerNum[i] == 2)
            {
                Dialog_T.color = new Vector4(0.73f, 0.74f, 0.96f, 1);
            }
            Character.ActionProhibit = CharacterActionProhibit[i];
            Time.timeScale = DialogTimeScale[i];
            DialogImage.SetActive(true);
            Dialog_T.text = text[i];
            for (float j = 0; j < RunTime[i]; j += 0.1f)
            {
                if (LeftClick == true)
                {
                    j = RunTime[i] + 1;
                    LeftClick = false;
                }
                yield return new WaitForSeconds(0.1f);
            }
            if (i == Num - 1)
            {
                monster_cd.enabled = true;
                monster2.enabled = true;
            }
        }
        LeftClick = false;
        Character.ActionProhibit = false;
        Time.timeScale = 1;
        Character.AttackGet = true;
        DialogImage.SetActive(false);
        foreach (GameObject trigger in nexttrigger)
        {
            trigger.SetActive(true);
        }
    }
}
