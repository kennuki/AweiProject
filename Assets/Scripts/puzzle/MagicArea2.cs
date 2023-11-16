using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArea2 : MonoBehaviour
{
    private Animator anim;
    public static int Magic2 = 0;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Magic2 >= 8)
        {
            anim.enabled = true;
            Destroy(this.gameObject, 2f);
        }

    }
}
