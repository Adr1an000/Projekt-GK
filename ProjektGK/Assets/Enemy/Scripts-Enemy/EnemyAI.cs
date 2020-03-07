using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    public int Team { get; private set; } = 1; // team (enemy = 1 or player = 0)

    [SerializeField]
    private GameObject Weapon; /// TODO

    public Transform Target { get; private set; } // attack target

    public EnemyStateMachine EnemyStateMachine => GetComponent<EnemyStateMachine>(); // enemy state machine behaviour
   
    // init enemy
    private void Awake()
    {
        InitStateMachine(); // initialise enemy's state machine
    }

    private void InitStateMachine()
    {
        var states = new Dictionary<Type, EnemyBaseState>()
        {
        
            { typeof(PatrolState), new PatrolState(this)}

        };
        GetComponent<EnemyStateMachine>().SetState(states);
    }

    // lock target
    public void SetTarget(Transform _target)
    {
        Target = _target;
    }

    // fire from weapon
    public void FireWeapon(bool _fire)
    {
        /// TODO
    }
}
