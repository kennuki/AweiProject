using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Load1());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator Load1()
    {
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene(1);
    }
}
