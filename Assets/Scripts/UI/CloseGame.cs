using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseGame : MonoBehaviour
{
    public GameObject ExitButton;
    public GameObject ShortCutButton;
    public GameObject ShortCutImage;
    public GameObject BGMSlider;
    void Start()
    {
        
    }
    int x = 0;
    public bool ExcOn = false;
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShortCutImage.SetActive(false);
            x = 1;
            if (ExitButton.activeSelf == true && x == 1)
            {
                Time.timeScale = 1;
                ExcOn = false;
                x--;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                ExitButton.SetActive(false);
                ShortCutButton.SetActive(false);
                BGMSlider.SetActive(false);
                CameraRotation.cameratotate = true;
            }
            else if(ExitButton.activeSelf == false&& x==1)
            {
                Time.timeScale = 0;
                ExcOn = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                ExitButton.SetActive(true);
                ShortCutButton.SetActive(true);
                BGMSlider.SetActive(true);
                CameraRotation.cameratotate = false;
            }

        }

    }
    public void OnCloseButtonHit()
    {
        Application.Quit();
    }
    public void OnShortCutButtonHit()
    {
        ShortCutImage.SetActive(true);

    }
    public void OnShortCutExitButtonHit()
    {
        ShortCutImage.SetActive(false);

    }
}
