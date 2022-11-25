using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroy : MonoBehaviour
{
    public float DieDelayTime = 0;
    void Start()
    {
        Destroy(this.gameObject, 2f);
    }
}
