using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAppear : MonoBehaviour
{
    [SerializeField] ParticleSystem [] GunAppearEffect = null;
    public Material gunbody;
    public Animator anim1;
    void Start()
    {
        StartCoroutine(GunHide());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject Gun;
    bool IfGunHided = true;
    bool InActive = false;
    private IEnumerator GunHide()
    {
        float t = 0;
        while (true)
        {
            InActive = false;
            if (IfGunHided == true && Input.GetKeyDown(KeyCode.X) && Cursor.visible == false|| IfGunHided == true && Input.GetKey(KeyCode.Mouse0) && Cursor.visible == false)
            {
                InActive = true;
                t = 0;
                Gun.SetActive(true);
                GunAppearEffect[0].Play();
                GunAppearEffect[1].Play();
                GunAppearEffect[2].Play();
                IfGunHided = false;
                StartCoroutine(FadeInFunction());
                yield return new WaitForSeconds(1.2f);
            }
            else if (IfGunHided == false && Input.GetKeyDown(KeyCode.X) && Cursor.visible == false||t>80)
            {
                t = 0;
                IfGunHided = true;
                GunAppearEffect[2].Play();
                StartCoroutine(FadeOutFunction());
                yield return null;
            }
            else if (IfGunHided == false && Input.GetKey(KeyCode.Mouse0) && Cursor.visible == false)
            {
                t = 0;
            }
            if (IfGunHided == false)
            t += Time.deltaTime;
            else
            {
                t = 0;
            }
            yield return null;
        }    
    }
    IEnumerator FadeInFunction()
    {
        for (float i = 0; i <= 1; i += Time.deltaTime*2.5f)
        {
            gunbody.SetFloat("Alpha", i);
            anim1.SetLayerWeight(1, i);
            yield return null;
        }

    }
    IEnumerator FadeOutFunction()
    {
        for (float i = 1; i >= 0; i -= Time.deltaTime * 2f)
        {
            if (InActive == true)
            {
                anim1.SetLayerWeight(1, 1);
                yield break;
            }
            gunbody.SetFloat("Alpha", i);
            anim1.SetLayerWeight(1, i);
            if (i < 0.1f)
            {
                Gun.SetActive(false);
            }
            yield return null;
        }
    }
}
