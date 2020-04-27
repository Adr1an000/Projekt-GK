using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RagdollToggle : MonoBehaviour
{
    public Animator Animator;
 //   public Rigidbody Rigidbody;
 //   public CapsuleCollider CapsuleCollider;
    public EnemyAI enemyAI;
    public EnemyStateMachine EnemyStateMachine;
    public LookAtPlayer LookAtPlayer;
    public NavMeshAgent Agent;
    public Collider MainCollider;

    public Collider[] ChildrenCollider;
    public Rigidbody[] ChildrenRigibody;
    public Animator[] ChildrenAnimator;

    public bool activeRagdoll = false;

    // Start is called before the first frame update
    void Awake()
    {
        ChildrenCollider = GetComponentsInChildren<Collider>();
        ChildrenRigibody = GetComponentsInChildren<Rigidbody>();
        ChildrenAnimator = GetComponentsInChildren<Animator>();
    }

    void Start()
    {
        RagdollActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(activeRagdoll == true)
        {
            RagdollActive(true);
            activeRagdoll = false;
        }
    }



    public void RagdollActive(bool active)
    {
        // children
        foreach(var collider in ChildrenCollider)
        {
            collider.enabled = active;
        }

        foreach(var rigidbody in ChildrenRigibody)
        {
            rigidbody.detectCollisions = active;
            rigidbody.isKinematic = !active;
        }

        if(active)
        {
            foreach (var a in ChildrenAnimator)
            {
                a.enabled = !active;
            }
        }

        // root
        Animator.enabled = !active;
        //       Rigidbody.detectCollisions = !active;
        //       Rigidbody.isKinematic = active;
        //        CapsuleCollider.enabled = !active;
        Agent.enabled = !active;
        enemyAI.enabled = !active;
        EnemyStateMachine.enabled = !active;
        LookAtPlayer.enabled = !active;

        MainCollider.enabled = !active;
    }
}
