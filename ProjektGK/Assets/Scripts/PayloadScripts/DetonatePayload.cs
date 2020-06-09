using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// author Dawid Musialik

public class DetonatePayload : MonoBehaviour
{
    public GameObject payloadToExplode;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Payload")
        {
            payloadToExplode.GetComponent<Bomb>().explode = true;
        }
    }
}
