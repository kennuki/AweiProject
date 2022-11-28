using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveMeDestroyAllChild : MonoBehaviour
{
    private void Start()
    {
      
    }
    private void Update()
    {
        if (Scene2trigger.LevelClear == true)
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public void DestroyAllChild()
    {
        foreach(Transform TTT in this.gameObject.transform)
        {
            Destroy(TTT.gameObject);
        }
    }
}
