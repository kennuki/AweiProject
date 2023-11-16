using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveMe : MonoBehaviour
{
    private void Start()
    {
        
    }
    private void Update()
    {
        if(Scene2trigger.LevelClear == true && GameCompleteTrigger.AllClear==false)
        {
            DontDestroyOnLoad(gameObject);
        }
        if(SceneManager.GetActiveScene().buildIndex == 4)
        {
            Destroy(gameObject);
        }
    }
}
