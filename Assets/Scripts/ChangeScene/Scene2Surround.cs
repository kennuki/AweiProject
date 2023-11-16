using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene2Surround : MonoBehaviour
{
    public Animator anim;
    void Start()
    {
        anim.enabled = true;
        StartCoroutine(MonsterCreate());
    }
    public GameObject[] Monster;
    private IEnumerator MonsterCreate()
    {
        Character.ActionProhibit = true;
        yield return new WaitForSeconds(6f);
        foreach(GameObject monster in Monster)
        {
            monster.SetActive(true);
        }
        Character.ActionProhibit = false;
    }
}
