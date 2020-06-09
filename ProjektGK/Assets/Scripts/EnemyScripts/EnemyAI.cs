using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;
using TMPro;

// author: Paweł Salicki

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class EnemyAI : MonoBehaviour
{
    public GameObject PlayerTarget { get; private set; } // player object

    public bool CoverIsClose { get; set; } // is cover in reach?

    public bool CoverNotReached { get; set; } = true; // if true, AI is not close enough to the cover object

    public EnemyStateMachine EnemyStateMachine => GetComponent<EnemyStateMachine>(); // enemy state machine behaviour

    // show to gui Unity

    [SerializeField]
    public int FacePlayerFactor = 50; // face player factor


    [SerializeField]
    public float PatrolSpeed = 2.0f; // speed of patrol movement

    [SerializeField]
    public float MinDistanceFromPlayer = 30.0f; // min distance from player to take action

    [SerializeField]
    public float TurnSpeed = 2.0f; // speed of turn

    [SerializeField]
    public float AttackRange = 10.0f; // attack range

    [SerializeField]
    public float DetectionRange = 20.0f; // player distance detection

    [SerializeField]
    public float ObstacleReverseRange = 2.0f; // obstacle detection range

    [SerializeField]
    public NavMeshAgent AgentPath; // path

    [SerializeField]
    public Weapon Weapon;

    [SerializeField]
    public Animator Anim;

    [SerializeField]
    public Health health;

    [SerializeField]
    public LookAtPlayer lookAtPlayer;

    [SerializeField]
    public LayerMask layerMask; // layer mask

    [SerializeField]
    public GameObject FloatingTextPrefab;

    private int lastHealth;

    // init enemy
    private void Awake()
    {
        InitStateMachine(); // initialise enemy's state machine

        if (!PlayerTarget)
        {
            PlayerTarget = GameObject.Find("Player");
        }

        if (!Anim)
        {
            Anim = GetComponent<Animator>();
        }

        if (!AgentPath)
        {
            AgentPath = GetComponent<NavMeshAgent>();
        }

        if (!Weapon)
        {
            Weapon = GetComponentInChildren<Weapon>();
        }

        if (!health)
        {
            lastHealth = health.currentHealth;
        }

        AgentPath.updateRotation = true;
    }

    private void Update()
    {
        if (health != null)
        {
            if (!health.alive)
            {
                lookAtPlayer.enabled = false;
                EnemyStateMachine.CurrentState = EnemyStateMachine.enemyStates[typeof(DeathState)];
                if (Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1
                    && Anim.GetCurrentAnimatorStateInfo(0).IsName("DEATH"))
                {
                    Destroy(gameObject);
                }
            }


            if (lastHealth != health.currentHealth && FloatingTextPrefab != null)
            {
                ShowFloatingText();
                lastHealth = health.currentHealth;

            }
        }
    }

    private void Start()
    {
     //   FindObjectOfType<SoundManager>().PlayMusic("Robot");
    }

    private void ShowFloatingText()
    {
        if(lastHealth - health.currentHealth < 0)
        {
            return;
        }


        var go = Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity, transform);
        go.GetComponent<TextMeshPro>().text = (lastHealth - health.currentHealth).ToString();

    }

    // init behaviour state machine
    private void InitStateMachine()
    {
        var states = new Dictionary<Type, EnemyBaseState>()
        {
            {typeof(SpawnState), new SpawnState(this) }, // spawn state
            { typeof(PatrolState), new PatrolState(this)}, // patrol state
            {typeof(ChaseState), new ChaseState(this) }, // chase player state
            { typeof(AttackState), new AttackState(this) },// attack player state
            { typeof(DeathState), new DeathState(this) } // death state
        };
        GetComponent<EnemyStateMachine>().SetState(states);
    }
}
