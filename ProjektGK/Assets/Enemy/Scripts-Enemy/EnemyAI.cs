using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private GameObject Weapon; /// TODO

    [SerializeField]
    public NavMeshAgent AgentPath;

    public GameObject PlayerTarget { get; private set; }

    public Transform Target { get; set; } // attack target

    public EnemyStateMachine EnemyStateMachine => GetComponent<EnemyStateMachine>(); // enemy state machine behaviour
   
    // init enemy
    private void Awake()
    {
        InitStateMachine(); // initialise enemy's state machine

        PlayerTarget = GameObject.Find("Player");
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
