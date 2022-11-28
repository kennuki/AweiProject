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
        if(Scene2trigger.LevelClear == true)
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
