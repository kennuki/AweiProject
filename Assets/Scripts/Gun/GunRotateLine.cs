using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotateLine : MonoBehaviour
{
    ParticleSystem RotaeLine;
    // Start is called before the first frame update
    void Start()
    {
        RotaeLine = GetComponent<ParticleSystem>();
    }
    float c;
    private void Update()
    {
        c+= Time.deltaTime;
        if (c > 5)
        {
            StartCoroutine(RandomActive(3));
            c = 0;
        }
    }


    private IEnumerator RandomActive(float t)
    {
        while (true)
        {
            c = 0;
            int s = Random.Range(-1, 2);
            if (!Input.GetKey(KeyCode.Mouse0))
            {

                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, Random.Range(0, 360));
                RotaeLine.Play();
                yield return new WaitForSeconds(Random.Range(t - s, t + 1 - s));
            }

            yield return new WaitForSeconds(Random.Range(t-s, t + 1-s));
        }
    }

}
