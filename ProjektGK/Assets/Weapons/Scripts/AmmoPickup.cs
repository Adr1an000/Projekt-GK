//author: Adrian Skutela

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int amount = 50;

    public void PickUp(GameObject player)
    {
        var weaponMngr = player.GetComponent<WeaponManager>();
        if (weaponMngr)
        {
            weaponMngr.ammo += amount;
            GetComponent<Respawnable>().Respawn();
            Debug.Log(GetComponent<Respawnable>());
            Destroy(gameObject);
        }
    }
}
