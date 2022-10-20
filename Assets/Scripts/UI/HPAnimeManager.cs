using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPAnimeManager : MonoBehaviour
{
    public GameObject[] HPAnime = new GameObject[3];
    public CharacterAbility CA;
    public Image black;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (CA.HP / CA.MaxHP < 0.7f && CA.HP / CA.MaxHP >= 0.5f)
        {
            black.material.SetColor("_BaseColor", new Color(0, 0, 0, 0.90f + Mathf.Sin(Time.time * 180 * Mathf.Deg2Rad) * 0.025f));
            HPAnime[0].SetActive(true);
            HPAnime[1].SetActive(false);
            HPAnime[2].SetActive(false);
        }
        else if (CA.HP / CA.MaxHP < 0.5f && CA.HP / CA.MaxHP >= 0.2f)
        {
            black.material.SetColor("_BaseColor", new Color(0.1f+ Mathf.Sin(Time.time * 180 * Mathf.Deg2Rad) * 0.05f, 0, 0, 0.85f + Mathf.Sin(Time.time * 230 * Mathf.Deg2Rad) * 0.08f));
            HPAnime[0].SetActive(false);
            HPAnime[1].SetActive(true);
            HPAnime[2].SetActive(false);
        }
        else if (CA.HP / CA.MaxHP < 0.2f && CA.HP / CA.MaxHP >= 0f)
        {
            black.material.SetColor("_BaseColor", new Color(0.2f + Mathf.Sin(Time.time * 180 * Mathf.Deg2Rad) * 0.08f, 0, 0, 0.8f + Mathf.Sin(Time.time * 270 * Mathf.Deg2Rad) * 0.15f));
            HPAnime[0].SetActive(false);
            HPAnime[1].SetActive(false);
            HPAnime[2].SetActive(true);
        }
        else
        {
            HPAnime[0].SetActive(false);
            HPAnime[1].SetActive(false);
            HPAnime[2].SetActive(false);
        }


    }
}
