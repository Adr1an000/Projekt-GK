using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// author Dawid Musialik

public class FallDownPlayer : MonoBehaviour
{
    public Transform respawnPosition;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.transform.position = respawnPosition.position;
        }
    }
}
