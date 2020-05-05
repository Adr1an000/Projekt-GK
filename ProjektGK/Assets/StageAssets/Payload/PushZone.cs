using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushZone : MonoBehaviour
{
    public CartFollowPath cartFollowPath;

    public float CartSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    /*void Update()
    {
        if (cartFollowPath.speed > 0)
        {
            cartFollowPath.speed -= Time.deltaTime;
        }
        else
        {
            cartFollowPath.speed = 0;
        }
    }*/

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
