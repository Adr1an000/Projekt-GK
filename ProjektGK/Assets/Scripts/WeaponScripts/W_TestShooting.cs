//author: Adrian Skutela

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_TestShooting : MonoBehaviour
{
    //public Weapon weapon;
    public WeaponManager manager;
    public Dictionary<KeyCode, int> keyMap = new Dictionary<KeyCode, int>();
    // Start is called before the first frame update
    void Start()
    {
        keyMap.Add(KeyCode.Alpha1, 0);
        keyMap.Add(KeyCode.Alpha2, 1);
        keyMap.Add(KeyCode.Alpha3, 2);
        if (!manager)
        {
            manager = GetComponentInChildren<WeaponManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            if (manager)
            {
                manager.GetCurrentWeapon().PressTrigger();
            }
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            if (manager)
            {
                manager.GetCurrentWeapon().ReleaseTrigger();
            }
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            if (manager)
            {
                manager.Reload();
            }
        }
        else
        {
            foreach (var pair in keyMap)
            {
                if (Input.GetKeyDown(pair.Key))
                {
                    manager.SetCurrentWeapon(pair.Value);
                }
            }
        }
    }
}
