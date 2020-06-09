//author: Dawid Musialik

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// author Dawid Musialik

public class Barrel : MonoBehaviour
{
    public GameObject explosionEffect;

    public bool explode = false;

    public string ExplosionSoundName = "ExplosionSound";

    private bool alreadyExploding = false;

    public float radious = 8f;

    public int damage = 50;

    private int affiliation = 5;

    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Projectile")
        {
            explode = true;
        }
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
        //Barrel explodes
        Instantiate(explosionEffect, transform.position, transform.rotation);
        FindObjectOfType<SoundManager>().PlayMusic(ExplosionSoundName);
        Destroy(gameObject);

        
        Collider[] barrelsToExplode = Physics.OverlapSphere(transform.position, radious);
      
       foreach (Collider nearbyObject in barrelsToExplode)
       {
            //Nearby barrels explodes
            Barrel b = nearbyObject.GetComponent<Barrel>();
            if (b != null)
                b.explode = true ;

            //Damage player and enemies
            Health h = nearbyObject.GetComponent<Health>();
            if (h != null)
                h.DealDamage(damage, affiliation);
                  
        }   
    }
}
