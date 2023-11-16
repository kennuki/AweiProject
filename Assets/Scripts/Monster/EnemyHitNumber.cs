using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHitNumber : MonoBehaviour
{
    public GameObject canvas;
    Vector3 RandonPosition;
    void Start()
    {
        RandonPosition = new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), 0);
        imagetocanvas();
        UpdatePosition();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0.5f, 0);
    }
    void UpdatePosition()
    {
            Vector3 enemyScreenPos = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 offset = new Vector3(1 + RandonPosition.x, 250 + RandonPosition.y, 0);
            transform.position = enemyScreenPos + offset;
    }
    void imagetocanvas()
    {
        //îcimageï˙ìûcanvasè„
        canvas = GameObject.Find("Canvas2");
        if (canvas != null)
            transform.SetParent(canvas.transform);
    }
}
