using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HedgeHog : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject pin;
    public GameObject ShootPoint;
    HedgeHogAbility HedgeHogAbility;
    void Start()
    {
        HedgeHogAbility = GetComponentInChildren<HedgeHogAbility>();
        float damage = HedgeHogAbility.AD * 1.1f;
        pin.GetComponent<Pin>().AttackDamage = damage;
        PlayerTransform = GameObject.Find("Character").GetComponent<Transform>();
        StartCoroutine(AttackFunction());
    }
    void Update()
    {

    }
    public Transform OutterBody;
    public Transform PlayerTransform;
    float Length;
    public float RotateSP = 10f;
    public float MoveSP = 0.06f;
    public float DetectDistance = 26;
    public float AttackSpeed = 2;
    private IEnumerator AttackFunction()
    {
        while (true)
        {
            Length = Vector3.Distance(PlayerTransform.transform.position, transform.position);
            if (Length < DetectDistance)
            {
                Instantiate(pin, ShootPoint.transform.position, Quaternion.Euler(Random.Range(-85, -95), transform.rotation.eulerAngles.y - 90, 90));
                Instantiate(pin, ShootPoint.transform.position, Quaternion.Euler(Random.Range(-65, -75), transform.rotation.eulerAngles.y - 90, 90));
                Instantiate(pin, ShootPoint.transform.position, Quaternion.Euler(Random.Range(-105, -115), transform.rotation.eulerAngles.y - 90, 90));
                yield return new WaitForSeconds(Random.Range(1.5f,3f));
                int a = Random.Range(1, 4);
                if (a == 3)
                {
                    Instantiate(pin, ShootPoint.transform.position, Quaternion.Euler(Random.Range(-85, -95), transform.rotation.eulerAngles.y - 90, 90));
                    Instantiate(pin, ShootPoint.transform.position, Quaternion.Euler(Random.Range(-65, -75), transform.rotation.eulerAngles.y - 90, 90));
                    Instantiate(pin, ShootPoint.transform.position, Quaternion.Euler(Random.Range(-105, -115),transform.rotation.eulerAngles.y - 90, 90));
                }
                for (int i = 0; i < 36; i++)
                {
                    transform.Rotate(0, 0, -RotateSP);
                    OutterBody.transform.Translate(0, 0, MoveSP);
                    yield return new WaitForSeconds(0.02f);
                }
                RotateSP = -RotateSP;
                MoveSP = -MoveSP;
                Instantiate(pin, ShootPoint.transform.position, Quaternion.Euler(Random.Range(-85, -95), transform.rotation.eulerAngles.y - 90, 90));
                Instantiate(pin, ShootPoint.transform.position, Quaternion.Euler(Random.Range(-65, -75), transform.rotation.eulerAngles.y - 90, 90));
                Instantiate(pin, ShootPoint.transform.position, Quaternion.Euler(Random.Range(-105, -115), transform.rotation.eulerAngles.y - 90, 90));
                yield return new WaitForSeconds(1f);
                for (int i = 0; i < 36; i++)
                {
                    transform.Rotate(0, 0, -RotateSP);
                    OutterBody.transform.Translate(0, 0, MoveSP);
                    yield return new WaitForSeconds(0.02f);
                }
                RotateSP = -RotateSP;
                MoveSP = -MoveSP;
            }
            else
            {
                yield return new WaitForSeconds(1/AttackSpeed);
            }
        }
      
    }
}
