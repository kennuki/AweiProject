using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vineglow : MonoBehaviour
{
    Animator anim;
    Renderer rend;
    public VineUnlock vineUnlock;
    public int UnlockOrder;
    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();
        rend = GetComponent<Renderer>();
        anim.enabled = false;
        StartCoroutine(VineUnlock());
    }
    float x = 1.8f;
    float i;
    public float rate = 0.02f;
    float rate_ = 0.02f;
    public float max = 4f;
    public float min = 1.5f;
    // Update is called once per frame
    void Update()  
    {
        rend.material.SetFloat("_emission", x);
        i = (Mathf.Abs(Mathf.Abs(x - 1) - 0.1f)) * rate_ + 0.01f * rate_;
        x += i;
        if (x < min)
        {
            rate_ = Random.Range(rate_ + 0.3f * rate_, rate_ - 0.3f * rate_);
            rate_ = Mathf.Clamp(rate_, -min, min);
            rate_ = rate;
            
        }
        else if (x > max)
        {
            rate_ = -rate;
        }
    }
    private IEnumerator VineUnlock()
    {
        while (true)
        {
            if (vineUnlock.UnlockVineAllow == UnlockOrder)
            {           
                anim.enabled = true;
                anim.Play("VineMove");
                Destroy(this.gameObject, 3f);
                yield break;
            }
            else
            {
                yield return new WaitForSeconds(0.01f);
            }

        }

    }
}
