using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// author: Paweł Salicki

public class bossUpDownMovement : MonoBehaviour
{
    float speed = 2.5f;

    float height = 1f;

    Vector3 pos;

    void Start()
    {
        pos = transform.position;
    }

    void Update()
    {
        float newY = Mathf.Sin(Time.time * speed) + pos.y;

        transform.position = new Vector3(pos.x, newY * height, pos.z);
    }
}
