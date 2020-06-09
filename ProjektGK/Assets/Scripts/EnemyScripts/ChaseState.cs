using System;
using UnityEngine;

// author: Paweł Salicki

public class ChaseState : EnemyBaseState
{
    private EnemyAI enemyAI;

    private Vector3 previousPosition;
    private float curSpeed;

    public ChaseState(EnemyAI _enemyAI) : base(_enemyAI.gameObject)
    {
        enemyAI = _enemyAI;
    }

    public override Type StatePerform()
    {
        UpdateAnimation();

        enemyAI.AgentPath.SetDestination(enemyAI.PlayerTarget.transform.position);

        if (Vector3.Distance(transform.position, enemyAI.PlayerTarget.transform.position) > enemyAI.MinDistanceFromPlayer)
        {
            enemyAI.AgentPath.isStopped = true;
            enemyAI.AgentPath.ResetPath();

            return typeof(PatrolState);
        }

        if(Vector3.Distance(transform.position, enemyAI.PlayerTarget.transform.position) < enemyAI.AttackRange)
        {
            enemyAI.AgentPath.ResetPath();
            enemyAI.AgentPath.velocity = Vector3.zero;
            enemyAI.AgentPath.isStopped = true;

            enemyAI.Anim.SetBool("isAttacking", true);

            StopAnimation();
            return typeof(AttackState);
        }

        return null;
    }

    private void StopAnimation()
    {
        enemyAI.Anim.SetFloat("bodySpeed", 0);
    }

    private void UpdateAnimation()
    {
        Vector3 curMove = transform.position - previousPosition;
        curSpeed = curMove.magnitude / Time.deltaTime;
        previousPosition = transform.position;

        enemyAI.Anim.SetFloat("bodySpeed", curSpeed);


        if (curSpeed > 6)
        {
            enemyAI.Anim.speed = curSpeed / 5;
        }
        else
        {
            enemyAI.Anim.speed = 1;
        }
    }
}
