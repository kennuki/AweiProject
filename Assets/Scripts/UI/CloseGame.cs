using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseGame : MonoBehaviour
{
    public GameObject ExitButton;
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
            x = 1;
            if (ExitButton.activeSelf == true && x == 1)
            {
                ExcOn = false;
                x--;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                ExitButton.SetActive(false);
            }
            else if(ExitButton.activeSelf == false&& x==1)
            {
                ExcOn = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                ExitButton.SetActive(true);
            }

        }

    }
    public void OnCloseButtonHit()
    {
        Application.Quit();
    }
}
