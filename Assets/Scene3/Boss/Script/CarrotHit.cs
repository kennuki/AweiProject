using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotHit : MonoBehaviour
{

    private float CarrotDamage;
    public CharacterAbility characterAbility;
    private CharacterController Player;
    Collider cd;
    private void Start()
    {
        characterAbility = GameObject.Find("Character").GetComponent<CharacterAbility>();
        cd = GetComponent<Collider>();

    }
    private void OnTriggerEnter(Collider other)
    {
        CarrotDamage = BossAblity.AD * 2 - characterAbility.DF;
        if (other.tag == "Player")
        {
            cd.enabled = false;
            CharacterAbility.Damage += CarrotDamage;
            Player = characterAbility.gameObject.GetComponent<CharacterController>();
            StartCoroutine(HitMove());
        }
    }
    private IEnumerator HitMove()
    {
        Vector3 PlayerPos = Player.gameObject.transform.position;
        Vector3 Pos = gameObject.transform.position;
        Vector3 Dir = (Pos - PlayerPos).normalized;
        for(float i = 0; i < 0.2f; i += Time.deltaTime)
        {
            Player.Move(-Dir*0.23f);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        
    }
}
