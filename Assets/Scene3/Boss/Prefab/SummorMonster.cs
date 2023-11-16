using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SummorMonster : MonoBehaviour
{
    public GameObject Monster;
    NavMeshAgent agent;
    Mare1 mare1;
    private void Start()
    {
        agent = Monster.GetComponent<NavMeshAgent>();
        mare1 = Monster.GetComponent<Mare1>();
        StartCoroutine(ActiveDelay());
    }
    private IEnumerator ActiveDelay()
    {
        yield return new WaitForSeconds(1.3f);
        if(agent!=null)
        agent.enabled = true;
        if(mare1!=null)
        mare1.enabled = true;
        if(Monster!=null)
        Monster.transform.parent = null;
        Destroy(this.gameObject);
    }

}
