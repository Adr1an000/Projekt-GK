using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    // hidden from gui Unity
    public bool IsHiding { get; set; } = false; // enemy near "COVER" object

    public GameObject PlayerTarget { get; private set; } // player object

    public Transform Target { get; set; } // attack target position

    public bool CoverIsClose { get; set; } // is cover in reach?

    public bool CoverNotReached { get; set; } = true; // if true, AI is not close enough to the cover object

    public EnemyStateMachine EnemyStateMachine => GetComponent<EnemyStateMachine>(); // enemy state machine behaviour

    // show to gui Unity
    [SerializeField]
    public int FrameInterval = 10; // every how many frames update behaviour

    [SerializeField]
    public int FacePlayerFactor = 50; // face player factor

    [SerializeField]
    public float RangeRandPoint = 6.0f; // max traveling distance behind the cover

    [SerializeField]
    public float DistToCoverObj = 20.0f; // distance to cover object

    [SerializeField]
    public float DistToCoverPos = 1.0f; // distance to cover position

    [SerializeField]
    public float PatrolSpeed  = 2.0f; // speed of patrol movement

    [SerializeField]
    public float MinDistanceFromPlayer = 30.0f; // min distance from player to take action

    [SerializeField]
    public float ChaseSpeed = 4.0f; // speed of chase player

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
    public LayerMask CoverLayer; // to set the layer that should be used as cover

    [SerializeField]
    public LayerMask VisibleLayer; // to declare objects on layer that might obstruct the view between AI and target/player

    [SerializeField]
    private GameObject Weapon; /// TODO

    // init enemy
    private void Awake()
    {
        InitStateMachine(); // initialise enemy's state machine

        PlayerTarget = GameObject.Find("Player"); // player !!!
    }

    // init behaviour state machine
    private void InitStateMachine()
    {
        var states = new Dictionary<Type, EnemyBaseState>()
        {
            { typeof(PatrolState), new PatrolState(this)}, // patrol state
            {typeof(ChaseState), new ChaseState(this) }, // chase player state
            { typeof(AttackState), new AttackState(this) } // attack player state

        };
        GetComponent<EnemyStateMachine>().SetState(states);
    }

    // lock target
    public void SetTarget(Transform _target)
    {
        Target = _target;
    }
}
