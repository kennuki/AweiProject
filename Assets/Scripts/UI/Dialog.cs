using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialog : MonoBehaviour
{
    public float t;
    void Start()
    {
        Character.IsDialoged++;
        t = Character.IsDialoged;
        cd = GetComponent<Collider>();
    }
    Collider cd;
    public GameObject nexttrigger;
    public GameObject DialogImage;
    public TextMeshProUGUI Dialog_T;
    public float[] RunTime;
    public string[] text;
    public int Num;
    public int[] PlayerNum;
    // Update is called once per frame
    void Update()
    {
        if (Character.IsDialoged-t>1)
        {
            Character.IsDialoged -= 1;
            Destroy(this);
        }
    }
    private IEnumerator ShowTextFunction()
    {
        for (int i = 0; i < Num; i++)
        {
            if (PlayerNum[i] == 1)
            {
                Dialog_T.color = new Vector4(0.77f, 0.98f, 0.917f,1);
            }
            if (PlayerNum[i] == 2)
            {
                Dialog_T.color = new Vector4(0.73f, 0.74f, 0.96f,1);
            }
            DialogImage.SetActive(true);
            Dialog_T.text = text[i];
                yield return new WaitForSeconds(RunTime[i]);
        }
        DialogImage.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        nexttrigger.SetActive(true);
        if(other.tag == "Player")
        {
            StartCoroutine(ShowTextFunction());
            Destroy(cd);
        }
        else if (other.tag == "Pin")
        {
            StartCoroutine(ShowTextFunction());
            Destroy(cd);
        }
    }

}
