using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHogAD : MonoBehaviour
{
    public MonsterAbility MonsterAbility;
    Collider cd;
    // Start is called before the first frame update
    void Start()
    {
        cd = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            CharacterAbility.Damage += MonsterAbility.AD * 1.2f;
            cd.enabled = false;
        }
    }
}
