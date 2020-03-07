using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float elapsedTime = 0f;

    public Vector3 Velocity { get; set; }
    public float Lifetime { get; set; }
    public float Damage { get; set; }
    public int Affiliation { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
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
