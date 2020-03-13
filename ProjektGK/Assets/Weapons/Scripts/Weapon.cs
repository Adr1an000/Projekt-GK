using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    [Tooltip("The end of the barrel where projectiles will be spawned")]
    public Transform barrelEnd;
    [Tooltip("Weapon will face the same direction as rotation reference transform")]
    public Transform rotationReference;

    [Tooltip("Projectile that will be spawned with each shot")]
    public Projectile projectile;
    public float projectileSpeed;
    public int damage;
    public int fireRate;
    [Range(0f, 1f)]
    public float spread;
    public float range;
    public bool automatic;
    public int numberOfProjectiles = 1;
    public Animator recoilAnimation;
    [Tooltip("Manually tweak the speed of the animation. Animation speed is equal to the fire rate times this value")]
    public float animationSpeedMultiplier = 1f;
    public ParticleSystem muzzleFlash;
    
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
    }

    // Update is called once per frame
    void Update()
    {
        if (rotationReference)
        {
            transform.rotation = rotationReference.rotation;
        }
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
                if (recoilAnimation)
                {
                    recoilAnimation.speed = fireRate * animationSpeedMultiplier;
                    recoilAnimation.Play("RecoilAnim");
                }
                if (muzzleFlash)
                {
                    muzzleFlash.Play();
                }
                for (int i = 0; i < numberOfProjectiles; i++)
                {
                    ShootProjectile();
                }
            }
        }
    }

    private void ShootProjectile()
    {
        Vector3 direction = barrelEnd.forward;
        direction += Random.Range(-spread, spread) * barrelEnd.right;
        direction += Random.Range(-spread, spread) * barrelEnd.up;
        Projectile spawnedProjectile = Instantiate(projectile, barrelEnd.position, Quaternion.LookRotation(direction));
        spawnedProjectile.Damage = damage;
        spawnedProjectile.Affiliation = 0; //TODO: implement affiliation
        spawnedProjectile.Velocity = direction.normalized * projectileSpeed;
        spawnedProjectile.Lifetime = range / projectileSpeed;
    }
}
