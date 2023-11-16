using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene1 : MonoBehaviour
{
    public GameObject Video;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Video.SetActive(true);
            StartCoroutine(Load1());
        }
    }
    private IEnumerator Load1()
    {
        yield return new WaitForSeconds(25f);
        SceneManager.LoadScene(1);
    }
}
