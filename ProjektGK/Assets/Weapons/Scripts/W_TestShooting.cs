using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class W_TestShooting : MonoBehaviour
{
    public Weapon weapon;
    // Start is called before the first frame update
    void Start()
    {
        if (!weapon)
        {
            weapon = GetComponentInChildren<Weapon>();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            if (weapon)
            {
                weapon.PressTrigger();
            }
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            if (weapon)
            {
                weapon.ReleaseTrigger();
            }
        }
    }
}
