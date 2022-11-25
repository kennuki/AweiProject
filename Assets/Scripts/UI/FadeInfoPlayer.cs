using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInfoPlayer : MonoBehaviour
{
    Text Hit;

    void Start()
    {
        Destroy(this.gameObject, 2f);
        Hit = this.GetComponent<Text>();
        Hit.color = new Color(0.8584f, 0f, 0f, 0.42f);
        Hit.text = Mathf.Floor(CharacterAbility.HitNumPlayer).ToString();
        StartCoroutine(FadeText());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0.5f, 0);
    }
    IEnumerator FadeText()
    {
        for (float i = 0.42f; i >= 0; i -= Time.deltaTime*0.6f)
        {
            Hit.color = new Color(0.8584f, 0f, 0f, i);
            yield return null;

        }
    }
}