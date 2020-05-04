using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform respawnPoint = null;
    private Health playerHealth = null;

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
            playerHealth.Heal(playerHealth.maxHealth);
        }
    }
    private void Start()
    {
        playerHealth = GetComponent<Health>();
        playerHealth.onDeath.AddListener(Respawn);
    }
}
