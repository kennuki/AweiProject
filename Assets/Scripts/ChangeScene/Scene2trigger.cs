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
    public GameObject canvas;
    private Collider Cd;
    public UI_Inventory uI_Inventory;
    public CharacterAbility characterAbility;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine(LoadSceneFunction(2));
        }
    }
    private void Start()
    {
        Cd = GetComponent<Collider>();
        StartCoroutine(LoadScene());
    }
    public IEnumerator LoadScene()
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
                foreach (SaveMeInactive saveMeInactive in saveMeInactives)
                {
                    saveMeInactive.SetChildInActive();
                }
                canvas.SetActive(true);
                yield return new WaitForSeconds(1f);
                SceneManager.sceneLoaded += character.OnSceneLoaded;
                LevelClear = false;
                SceneManager.LoadScene(2);
                uI_Inventory.OnSceneChange();
                characterAbility.OnSceneChange();
            }
            /*else if (MemoryItemManage.OldLetterMission == true&&SceneManager.GetActiveScene().buildIndex==1)
             {
                 yield return new WaitForSeconds(1f);
                 LevelClear = true;
                 foreach (SaveMeDestroyAllChild saveMeDestroyAllChild in saveMeDestroyAllChildren)
                 {
                     saveMeDestroyAllChild.DestroyAllChild();
                 }
                 foreach (SaveMeInactive saveMeInactive in saveMeInactives)
                 {
                     saveMeInactive.SetChildInActive();
                 }
                 canvas.SetActive(true);
                 yield return new WaitForSeconds(1f);
                 SceneManager.sceneLoaded += character.OnSceneLoaded;
                 LevelClear = false;
                 SceneManager.LoadScene(2);
                 yield return new WaitForSeconds(1f);
             }*/
             else if (Input.GetKey(KeyCode.K))
             {
                 LevelClear = true;
                 foreach (SaveMeDestroyAllChild saveMeDestroyAllChild in saveMeDestroyAllChildren)
                 {
                     saveMeDestroyAllChild.DestroyAllChild();
                 }
                 foreach (SaveMeInactive saveMeInactive in saveMeInactives)
                 {
                     saveMeInactive.SetChildInActive();
                 }
                 canvas.SetActive(true);
                 yield return new WaitForSeconds(1f);
                 SceneManager.sceneLoaded += character.OnSceneLoaded;
                 LevelClear = false;
                 SceneManager.LoadScene(3);
                 uI_Inventory.OnSceneChange();
                 characterAbility.OnSceneChange();
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }

    }
    public IEnumerator LoadSceneFunction(int SceneIndex)
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            LevelClear = true;
            foreach (SaveMeDestroyAllChild saveMeDestroyAllChild in saveMeDestroyAllChildren)
            {
                saveMeDestroyAllChild.DestroyAllChild();
            }
            foreach (SaveMeInactive saveMeInactive in saveMeInactives)
            {
                saveMeInactive.SetChildInActive();
            }
            canvas.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            uI_Inventory.OnSceneChange();
            characterAbility.OnSceneChange();
            SceneManager.sceneLoaded += character.OnSceneLoaded;
            LevelClear = false;
            SceneManager.LoadScene(SceneIndex);
            Cd.enabled = false;
            yield return new WaitForSeconds(0.1f);

            yield break;
        }

    }


}
