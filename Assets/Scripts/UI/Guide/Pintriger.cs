using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pintriger : Dialog
{
    public DoorRotate[] door;
    private void Awake()
    {
        foreach(DoorRotate door in door)
        {
            door.enabled = true;
        }
        Character.ActionProhibit = true;
    }
    protected override void Update()
    {
        if (Character.IsDialoged - t > 2)
        {
            Destroy(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            LeftClick = true;
        }
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
            for (float j = 0; j < RunTime[i]; j += 0.01f)
            {
                if (LeftClick == true)
                {
                    Time.timeScale = 1;
                    DialogImage.SetActive(false);
                    yield break;
                }
                yield return new WaitForSecondsRealtime(0.01f);
            }
        }
        LeftClick = false;
        Character.ActionProhibit = false;
        Time.timeScale = 1;
        DialogImage.SetActive(false);
        foreach (GameObject trigger in nexttrigger)
        {
            trigger.SetActive(true);
        }
    }
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pin")
        {
            Destroy(cd);
            StartCoroutine(ShowTextFunction());
        }
    }
}
