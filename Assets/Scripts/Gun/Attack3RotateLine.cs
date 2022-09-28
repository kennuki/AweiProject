using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack3RotateLine : MonoBehaviour
{
    ParticleSystem RotaeLine;
    // Start is called before the first frame update
    void Start()
    {
        RotaeLine = GetComponent<ParticleSystem>();
        StartCoroutine(RandomActive(0.2f));
    }


    private IEnumerator RandomActive(float t)
    {
        while (true)
        {
            
            float s = Random.Range(0, 0.1f);
            if (!Input.GetKey(KeyCode.Mouse0))
            {

                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, Random.Range(0, 360));
                RotaeLine.Play();
                yield return new WaitForSeconds(Random.Range(t - s, t +0.1f - s));
            }

            yield return new WaitForSeconds(Random.Range(t - s, t + 0.1f - s));
        }
    }
}
