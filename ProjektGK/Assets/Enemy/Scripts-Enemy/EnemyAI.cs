using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    public int Team; // team (enemy = 1 or player = 0)

    [SerializeField]
    private GameObject Weapon; /// TODO

    [SerializeField]
    public NavMeshAgent agent;

    [SerializeField]
    public GameObject playerTarget;

    public Transform Target { get; set; } // attack target

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
        
            { typeof(PatrolState), new PatrolState(this)},
            {typeof(ChaseState), new ChaseState(this) }

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
