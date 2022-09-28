using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HedgeHog : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AttackFunction());
    }

    public GameObject Pin;
    public GameObject ShootPoint;
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.H))
        {
            Instantiate(Pin, transform.position, Quaternion.Euler(Random.Range(-85,-95), 0+OutterBody.transform.rotation.eulerAngles.y-90, 90));
            Instantiate(Pin, transform.position, Quaternion.Euler(Random.Range(-65, -75), 0+transform.rotation.eulerAngles.y - 90, 90));
            Instantiate(Pin, transform.position, Quaternion.Euler(Random.Range(-105, -115), 0+transform.rotation.eulerAngles.y - 90, 90));
        }
    }

    public Transform OutterBody;
    public Transform PlayerTransform;
    float Length;
    public float RotateSP = 10f;
    public float MoveSP = 0.06f;
    private IEnumerator AttackFunction()
    {
        while (true)
        {
            Length = Vector3.Distance(PlayerTransform.transform.position, transform.position);
            if (Length < 25)
            {
                Instantiate(Pin, transform.position, Quaternion.Euler(Random.Range(-85, -95), transform.rotation.eulerAngles.y - 90, 90));
                Instantiate(Pin, transform.position, Quaternion.Euler(Random.Range(-65, -75), transform.rotation.eulerAngles.y - 90, 90));
                Instantiate(Pin, transform.position, Quaternion.Euler(Random.Range(-105, -115), transform.rotation.eulerAngles.y - 90, 90));
                yield return new WaitForSeconds(Random.Range(1.5f,3f));
                int a = Random.Range(1, 4);
                if (a == 3)
                {
                    Instantiate(Pin, transform.position, Quaternion.Euler(Random.Range(-85, -95), transform.rotation.eulerAngles.y - 90, 90));
                    Instantiate(Pin, transform.position, Quaternion.Euler(Random.Range(-65, -75), transform.rotation.eulerAngles.y - 90, 90));
                    Instantiate(Pin, transform.position, Quaternion.Euler(Random.Range(-105, -115),transform.rotation.eulerAngles.y - 90, 90));
                }
                for (int i = 0; i < 36; i++)
                {
                    transform.Rotate(0, 0, -RotateSP);
                    OutterBody.transform.Translate(MoveSP, 0, 0);
                    yield return new WaitForSeconds(0.02f);
                }
                RotateSP = -RotateSP;
                MoveSP = -MoveSP;
                Instantiate(Pin, transform.position, Quaternion.Euler(Random.Range(-85, -95), transform.rotation.eulerAngles.y - 90, 90));
                Instantiate(Pin, transform.position, Quaternion.Euler(Random.Range(-65, -75), transform.rotation.eulerAngles.y - 90, 90));
                Instantiate(Pin, transform.position, Quaternion.Euler(Random.Range(-105, -115), transform.rotation.eulerAngles.y - 90, 90));
                yield return new WaitForSeconds(1f);
                for (int i = 0; i < 36; i++)
                {
                    transform.Rotate(0, 0, -RotateSP);
                    OutterBody.transform.Translate(MoveSP, 0, 0);
                    yield return new WaitForSeconds(0.02f);
                }
                RotateSP = -RotateSP;
                MoveSP = -MoveSP;
            }
            else
            {
                yield return new WaitForSeconds(0.5f);
            }
        }
      
    }
}
