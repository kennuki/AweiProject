using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic1Monster : MonoBehaviour
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
            MagicArea.Magic1 += 1;
            Destroy(this);
        }
    }
}
