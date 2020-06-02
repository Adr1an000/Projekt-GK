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
    public CartFollowPath cartFollowPath;
    public int invincibileTime = 3;

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
            cartFollowPath.speed = 0;
            StartCoroutine(SetPlayerInvincivile());
        }
    }
    private void Start()
    {
        playerHealth = GetComponent<Health>();
        playerHealth.onDeath.AddListener(Respawn);
        weaponManager = GetComponent<WeaponManager>();
    }
    //After respawn, sets player invincibile for given time.
    IEnumerator SetPlayerInvincivile()
    {
        //Można tutaj wrzucić wyświetlanie ekranu nieśmiertelności.
        GetComponent<Health>().invincible = true;
        yield return new WaitForSeconds (invincibileTime);
        GetComponent<Health>().invincible = false;
    }
}
