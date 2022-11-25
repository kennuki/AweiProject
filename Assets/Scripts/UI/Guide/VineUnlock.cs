using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineUnlock : Dialog
{
    public bool Touch = false;
    public float VineUnlockDelay;
    public GameObject[] vines;
    public Animator anim;
    public int UnlockVineAllow=0;
    void Start()
    {
        anim.enabled = false;
        cd = GetComponent<Collider>();
        StartCoroutine(MemoryTrigger());
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
        }
        LeftClick = false;
        Character.ActionProhibit = false;
        Time.timeScale = 1;
        DialogImage.SetActive(false);
        anim.enabled = true;
        yield return new WaitForSeconds(VineUnlockDelay);
        UnlockVineAllow += 1;
        foreach (GameObject trigger in nexttrigger)
        {
            trigger.SetActive(true);
        }
    }
    protected override void OnTriggerEnter(Collider other)
    {

    }
    private IEnumerator MemoryTrigger()
    {
        while (true)
        {
            if (Touch == false)
            {
                yield return new WaitForSeconds(0.01f);
            }
            else if(Touch == true)
            {
                
                Destroy(cd);
                Character.IsDialoged++;

                StartCoroutine(ShowTextFunction());
                yield break;
            }
        }
    }
    
}
