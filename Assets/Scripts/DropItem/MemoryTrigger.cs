using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryTrigger : MonoBehaviour
{
    public string TriggerName;
    public GameObject TriggerManager;
    public Transform Trigger;
    private VineUnlock VineUnlock;
    private void Start()
    {
        TriggerManager = GameObject.Find("Dialog Trigger Manager");
        Trigger = TriggerManager.transform.Find(TriggerName);
        VineUnlock = Trigger.GetComponent<VineUnlock>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            VineUnlock.Touch = true;

        }
    }
}
