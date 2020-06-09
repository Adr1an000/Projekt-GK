using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// author: Paweł Salicki

public class Bomb : MonoBehaviour
{
    public GameObject explosionEffect;
    public GameObject explosionTarget;

    public bool explode = false;

    public string ExplosionSoundName = "ExplosionSound";

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

        FindObjectOfType<SoundManager>().PlayMusic(ExplosionSoundName);

        if (explosionTarget != null)
        {
            Destroy(explosionTarget);
        }

        Destroy(gameObject);
    }
}
