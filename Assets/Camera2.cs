using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Camera2 : MonoBehaviour
{
    CinemachineVirtualCamera cm1;
    // Start is called before the first frame update
    void Start()
    {
        cm1 = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F6))
        {

            if (cm1.m_Priority == 8)
            {
                cm1.m_Priority = 12;
            }
            else if (cm1.m_Priority == 12)
            {
                cm1.m_Priority = 8;
            }
        }
    }
}
