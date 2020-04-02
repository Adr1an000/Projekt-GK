using System;
using UnityEngine;

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
        //   Debug.Log("CHASE");

        UpdateAnimation();

        enemyAI.AgentPath.SetDestination(enemyAI.PlayerTarget.transform.position);

        if (Vector3.Distance(transform.position, enemyAI.PlayerTarget.transform.position) > enemyAI.MinDistanceFromPlayer)
        {
            enemyAI.AgentPath.isStopped = true;
            enemyAI.AgentPath.ResetPath();

            return typeof(PatrolState);
        }

        if(Vector3.Distance(transform.position, enemyAI.PlayerTarget.transform.position) < enemyAI.AttackRange / 2)
        {
            enemyAI.AgentPath.ResetPath();

            enemyAI.Anim.SetBool("isAttacking", true);

            return typeof(AttackState);
        }

        return null;
    }

    public void UpdateAnimation()
    {
        Vector3 curMove = transform.position - previousPosition;
        curSpeed = curMove.magnitude / Time.deltaTime;
        previousPosition = transform.position;

        enemyAI.Anim.SetFloat("bodySpeed", curSpeed);


        /*
        if(curSpeed > 5)
        {
            enemyAI.Anim.speed = curSpeed / 5;
        }
        else
        {
            enemyAI.Anim.speed = 1;
        }
        */
        Debug.Log(curSpeed);
    }
}
