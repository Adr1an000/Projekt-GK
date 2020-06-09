using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// author Dawid Musialik

public class PushZone : MonoBehaviour
{
    public CartFollowPath cartFollowPath;

    public float CartSpeed = 10;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            cartFollowPath.speed = CartSpeed;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            cartFollowPath.speed = 0;
        }
    }
}
