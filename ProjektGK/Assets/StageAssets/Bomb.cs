using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosionEffect;

    public bool explode = false;
    private bool alreadyExploding = false;

    void Start()
    {

    }


    void Update()
    {
        if (explode && !alreadyExploding)
        {
            Explode();

            alreadyExploding = true;
        }
    }

    private void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
