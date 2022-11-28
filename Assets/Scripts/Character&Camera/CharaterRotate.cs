using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaterRotate : MonoBehaviour
{
    Animator anim1;
    // Start is called before the first frame update
    void Start()
    {
        anim1 = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Character.ActionProhibit != true)
        {
            RotateFunction();
        }
        else if (Character.ActionProhibitWithoutMove == true)
        {
            RotateFunction();
        }

    }

    float faceAngle;
    Quaternion targetRotation;
    public void RotateFunction()
    {
        float h = Input.GetAxis("Horizontal") ;
        float j = Input.GetAxis("Vertical");
        if (anim1.GetBool("Attack") == true || anim1.GetBool("Attack2") == true || anim1.GetBool("Attack3") == true || anim1.GetInteger("SkillState")>=2)
        {
            h = 0;
            j = 0;
        }
        if (h == 0 && j == 0)
        {

        }
        else
        {
            faceAngle = Mathf.Atan2(h, j) * Mathf.Rad2Deg;
            targetRotation = Quaternion.Euler(0, faceAngle + Character.imaangle * Mathf.Rad2Deg, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.3f);
        }
        if (h != 0 || j != 0)
        {

        }
        else if (anim1.GetBool("Attack") == true || anim1.GetBool("Attack2") == true || anim1.GetBool("Attack3") == true || anim1.GetInteger("SkillState") >= 2)
        {
            faceAngle = Mathf.Atan2(h, j) * Mathf.Rad2Deg;
            targetRotation = Quaternion.Euler(0, faceAngle+5 + Character.imaangle * Mathf.Rad2Deg, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.3f);
        }


    }
}
