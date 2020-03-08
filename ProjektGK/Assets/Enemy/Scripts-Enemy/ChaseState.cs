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

        Debug.Log("Chasing player");

        transform.LookAt(enemyAI.Target);

        enemyAI.agent.SetDestination(enemyAI.Target.position);

      

        return null;
    }
}
