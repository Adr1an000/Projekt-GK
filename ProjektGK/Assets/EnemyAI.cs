using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyAI : MonoBehaviour
{
    // this should be in it...
    [SerializeField]
    public int Team; // enemy/player

    [SerializeField]
    private GameObject Weapon; // weapon

    public Transform Target { get; private set; } // attack target

    public EnemyStateMachine EnemyStateMachine => GetComponent<EnemyStateMachine>(); // enemy interactions

   
    public AISettings aISettings;
   

    // Another weapons ?
    /*
    [SerializeField]
    private NPCWeaponSystem weaponSystem;
    */

    // init stuff
    private void Awake()
    {
        InitStateMachine(); // initialise enemy's state machine

     //   weaponSystem = GetComponentInChildren<NPCWeaponSystem>(); // weapon system
    }

    private void InitStateMachine()
    {
        var states = new Dictionary<Type, EnemyBaseState>()
        {
        
            { typeof(PatrolState), new PatrolState(this)}

        };
        GetComponent<EnemyStateMachine>().SetState(states);
    }

    public void SetTarget(Transform _target) // target lock
    {
        Target = _target;
    }

    public void FireWeapon(bool _fireState) // FIRE 
    {
       // weaponSystem.FireWeaponSlave(_fireState);
    }
}
