using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveMeDestroyAllChild : MonoBehaviour
{
    public void DestroyAllChild()
    {
        foreach(Transform TTT in this.gameObject.transform)
        {
            Destroy(TTT.gameObject);
        }
    }
}
