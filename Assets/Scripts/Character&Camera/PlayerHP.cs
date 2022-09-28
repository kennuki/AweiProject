using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public CharacterAbility CA;

    void Update()
    {
        this.transform.localScale = new Vector3((0 + 1 * CA.HP / CA.MaxHP), 1, 1);
    }
}
