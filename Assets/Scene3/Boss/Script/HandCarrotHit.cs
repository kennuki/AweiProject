using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCarrotHit : MonoBehaviour
{
    private float CarrotDamage;
    public CharacterAbility characterAbility;
    CapsuleCollider cd;
    BoxCollider cd2;
    private void Start()
    {
        characterAbility = GameObject.Find("Character").GetComponent<CharacterAbility>();
        cd = GetComponent<CapsuleCollider>();
        cd2 = GetComponent<BoxCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        CarrotDamage = (BossAblity.AD * 1.65f) - characterAbility.DF;
        if (other.tag == "Player")
        {
            cd.enabled = false;
            cd2.enabled = false;
            CharacterAbility.Damage += CarrotDamage;
        }
    }
}
