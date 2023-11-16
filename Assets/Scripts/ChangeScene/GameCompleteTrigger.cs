using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCompleteTrigger : MonoBehaviour
{
    public static bool AllClear = false;
    public Animator anim;
    private Scene2trigger scene2Trigger;
    private void Start()
    {
        AllClear = true;
        scene2Trigger = GameObject.Find("Scene2Trigger").GetComponent<Scene2trigger>();
    }
    private void Update()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("11");
            anim.SetBool("Owari", true);
            StartCoroutine(scene2Trigger.LoadSceneFunction(4));
        }
    }
}
