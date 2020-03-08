using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : EnemyBaseState
{
    private EnemyAI enemyAI;

    private bool chasePlayer = false;

    private Vector3 targetLastLocation;

    public ChaseState(EnemyAI _enemyAI) : base(_enemyAI.gameObject)
    {
        enemyAI = _enemyAI;
        chasePlayer = true;
    }

    public override Type StatePerform()
    {
        if(enemyAI.Target == null)
        {
            chasePlayer = true;

            return typeof(PatrolState);
        }

        if(chasePlayer == true)
        {
            transform.LookAt(enemyAI.Target);

            targetLastLocation = enemyAI.Target.position;

            chasePlayer = false;
        }

        enemyAI.agent.SetDestination(targetLastLocation);

        enemyAI.agent.stoppingDistance = 5;

        if (Vector3.Distance(transform.position, enemyAI.agent.destination) <= 5f)
        {
            enemyAI.agent.isStopped = true;
            enemyAI.agent.ResetPath();
            enemyAI.Target = null;
        }

        return null;
    }
}
