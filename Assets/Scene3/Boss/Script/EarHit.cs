using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarHit : MonoBehaviour
{

    private float EarDamage;
    public CharacterAbility characterAbility;
    Collider cd;
    private void Start()
    {
        characterAbility = GameObject.Find("Character").GetComponent<CharacterAbility>();
        cd = GetComponent<Collider>();
        
    }
    private void OnTriggerEnter(Collider other)
    {
        EarDamage = BossAblity.AD * 2 - characterAbility.DF;
        if (other.tag == "Player")
        {
            cd.enabled = false;
            CharacterAbility.Damage += EarDamage;
        }
    }
}
