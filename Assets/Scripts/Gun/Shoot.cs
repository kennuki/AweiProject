using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Character character;
    public GameObject bullet;
    public GameObject bullet2;
    public GameObject CM1;
    public GameObject CM1LookPoint;
    public GameObject ShootRight;
    public GameObject ShootLeft;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ShootPosition();
    }


    public static Vector3 ShootDir;
    public bool ShootBreak = false;
    private void ShootPosition()
    {
        if (character.ShootAllow == 1)
        {
            character.ShootAllow = 0;
            ShootBreak = false;
            StartCoroutine(ShootDelay(ShootRight, 0.35f,bullet));
        }
        else if(character.ShootAllow == 2)
        {
            character.ShootAllow = 0;
            ShootBreak = false;
            StartCoroutine(ShootDelay(ShootLeft, 0.35f, bullet));
        }
        else if (character.ShootAllow == 3)
        {
            character.ShootAllow = 0;
            ShootBreak = false;
            StartCoroutine(ShootDelay(ShootRight,0.7f, bullet2));
            StartCoroutine(ShootDelay(ShootLeft, 0.8f, bullet2));
        }
    }
    private IEnumerator ShootDelay(GameObject Pos,float t,GameObject BulletType)
    {
        yield return new WaitForSeconds(t / character.AS*3/4);
        if (ShootBreak == true)
        {
            if (t == 0.7f)
            {

            }
            else
            ShootBreak = false;
            yield break;
        }
        yield return new WaitForEndOfFrame();
        ShootBreak = false;
        yield return new WaitForSeconds(t / character.AS/2*1/4);
        Vector3 dir = Vector3.Normalize(CM1.transform.position - CM1LookPoint.transform.position);
        ShootDir = Vector3.Normalize(Pos.transform.position - (CM1LookPoint.transform.position - 15 * dir));
        Instantiate(BulletType, Pos.transform.position, Quaternion.Euler(0, Mathf.Atan2(ShootDir.x, ShootDir.z) *Mathf.Rad2Deg-90, Mathf.Atan2(ShootDir.y, Mathf.Sqrt(ShootDir.z* ShootDir.z + ShootDir.x* ShootDir.x)) * Mathf.Rad2Deg));
        yield break;
    }
}
