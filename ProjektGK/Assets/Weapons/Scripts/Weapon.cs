//author: Adrian Skutela

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    [Tooltip("The end of the barrel where projectiles will be spawned")]
    public Transform barrelEnd;
    [Tooltip("Weapon will face the same direction as rotation reference transform")]
    public Transform rotationReference;
    
    [Header("Stats")]
    public float projectileSpeed;
    public int damage;
    public float fireRate;
    [Range(0f, 1f)]
    public float spread;
    public float range;
    public bool automatic;
    public int numberOfProjectiles = 1;
    public int magazineSize;
    public int ammoPerShot;

    [Header("Appearance")]
    [Tooltip("Projectile that will be spawned with each shot")]
    public Projectile projectile;
    public ParticleSystem muzzleFlash;
    public Sprite Crosshair;
    [Header("Animation")]
    public Animator animator;
    [Tooltip("Manually tweak the speed of the animation. Animation speed is equal to the fire rate times this value")]
    public float animationSpeedMultiplier = 1f;

    [Header("Sounds")]
    public SoundManager soundManager = null;
    public string soundName = "GunshotGun";

    private float timeSinceLastShot = 0f;
    private bool shouldShoot = false;

    private int currentAmmo;
    public int Affiliation { get; set; }
    public bool UseAmmo { get; set; } = true;

    public void PressTrigger()
    {
        shouldShoot = true;
    }

    public void ReleaseTrigger()
    {
        //TODO: add animation
        shouldShoot = false;
    }

    public void Equip()
    {
        gameObject.SetActive(true);
        PlayAnimation("Equip");
    }
    public void Unequip()
    {
        gameObject.SetActive(false);
        PlayAnimation("Unequip");
        shouldShoot = false;
    }

    public bool IsInRange(Transform target)
    {
        return (target.position - barrelEnd.position).sqrMagnitude < range * range;
    }
    public bool HasLineOfSight(Transform target)
    {
        RaycastHit hitInfo = new RaycastHit();
        if (Physics.Linecast(barrelEnd.position, target.position, out hitInfo))
        {
            if (hitInfo.collider.gameObject != target.gameObject)
            {
                return false;
            }
        }
        return true;
    }

    public void Drop()
    {

    }

    public void PickUp()
    {

    }

    public int CurrentAmmo()
    {
        return currentAmmo;
    }

    public bool NeedsReload()
    {
        return currentAmmo < ammoPerShot;
    }
    public int Reload(int ammoCount)
    {
        int previousAmmo = currentAmmo;
        currentAmmo += ammoCount;
        if (currentAmmo > magazineSize)
            currentAmmo = magazineSize;
        return currentAmmo - previousAmmo;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
        currentAmmo = magazineSize;
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
            if (timeSinceLastShot > 1f / fireRate && (currentAmmo >= ammoPerShot || !UseAmmo))
            {
                timeSinceLastShot = 0f;

                PlayAnimation("Shoot", fireRate * animationSpeedMultiplier);
                if (muzzleFlash)
                {
                    muzzleFlash.Play();
                }
                if (UseAmmo)
                    currentAmmo -= ammoPerShot;
                for (int i = 0; i < numberOfProjectiles; i++)
                {
                    ShootProjectile();
                }
                if (soundManager)
                {
                    soundManager.PlayMusic(soundName);
                }
            }
        }
    }

    private bool PlayAnimation(string name, float speedMult = 1f)
    {
        if (animator == null)
            return false;

        animator.SetFloat(name + "Speed", speedMult);
        animator.SetTrigger(name);
        return true;
    }

    private void ShootProjectile()
    {
        Vector3 direction = barrelEnd.forward;
        direction += Random.Range(-spread, spread) * barrelEnd.right;
        direction += Random.Range(-spread, spread) * barrelEnd.up;
        Projectile spawnedProjectile = Instantiate(projectile, barrelEnd.position, Quaternion.LookRotation(direction));
        spawnedProjectile.Damage = damage;
        spawnedProjectile.Affiliation = Affiliation; //TODO: implement affiliation
        spawnedProjectile.Velocity = direction.normalized * projectileSpeed;
        spawnedProjectile.Lifetime = range / projectileSpeed;
    }
}
