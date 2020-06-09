//author: Adrian Skutela

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int amount = 50;

    public void PickUp(GameObject player)
    {
        var health = player.GetComponent<Health>();
        if (health)
        {
            if (health.currentHealth < health.maxHealth)
            {
                health.Heal(amount);
                GetComponent<Respawnable>().Respawn();
                Destroy(gameObject);
            }
        }
    }
}
