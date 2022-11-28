using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene2trigger : MonoBehaviour
{
    public Character character;
    public SaveMeDestroyAllChild[] saveMeDestroyAllChildren;
    public SaveMeInactive[] saveMeInactives;
    public static bool LevelClear = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "player")
        {
            SceneManager.LoadScene(2);
        }
    }
    private void Start()
    {
        StartCoroutine(Load2Scene());
    }
    public IEnumerator Load2Scene()
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.L))
            {
                LevelClear = true;
                foreach (SaveMeDestroyAllChild saveMeDestroyAllChild in saveMeDestroyAllChildren)
                {
                    saveMeDestroyAllChild.DestroyAllChild();
                }
                foreach(SaveMeInactive saveMeInactive in saveMeInactives)
                {
                    saveMeInactive.SetChildInActive();
                }
                yield return new WaitForSeconds(1f);
                SceneManager.sceneLoaded += character.OnSceneLoaded;
                LevelClear = false;
                SceneManager.LoadScene(2);
            }
            yield return new WaitForEndOfFrame();
        }

    }

}
