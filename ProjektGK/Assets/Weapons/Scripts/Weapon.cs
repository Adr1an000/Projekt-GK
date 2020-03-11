using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    [Tooltip("The end of the barrel where projectiles and effects will be spawned")]
    public Transform barrelEnd;

    [Tooltip("Projectile that will be spawned with each shot")]
    public Projectile projectile;
    public float projectileSpeed;
    public int damage;
    public int fireRate;
    [Range(0f, 1f)]
    public float spread;
    public float range;
    public bool automatic;
    
    private float timeSinceLastShot = 0f;
    private bool shouldShoot = false;

    public void PressTrigger()
    {
        shouldShoot = true;
    }

    public void ReleaseTrigger()
    {
        shouldShoot = false;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        shouldShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        if(shouldShoot)
        {
            if (!automatic)
            {
                shouldShoot = false;
            }
            if (timeSinceLastShot > 1f / fireRate)
            {
                timeSinceLastShot = 0f;
                ShootProjectile();
            }
        }
    }

    private void ShootProjectile()
    {
        Vector3 direction = barrelEnd.forward;
        direction.x += Random.Range(-spread, spread);
        direction.y += Random.Range(-spread, spread);
        Projectile spawnedProjectile = Instantiate(projectile, barrelEnd.position, Quaternion.LookRotation(direction));
        spawnedProjectile.Damage = damage;
        spawnedProjectile.Affiliation = 0; //TODO: implement affiliation
        spawnedProjectile.Velocity = direction.normalized * projectileSpeed;
        spawnedProjectile.Lifetime = range / projectileSpeed;
    }
}
