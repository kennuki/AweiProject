using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class renderfade : MonoBehaviour
{
    MeshRenderer meshRenderer;
    float emission;
    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        emission = meshRenderer.material.GetFloat("_emission");
    }
    private void Update()
    {
        emission -= Time.deltaTime*2;
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
