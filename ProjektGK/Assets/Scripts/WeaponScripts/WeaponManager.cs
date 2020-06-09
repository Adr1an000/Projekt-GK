//author: Adrian Skutela

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    public List<Weapon> startingWeapons;
    public int slotCount;
    public List<Weapon> weapons { get; } = new List<Weapon>();
    private int currentWeapon = -1;
    private int affiliation;

    public int ammo = 100;
    public bool unlimitedAmmo = false;
    public bool weaponsUseAmmo = true;

    public Text current;
    public Text rest;
    public Image crosshair;
    public Sprite defaultCrosshair;

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
        UpdateCrosshairUI();
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

    public bool NeedsReload()
    {
        return GetCurrentWeapon().NeedsReload();
    }

    public void Reload()
    {
        ammo -= GetCurrentWeapon().Reload(ammo);
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
                startingWeapons[i].UseAmmo = weaponsUseAmmo;
                weapons.Add(startingWeapons[i]);
            }
            else
                weapons.Add(null);
        }
        Debug.Log( SetCurrentWeapon(0) );
    }

    void UpdateAmmoUI()
    {
        if (current)
        {
            current.text = $"{GetCurrentWeapon().CurrentAmmo()}";
        }
        if (rest)
        {
            rest.text = $"{ammo}";
        }
    }
    void UpdateCrosshairUI()
    {
        if (crosshair)
        {
            if (GetCurrentWeapon().Crosshair)
            {
                crosshair.sprite = GetCurrentWeapon().Crosshair;
            }
            else
            {
                crosshair.sprite = defaultCrosshair;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (unlimitedAmmo && ammo < 1000)
        {
            ammo = 1000;
        }
        UpdateAmmoUI();
    }
}
