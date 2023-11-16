using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArea : MonoBehaviour
{
    private Animator anim;
    public static int Magic1 = 0;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Magic1 >= 8)
        {
            anim.enabled = true;
            Destroy(this.gameObject, 2f);
        }
    }


}
