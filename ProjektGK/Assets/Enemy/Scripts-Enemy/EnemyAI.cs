using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    //******************

    public int frameInterval = 10;
    public int facePlayerFactor = 50; // for facing the player

    public float rangeRandPoint = 6f;
    public bool isHiding = false;

    public bool coverIsClose; // is cover in reach?
    public bool coverNotReached = true; // if true, AI is not close enough to the cover object
    public float distToCoverObj = 20f; // 60f
    public float distToCoverPos = 1f;

    //*******************


    [SerializeField]
    private GameObject Weapon; /// TODO

    [SerializeField]
    public NavMeshAgent AgentPath;

    [SerializeField]
    public LayerMask coverLayer; // to set the layer that should be used as cover

    [SerializeField]
    public LayerMask visibleLayer; // to declare objects on layer that might obstruct the view between AI and target/player

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
            {typeof(ChaseState), new ChaseState(this) },
            { typeof(AttackState), new AttackState(this) }

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
