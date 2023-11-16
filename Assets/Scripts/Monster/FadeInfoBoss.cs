using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInfoBoss : MonoBehaviour
{
    Text Hit;

    void Start()
    {
        Destroy(this.gameObject, 2f);
        Hit = this.GetComponent<Text>();
        Hit.color = new Color(0.96f, 0.91f, 0.56f, 1);
        Hit.text = Mathf.Floor(BossAblity.HitNum).ToString();
        StartCoroutine(FadeText());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator FadeText()
    {
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            Hit.color = new Color(0.96f, 0.91f, 0.56f, i);
            yield return null;
        }
    }
}
