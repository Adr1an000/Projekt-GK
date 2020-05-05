using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform respawnPoint = null;
    public int restoreAmmo = 100;
    public int restoreHealth = 200;
    private Health playerHealth = null;
    private WeaponManager weaponManager = null;

    public void SetRespawnPoint(Transform newPoint)
    {
        Debug.Log(newPoint);
        respawnPoint = newPoint;
    }
    public void Respawn()
    {
        if (respawnPoint != null)
        {
            Debug.Log(respawnPoint.position);
            GetComponent<CharacterController>().enabled = false;
            transform.position = respawnPoint.position;
            GetComponent<CharacterController>().enabled = true;
            playerHealth.Heal(restoreHealth);
            weaponManager.ammo += restoreAmmo;
        }
    }
    private void Start()
    {
        playerHealth = GetComponent<Health>();
        playerHealth.onDeath.AddListener(Respawn);
        weaponManager = GetComponent<WeaponManager>();
    }
}
