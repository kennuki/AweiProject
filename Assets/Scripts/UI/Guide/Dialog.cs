using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialog : MonoBehaviour
{
    public float t;
    private void Start()
    {
        cd = GetComponent<Collider>();
    }
    protected Collider cd;
    public GameObject[] nexttrigger;
    public GameObject DialogImage;
    public TextMeshProUGUI Dialog_T;
    public float[] RunTime;
    public string[] text;
    public int Num;
    public int[] PlayerNum;
    public float[] DialogTimeScale;
    public bool[] CharacterActionProhibit;
    protected bool LeftClick = false;
    // Update is called once per frame
    protected virtual void Update()
    {
        if (Character.IsDialoged-t>2)
        {
            Destroy(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            LeftClick = true;
        }
    }
    protected virtual IEnumerator ShowTextFunction()
    {
        for (int i = 0; i < Num; i++)
        {
            LeftClick = false;
            if (PlayerNum[i] == 1)
            {
                Dialog_T.color = new Vector4(0.77f, 0.98f, 0.917f,1);
            }
            if (PlayerNum[i] == 2)
            {
                Dialog_T.color = new Vector4(0.73f, 0.74f, 0.96f,1);
            }
            Character.ActionProhibit = CharacterActionProhibit[i];
            Time.timeScale = DialogTimeScale[i];
            DialogImage.SetActive(true);
            Dialog_T.text = text[i];
            for(float j = 0; j < RunTime[i]*DialogTimeScale[i]; j += 0.1f)
            {
                if(LeftClick == true)
                {
                    j = RunTime[i]+1;
                    LeftClick = false;
                }
                yield return new WaitForSeconds(0.1f);
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
    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Destroy(cd);
            Character.IsDialoged++;
            StartCoroutine(ShowTextFunction());
        }
    }

}
