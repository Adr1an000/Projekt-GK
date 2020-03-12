using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : EnemyBaseState
{
    private EnemyAI enemyAI;

    public ChaseState(EnemyAI _enemyAI) : base(_enemyAI.gameObject)
    {
        enemyAI = _enemyAI;
    }

    public override Type StatePerform()
    {
        if(enemyAI.Target == null)
        {
            return typeof(PatrolState);
        }

        enemyAI.AgentPath.SetDestination(enemyAI.PlayerTarget.transform.position);

        enemyAI.AgentPath.stoppingDistance = 5;

        if (Vector3.Distance(transform.position, enemyAI.PlayerTarget.transform.position) > AISettings.MinDistanceFromPlayer + 10f)
        {
            enemyAI.AgentPath.isStopped = true;
            enemyAI.AgentPath.ResetPath();
            enemyAI.Target = null;
        }

        if(Vector3.Distance(transform.position, enemyAI.PlayerTarget.transform.position) < AISettings.AttackRange + 20f)
        {
            enemyAI.AgentPath.ResetPath();
            enemyAI.Target = null;
            return typeof(AttackState);
        }

        return null;
    }
}
