using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMP : MonoBehaviour
{
    public CharacterAbility CA;

    void Update()
    {
        this.transform.localScale = new Vector3((0 + 1 * CA.MP / CA.MaxMP), 1, 1);
    }
}
