using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public CharacterAbility CA;

    void Update()
    {
        this.transform.localScale = new Vector3((0 + 1.168f * CA.HP / CA.MaxHP), 0.69f, 1);
    }
}
