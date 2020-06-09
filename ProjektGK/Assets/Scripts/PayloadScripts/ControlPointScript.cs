using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// author Dawid Musialik

public class ControlPointScript : MonoBehaviour
{
    public int TimeToAdd = 120;
    public GameObject Timer;
    public Light LightPoint;


    void Start()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Payload")
        {
            Timer.GetComponent<TimerPush>().AddTime(120);
            LightPoint.color = Color.green;

            Destroy(gameObject);
        }
    }


    void Update()
    {
        
    }
}
