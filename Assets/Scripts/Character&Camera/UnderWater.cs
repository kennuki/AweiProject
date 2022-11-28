using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderWater : MonoBehaviour
{
    public GameObject CameraPlaneFilter;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "UnderWater")
        {
            CameraPlaneFilter.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "UnderWater")
        {
            CameraPlaneFilter.SetActive(false);
        }
    }
}
