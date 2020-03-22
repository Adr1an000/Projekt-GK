using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public List<Weapon> startingWeapons;
    public int slotCount;
    private List<Weapon> weapons = new List<Weapon>();
    private int currentWeapon = -1;
    private int affiliation;

    public Weapon GetCurrentWeapon()
    {
        if (currentWeapon < 0 || currentWeapon >= weapons.Count)
            return null;
        return weapons[currentWeapon];
    }

    // Start is called before the first frame update

    public bool SetCurrentWeapon(int slot)
    {
        if (slot >= weapons.Count || slot < 0 || weapons[slot] == null)
            return false;
        if (slot != currentWeapon)
        {
            if (GetCurrentWeapon() != null)
                GetCurrentWeapon().Unequip();
            currentWeapon = slot;
            GetCurrentWeapon().Equip();
        }
        return true;
    }

    public bool AddWeapon(Weapon weapon, int slot = -1, bool replace = false)
    {
        if (slot < 0)
            slot = FindFreeSlot();
        if (slot >= weapons.Count || slot < 0)
            return false;
        if (weapons[slot] != null)
        {
            if (replace)
            {
                GetCurrentWeapon().Drop();
                weapon.PickUp();
            }
            else
                return false;
        }
        weapon.Affiliation = affiliation;
        weapons[slot] = weapon;
        return true;
    }

    public int FindFreeSlot()
    {
        for (int i = 0; i < weapons.Count; i++)
            if (weapons[i] == null)
                return i;
        return -1;
    }
    void Start()
    {
        Health health = GetComponentInParent<Health>();
        if (health != null)
            affiliation = health.affiliation;
        for (int i = 0; i < slotCount; i++)
        {
            if (i < startingWeapons.Count)
            {
                startingWeapons[i].Affiliation = affiliation;
                weapons.Add(startingWeapons[i]);
            }
            else
                weapons.Add(null);
        }
        Debug.Log( SetCurrentWeapon(0) );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
