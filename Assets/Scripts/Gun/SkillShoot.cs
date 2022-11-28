using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillShoot : MonoBehaviour
{
    public Character character;
    public GameObject bullet;
    public GameObject bullet2;
    public GameObject CM1;
    public GameObject CM1LookPoint;
    public GameObject ShootRight;
    public GameObject ShootLeft;
    public GameObject ShootEffectRight;
    public GameObject ShootEffectLeft;
    public GameObject ShootEffect;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShootFunction(GameObject Pos, GameObject BulletType,GameObject EffectPos, GameObject Effect)
    {
        Vector3 dir = Vector3.Normalize(CM1.transform.position - CM1LookPoint.transform.position);
        Shoot.ShootDir = Vector3.Normalize(Pos.transform.position - (CM1LookPoint.transform.position - 15 * dir));
        Instantiate(BulletType, Pos.transform.position, Quaternion.Euler(0, Mathf.Atan2(Shoot.ShootDir.x, Shoot.ShootDir.z) * Mathf.Rad2Deg - 90, Mathf.Atan2(Shoot.ShootDir.y, Mathf.Sqrt(Shoot.ShootDir.z * Shoot.ShootDir.z + Shoot.ShootDir.x * Shoot.ShootDir.x)) * Mathf.Rad2Deg));
        Instantiate(Effect, EffectPos.transform.position, Quaternion.identity);
    }
}
