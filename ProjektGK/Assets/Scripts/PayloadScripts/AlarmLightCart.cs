using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// author: Paweł Salicki

public class AlarmLightCart : MonoBehaviour
{
    Light light;
    public float minWaitTime;
    public float maxWaitTime;

    void Start()
    {
        light = GetComponent<Light>();
        StartCoroutine(Flashing());
    }

    IEnumerator Flashing()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            light.enabled = !light.enabled;
        }
    }
}
