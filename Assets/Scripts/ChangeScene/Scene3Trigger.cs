using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene3Trigger : MonoBehaviour
{
    private Scene2trigger scene2Trigger;
    private Collider Cd;
    public GameObject Teleporter;
    private void Start()
    {
        Cd = GetComponent<Collider>();
        scene2Trigger = GameObject.Find("Scene2Trigger").GetComponent<Scene2trigger>();
    }
    private void Update()
    {
        if(MemoryItemManage.InvitationMission == true)
        {
            Cd.enabled = true;
            Teleporter.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(scene2Trigger.LoadSceneFunction(3));
        }
    }

}
