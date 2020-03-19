using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float elapsedTime = 0f;

    public Vector3 Velocity { get; set; }
    public float Lifetime { get; set; }
    public int Damage { get; set; }
    public int Affiliation { get; set; }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        //if other object is a weaon or a projectile ignore the collision
        if (collision.gameObject.GetComponent<Weapon>() != null ||
            collision.gameObject.GetComponentInParent<Weapon>() != null ||
            collision.gameObject.GetComponent<Projectile>() != null ||
            collision.gameObject.GetComponentInParent<Projectile>() != null)
        {
            return;
        }
        Health health = collision.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.DealDamage(Damage, Affiliation);
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Velocity * Time.deltaTime;
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= Lifetime)
        {
            Destroy(gameObject);
        }
    }
}
