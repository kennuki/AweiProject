using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class renderfade : MonoBehaviour
{
    MeshRenderer meshRenderer;
    float emission;
    float Speed;
    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        emission = meshRenderer.material.GetFloat("_emission");
        Speed = emission / 2;
    }
    private void Update()
    {
        emission -= Time.deltaTime*Speed;
        if (emission >= 0)
        {
            meshRenderer.material.SetFloat("_emission", emission);
        }
        else
        {  
            Destroy(this);
        }
    }
}
