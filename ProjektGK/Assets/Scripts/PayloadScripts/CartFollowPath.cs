
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//author: Paweł Salicki, Adrian Skutela

public class CartFollowPath : MonoBehaviour
{
    public Transform[] target;
    public float speed;
    public PlayerRespawn playerRespawn;

    private int current;

    void FixedUpdate()
    {
        if(Vector3.Distance(transform.position, target[current].position) > 1)
        {
            Vector3 pos = Vector3.MoveTowards(transform.position, target[current].position, speed * Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(pos);
        }
        else
        {
            if (playerRespawn)
            {
                playerRespawn.SetRespawnPoint(target[current]);
            }
            current = (current + 1) % target.Length;
        }
        var targetDirection = target[current].transform.position - transform.position;
        var targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

        if (transform.rotation != targetRotation)
        {
            GetComponent<Rigidbody>().rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
        }
    }
}
