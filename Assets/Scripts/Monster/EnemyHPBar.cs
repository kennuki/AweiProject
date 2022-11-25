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
        imagetocanvas();
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
        Vector3 offset = new Vector3(0, 145, 0);
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
    void imagetocanvas()
    {
        //把image放到canvas上
        canvas = GameObject.Find("Canvas2");
        transform.SetParent(canvas.transform);
    }
}
