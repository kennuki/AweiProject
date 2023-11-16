using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPRecoverQuide : MonoBehaviour
{
    public CharacterAbility characterAbility;
    public GameObject BlackCanvas;
    public GameObject GuideTutor;
    public PlayerBag PlayerBag;
    void Start()
    {
        StartCoroutine(HPLower());
    }

    // Update is called once per frame
    void Update()
    {

    }
    private IEnumerator HPLower()
    {
        while (true)
        {
            
            if (characterAbility.HP / characterAbility.MaxHP < 0.6f)
            {
                foreach (Item item in PlayerBag.inventory.GetItemList())
                {
                    if (item.itemType == Item.ItemType.HealthPotion)
                    {
                        BlackCanvas.SetActive(true);
                        GuideTutor.SetActive(true);
                        Time.timeScale = 0;
                        continue;
                    }

                }
                
            }
            if(BlackCanvas.activeSelf)
            {
                while (true)
                {
                    
                    if (Input.GetKey(KeyCode.Z))
                    {
                        BlackCanvas.SetActive(false);
                        GuideTutor.SetActive(false);
                        Time.timeScale = 1;
                        Destroy(gameObject);
                        yield break;
                    }
                    yield return new WaitForEndOfFrame();
                }

            }
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }
}
