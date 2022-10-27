using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class CameraRotation : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cameratotate = true;
        BagIcon.SetActive(false);
        rect.anchoredPosition = new Vector2(0, -540);
        StartCoroutine(LockCursorToMiddle());
        StartCoroutine(CameraObstacleInteraction());
    }
    private void Update()
    {
        CursorHide();
        if (cameratotate == true)
        {
            CameraRotateFunction();
        }
        CameraScrollFunction();
        AnimationPosXWeight();
    }

    void FixedUpdate()
    {

    }

    bool cameratotate = true;
    private IEnumerator LockCursorToMiddle()
    {
        float waittime;
        bool state = false;
        while (true)
        {
            state = !state;
            if (cameratotate == false)
            {
                waittime = 0f;
                state = false;
            }
            else if (state)
            {
                Cursor.lockState = CursorLockMode.Locked;
                va2 = Vector2.zero;
                vt2 = vn2;
                //vt2 = new Vector2(0.4992765f, 0.5012655f);
                waittime = 0f;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                vn2 = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                waittime = 0.1f;
            }
            yield return new WaitForSeconds(waittime);
        }
    }


    public GameObject BagIcon;
    public RectTransform rect;
    private void CursorHide()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt) && cameratotate == false)
        { 
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            cameratotate = true;
            BagIcon.SetActive(false);
            rect.anchoredPosition = new Vector2(-998, -213);
        }
        else if (Input.GetKeyDown(KeyCode.LeftAlt) && cameratotate == true)
        {
   
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            cameratotate = false;
            BagIcon.SetActive(true);
        }
    }


    public CinemachineVirtualCamera CM1;
    public GameObject LookPoint;
    public GameObject PlayerRotate2;
    public GameObject PlayerRotate;  //control character's rotation that follow the camera
    public float CmRotateRate = 1f;  //rotate speed rate
    public Vector2 va2 = Vector2.zero;      //offset of the cursor
    Vector2 vt2 = Vector2.zero;   //temp that store the cursor's position in the screen
    Vector2 v2 = new Vector2(0.5f,0.5f);
    Vector2 vn2 = Vector2.zero;
    private void CameraRotateFunction()
    {
        float h = Input.GetAxis("Horizontal");
        float j = Input.GetAxis("Vertical");
        var transposer = CM1.GetCinemachineComponent<CinemachineTransposer>();
        if (Input.GetKeyDown(KeyCode.F7))
        {
            transposer.m_FollowOffset = new Vector3(0.2f, 0.7f, -8);
        }
        v2 = Camera.main.ScreenToViewportPoint(Input.mousePosition); //cursor's position in screen(0~1)
        va2 = v2 - vt2;
        if (Mathf.Abs(va2.x) > 0f&&Cursor.lockState==CursorLockMode.None)
        {
            this.transform.Rotate(Vector3.up * va2.x * CmRotateRate, Space.World);
            PlayerRotate.transform.Rotate(Vector3.up * va2.x * CmRotateRate, Space.World);
            if (h == 0 && j == 0)
            {
                PlayerRotate2.transform.Rotate(Vector3.up  * -va2.x * CmRotateRate, Space.World);
            }
        }
        float CameraXAngle = this.transform.eulerAngles.x;
        if (CameraXAngle > 180)
        {
            CameraXAngle -= 360;
        }
        float faceAngle;
        Vector2 PlayerXZ = new Vector2(LookPoint.transform.position.x, LookPoint.transform.position.z);
        Vector2 CameraXZ = new Vector2(transform.position.x, transform.position.z);
        float XZDistance = Vector2.Distance(PlayerXZ, CameraXZ);
        faceAngle = Mathf.Atan2(XZDistance, LookPoint.transform.position.y - transform.position.y) * Mathf.Rad2Deg;
        if (Mathf.Abs(va2.y) >= 0f && Cursor.lockState == CursorLockMode.None)
        {
            if (va2.y > 0 && CameraXAngle > -25f)
            {
                if (Physics.Raycast(LookPoint.transform.position, (transform.position - LookPoint.transform.position), out Hit, 10f, mask))
                {
                    if (Hit.transform.tag == "Floor" && Vector3.Distance(transform.position, LookPoint.transform.position) > 1 && Vector3.Distance(transform.position, LookPoint.transform.position) < 9f)
                    {
                        transposer.m_FollowOffset += new Vector3(0, 0, va2.y * 3f) * CmRotateRate / 30;
                        this.transform.Rotate(Vector3.left * va2.y * CmRotateRate * 0.12f, Space.Self);
                    }
                }
                transposer.m_FollowOffset += new Vector3(0, -va2.y * 4.2f, 0) * CmRotateRate / 30;
                this.transform.Rotate(Vector3.left * va2.y * CmRotateRate * 1.2f, Space.Self);
                Quaternion targetRotation = Quaternion.Euler(faceAngle - 90, transform.eulerAngles.y, transform.eulerAngles.z);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.2f);



            }
            else if (va2.y <= 0 && CameraXAngle < 30f)
            {
                if (Physics.Raycast(LookPoint.transform.position, (transform.position - LookPoint.transform.position), out Hit, 10f, mask))
                {
                    if (Hit.transform.tag == "Floor" && Vector3.Distance(transform.position, LookPoint.transform.position) > 1 && Vector3.Distance(transform.position, LookPoint.transform.position) < 9f)
                    {
                        transposer.m_FollowOffset += new Vector3(0, 0, va2.y * 3f) * CmRotateRate / 30;
                        this.transform.Rotate(Vector3.left * va2.y * CmRotateRate * 0.12f, Space.Self);
                    }
                }
                transposer.m_FollowOffset += new Vector3(0, -va2.y * 4.2f, 0) * CmRotateRate / 30;
                this.transform.Rotate(Vector3.left * va2.y * CmRotateRate * 1.2f, Space.Self);
                Quaternion targetRotation = Quaternion.Euler(faceAngle - 90, transform.eulerAngles.y, transform.eulerAngles.z);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.2f);
            }
        }

        va2 = Vector2.zero;
        vt2 = v2;
    }
    public float test=0.2f;
    Vector3 CameraOffsetToLocal = Vector3.zero;
    RaycastHit Hit;
    int mask = 7;
    private IEnumerator CameraObstacleInteraction()
    {
        var transposer = CM1.GetCinemachineComponent<CinemachineTransposer>();
        while (true)
        {
            float MaxRange = Vector3.Distance(LookPoint.transform.position, transform.position);
            Debug.DrawLine(LookPoint.transform.position, transform.position,Color.green);
            //float MaxRange = Vector3.Distance(transform.position, PlayerRotate.transform.position);
            float CameraXAngle = this.transform.eulerAngles.x;
            if (CameraXAngle > 180)
            {
                CameraXAngle -= 360;
            }
            if (CameraXAngle > 31)
            {
                transform.Rotate(-test, 0, 0);
            }
            else if (CameraXAngle < -30)
            {
                transform.Rotate(test, 0, 0);
            }
            if (transposer.m_FollowOffset.y > 5*MaxRange/7)
            {
                transposer.m_FollowOffset.y = 5 * MaxRange / 7;
            }

            if (Physics.Raycast(LookPoint.transform.position, (transform.position - LookPoint.transform.position), out Hit, 8,mask))
            {
                if (Hit.transform.tag == "Floor")
                {

                }             
                else if(Hit.transform.tag != "Wall")
                {
                    if (Vector3.Distance(transform.position, LookPoint.transform.position) < 7)
                    {
                        CameraOffsetToLocal = transform.TransformPoint(transposer.m_FollowOffset);
                        CameraOffsetToLocal += (transform.position - LookPoint.transform.position) * test;
                        CameraOffsetToLocal = transform.InverseTransformPoint(CameraOffsetToLocal);
                        CameraOffsetToLocal.x = 0;
                        transposer.m_FollowOffset = CameraOffsetToLocal;
                    }
                }
            }
            else if (Vector3.Distance(transform.position, LookPoint.transform.position) < 7)
            {
                CameraOffsetToLocal = transform.TransformPoint(transposer.m_FollowOffset);
                CameraOffsetToLocal += (transform.position - LookPoint.transform.position) * test;
                CameraOffsetToLocal = transform.InverseTransformPoint(CameraOffsetToLocal);
                CameraOffsetToLocal.x = 0;
                transposer.m_FollowOffset = CameraOffsetToLocal;
            }


            if (Physics.Raycast(LookPoint.transform.position, (transform.position - LookPoint.transform.position), out Hit,8, mask))
            {
                if (Hit.transform.tag == "Wall")
                {
                    /*CameraPositionChangeDistance = (this.transform.position - ObstaclePosition);
                    CameraOffsetToLocal = transform.TransformPoint(transposer.m_FollowOffset);
                    CameraOffsetToLocal -= CameraPositionChangeDistance;
                    CameraOffsetToLocal = transform.InverseTransformPoint(CameraOffsetToLocal);
                    CameraOffsetToLocal.x = transposer.m_FollowOffset.x;
                    CameraOffsetToLocal.y = transposer.m_FollowOffset.y;
                    transposer.m_FollowOffset = CameraOffsetToLocal;*/



                    /*if (CameraXAngle > 10 && transposer.m_FollowOffset.y > 2)
                    {
                        transposer.m_FollowOffset.y -= 0.002f * CameraXAngle;
                    }*/
                    if (Vector3.Distance(transform.position, LookPoint.transform.position) > 8)
                    {
                        CameraOffsetToLocal = transform.TransformPoint(transposer.m_FollowOffset);
                        CameraOffsetToLocal -= (transform.position - LookPoint.transform.position) * test;
                        CameraOffsetToLocal = transform.InverseTransformPoint(CameraOffsetToLocal);
                        CameraOffsetToLocal.x = 0;
                        transposer.m_FollowOffset = CameraOffsetToLocal;
                    }
                }
            }

            if (Vector3.Distance(transform.position, LookPoint.transform.position) > 8)
            {
                CameraOffsetToLocal = transform.TransformPoint(transposer.m_FollowOffset);
                CameraOffsetToLocal -= (transform.position - LookPoint.transform.position) * test;
                CameraOffsetToLocal = transform.InverseTransformPoint(CameraOffsetToLocal);
                CameraOffsetToLocal.x = 0;
                transposer.m_FollowOffset = CameraOffsetToLocal;
            }

            yield return new WaitForSeconds(test);
        }
       
    }

    public float MinFov = 10;
    public float MaxFov = 40;
    public float ScrollSensitivity = 10;
    private void CameraScrollFunction()
    {
        float Fov = CM1.m_Lens.FieldOfView;
        Fov -= Input.GetAxis("Mouse ScrollWheel") * ScrollSensitivity;
        Fov = Mathf.Clamp(Fov, MinFov, MaxFov);
        CM1.m_Lens.FieldOfView = Fov;
    }

    public Animator anim1;
    private void AnimationPosXWeight()
    {
        float CameraXAngle = this.transform.eulerAngles.x;
        if (CameraXAngle > 180)
        {
            CameraXAngle -= 360;
        }
        anim1.SetFloat("PosX", Mathf.Clamp((CameraXAngle + 15) / 55, 0, 1));
    }


}
