using System;
using UnityEngine;

// author: Paweł Salicki

public class DeathState : EnemyBaseState
{
    private EnemyAI enemyAI;

    public DeathState(EnemyAI _enemyAI) : base(_enemyAI.gameObject)
    {
        enemyAI = _enemyAI;
    }

    public override Type StatePerform()
    {
        enemyAI.AgentPath.isStopped = true;
        enemyAI.AgentPath.ResetPath();

        return null;
    }
}
