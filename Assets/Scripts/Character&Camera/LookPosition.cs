using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookPosition : MonoBehaviour
{
    public GameObject SecondPostion;
    Vector3 Offset;
    void Start()
    {
        Offset = transform.position - SecondPostion.transform.position;
    }
    void Update()
    {
        RaycastHit hit;
        RaycastHit hit2;
        if (Physics.Raycast(transform.position, transform.position - SecondPostion.transform.position, out hit, Vector3.Distance(transform.position, SecondPostion.transform.position)))
        {
            if (hit.transform.gameObject.tag == "Wall")
            {
                transform.position = SecondPostion.transform.position;
            }



        }
        if (Physics.Raycast(SecondPostion.transform.position, Quaternion.Euler(transform.eulerAngles)*-Offset, out hit2, Vector3.Distance(SecondPostion.transform.position, SecondPostion.transform.position + Offset)))
        {
            if (hit2.transform.gameObject.tag !="Wall")
            {
                transform.position = SecondPostion.transform.position + Quaternion.Euler(transform.eulerAngles) * -Offset;
            }
            else if (hit2.transform.gameObject.tag == "Wall")
            {
                transform.position = SecondPostion.transform.position;
            }
        }
        else
        {
            transform.position = SecondPostion.transform.position + Quaternion.Euler(transform.eulerAngles) * -Offset;
        }

        Debug.DrawLine(SecondPostion.transform.position, SecondPostion.transform.position + Quaternion.Euler(transform.eulerAngles) * -Offset);
       // Debug.Log(hit2.transform.gameObject.tag);
    }
}
