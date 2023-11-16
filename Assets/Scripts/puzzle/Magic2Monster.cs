using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic2Monster : MonoBehaviour
{
    private HedgeHogAbility ability;
    private void Start()
    {
        ability = GetComponentInChildren<HedgeHogAbility>();
    }
    private void Update()
    {
        if (ability.HP <= 0)
        {
            MagicArea2.Magic2 += 1;
            Destroy(this);
        }
    }
}
