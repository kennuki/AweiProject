using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCreate : Dialog
{
    public Character character;
    public GameObject[] gameObjects;
    protected override IEnumerator ShowTextFunction()
    {
        character.ThreeAttackGet = true;
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
            if (i == 4)
            {
               for(int k = 0; k < gameObjects.Length; k++)
                {
                    gameObjects[k].SetActive(true);
                }
            }
            for (float j = 0; j < RunTime[i]; j += 0.1f)
            {
                if (LeftClick == true)
                {
                    j = RunTime[i] + 1;
                    LeftClick = false;
                }
                yield return new WaitForSeconds(0.1f);
            }
        }
        Character.ActionProhibit = false;
        Time.timeScale = 1;
        DialogImage.SetActive(false);
        foreach (GameObject trigger in nexttrigger)
        {
            trigger.SetActive(true);
        }
    }
}
