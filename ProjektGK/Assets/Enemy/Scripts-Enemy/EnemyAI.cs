using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class EnemyAI : MonoBehaviour
{
    // hidden from gui Unity
    public bool IsHiding { get; set; } = false; // enemy near "COVER" object

    public GameObject PlayerTarget { get; private set; } // player object

    public bool CoverIsClose { get; set; } // is cover in reach?

    public bool CoverNotReached { get; set; } = true; // if true, AI is not close enough to the cover object

    public EnemyStateMachine EnemyStateMachine => GetComponent<EnemyStateMachine>(); // enemy state machine behaviour

    // show to gui Unity

    [SerializeField]
    public int FacePlayerFactor = 50; // face player factor


    [SerializeField]
    public float PatrolSpeed  = 2.0f; // speed of patrol movement

    [SerializeField]
    public float MinDistanceFromPlayer = 30.0f; // min distance from player to take action

    [SerializeField]
    public float TurnSpeed = 2.0f; // speed of turn

    [SerializeField]
    public float AttackRange = 10.0f; // attack range

    [SerializeField]
    public float DetectionRange = 10.0f; // player distance detection

    [SerializeField]
    public float ObstacleReverseRange = 2.0f; // obstacle detection range

    [SerializeField]
    public NavMeshAgent AgentPath; // path

    [SerializeField]
    public Weapon Weapon;

    [SerializeField]
    public Animator Anim;

    [SerializeField]
    public LayerMask layerMask; // layer mask

    // init enemy
    private void Awake()
    {
        InitStateMachine(); // initialise enemy's state machine

        if(!PlayerTarget)
        {
            PlayerTarget = GameObject.Find("Player");
        }

        if(!Anim)
        {
            Anim = GetComponent<Animator>();
        }

        if(!AgentPath)
        {
            AgentPath = GetComponent<NavMeshAgent>();
        }

        if(!Weapon)
        {
            Weapon = GetComponentInChildren<Weapon>();
        }

        AgentPath.updateRotation = true;
    }

    // init behaviour state machine
    private void InitStateMachine()
    {
        var states = new Dictionary<Type, EnemyBaseState>()
        {
            {typeof(SpawnState), new SpawnState(this) }, // spawn state
            { typeof(PatrolState), new PatrolState(this)}, // patrol state
            {typeof(ChaseState), new ChaseState(this) }, // chase player state
            { typeof(AttackState), new AttackState(this) } // attack player state

        };
        GetComponent<EnemyStateMachine>().SetState(states);
    }
}
