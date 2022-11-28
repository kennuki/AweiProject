using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveMeInactive : MonoBehaviour
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
    public void SetChildInActive()
    {
        foreach (Transform transform in this.gameObject.transform)
            transform.gameObject.SetActive(false);
    }
}
