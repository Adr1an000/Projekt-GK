//author: Adrian Skutela, Dawid Musialik

using System.Collections;
using System.Collections.Generic;
using System.Globalization;

using UnityEngine;
using UnityEngine.UI;

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
    public GameObject respawnScreen;
    private Text respawnTimerText;

    public void SetRespawnPoint(Transform newPoint)
    {
        Debug.Log(newPoint);
        respawnPoint = newPoint;
    }
    public void Respawn()
    {
        if (respawnPoint != null)
        {
            GameObject s = GameObject.Find("Stats");
            if (s != null)
                s.GetComponent<Stats>().increment_deaths();//new

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
        respawnScreen.SetActive(false);
        playerHealth = GetComponent<Health>();
        playerHealth.onDeath.AddListener(Respawn);
        weaponManager = GetComponent<WeaponManager>();
        respawnTimerText = respawnScreen.transform.GetChild(0).gameObject.GetComponent<Text>();
    }
    //After respawn, sets player invincibile for given time.
    IEnumerator SetPlayerInvincivile()
    {
        
        GetComponent<Health>().invincible = true;
        respawnScreen.SetActive(true);
        for(int i=0; i<invincibileTime; i++)
        {
            respawnTimerText.text=(invincibileTime-i).ToString();
            yield return new WaitForSeconds(1);
        }
        GetComponent<Health>().invincible = false;
        respawnScreen.SetActive(false);
    }
}
