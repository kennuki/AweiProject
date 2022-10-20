using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMP : MonoBehaviour
{
    public CharacterAbility CA;

    void Update()
    {
        this.transform.localScale = new Vector3((0 + 1.168f * CA.MP / CA.MaxMP), 0.69f, 1);
    }
}
