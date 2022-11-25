using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    public float AttackDamage = 0;
    void Start()
    {
        StartCoroutine(RotateFunction(3.3f, 180));
        Destroy(this.gameObject, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            CharacterAbility.Damage += AttackDamage;
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate()
    {
        ScaleFunction();
        AccelerateFunction();
    }

    public float InitialSpeed = 0.02f;
    public float AccelerateRate = 1.018f;
    public float MaxAcceleration = 5f;
    private void AccelerateFunction()
    {
        InitialSpeed = Mathf.Clamp(InitialSpeed * AccelerateRate* AccelerateRate * AccelerateRate, 0, MaxAcceleration);
        transform.Translate(Vector3.up * InitialSpeed);
    }
    private IEnumerator RotateFunction(float RotateSpeed, int MaxAngle)
    {
        for (float i = 1; i <= MaxAngle; i += RotateSpeed)
        {
            transform.Rotate(RotateSpeed, 0, 0);
            yield return new WaitForFixedUpdate();
        }
    }

    float ScaleRate = 1.01f;
    float MaxScale = 45f;
    private void ScaleFunction()
    {
        if (transform.localScale.y < MaxScale)
        {
            transform.localScale = transform.localScale * ScaleRate;
        }

    }

    
}
