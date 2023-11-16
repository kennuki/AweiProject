using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour
{
    public GameObject Target;
    public HedgeHogAbility TargetHedgehog;
    public Image Bar;
    public GameObject canvas;
    public GameObject HpBar;
    void Start()
    {
        HpBar.SetActive(false);
        StartCoroutine(imagetocanvas());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdatePosition();
        UpdateBarWidth();
        HpBar.SetActive(TargetHedgehog.deadbydaylight);
    }
    // 更新血條座標
    void UpdatePosition()
    {
        Vector3 enemyScreenPos = Camera.main.WorldToScreenPoint(Target.transform.position);
        Vector3 offset = new Vector3(0, 230, 0);
        transform.position = enemyScreenPos + offset;
    }

    // 更新血條長度
    void UpdateBarWidth()
    {
        float hpScale = (TargetHedgehog.HP / TargetHedgehog.MaxHP);
        Bar.rectTransform.localScale = new Vector3(
            hpScale,
            Bar.rectTransform.localScale.y,
            Bar.rectTransform.localScale.z);
        if (TargetHedgehog.HP <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    private IEnumerator imagetocanvas()
    {
        while (true)
        {
            canvas = GameObject.Find("Canvas2");
            if (canvas != null)
            {
                transform.SetParent(canvas.transform);
                yield break;
            }
            else if (canvas == null)
            {
                yield return new WaitForSeconds(0.1f);
                canvas = GameObject.Find("Canvas2");
            }
        }
        
    }
}
