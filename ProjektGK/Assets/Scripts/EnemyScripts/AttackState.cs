using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// author: Paweł Salicki

public class AttackState : EnemyBaseState
{
    private EnemyAI enemyAI;

    private float decisionTimer;

    private float rotateToPlayerTimer;

    private float rotateToPlayerMax = 0.01F;

    private float decisionTimerMax = 0.25F;

    private bool calculateRandomDir = false;

    private enum MovementDecision { NOTHING, RANDOM_DIRECTION, STRAFE_LEFT, STRAFE_RIGHT };

    MovementDecision decision = MovementDecision.NOTHING;

    private Vector3 previousPosition;
    private float curSpeed;

    public AttackState(EnemyAI _enemyAI) : base(_enemyAI.gameObject)
    {
        enemyAI = _enemyAI;
    }

    private void FacePlayer()
    {
        var targetDirection = enemyAI.PlayerTarget.transform.position - transform.position;
        var targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * enemyAI.FacePlayerFactor);
    }


    public override Type StatePerform()
    {
        UpdateAnimation();

        float distance = Vector3.Distance(enemyAI.transform.position, enemyAI.PlayerTarget.transform.position);

        decisionTimer += Time.deltaTime;
        rotateToPlayerTimer += Time.deltaTime;

        if (decisionTimer > decisionTimerMax)
        {
            decisionTimer = 0;
            ChooseMovement();
        }

        if (rotateToPlayerTimer > rotateToPlayerMax)
        {
            rotateToPlayerTimer = 0;
            FacePlayer();
        }

        MoveToDirection();


        if (distance < enemyAI.AttackRange / 6)
        {
            Vector3 dirToPlayer = transform.position - enemyAI.PlayerTarget.transform.position;
            Vector3 newPos = (transform.position + dirToPlayer) / 2;
            enemyAI.AgentPath.isStopped = false;

            enemyAI.AgentPath.SetDestination(newPos);
        }
        else if (distance > enemyAI.AttackRange / 3)
        {
            enemyAI.AgentPath.SetDestination(enemyAI.PlayerTarget.transform.position);
            enemyAI.AgentPath.isStopped = false;


            if (distance < enemyAI.AttackRange / 5)
            {
                enemyAI.AgentPath.ResetPath();
            }
        }
        else
        {
            FacePlayer();
        }


        if (distance > enemyAI.AttackRange)
        {
            enemyAI.AgentPath.isStopped = true;
            enemyAI.AgentPath.ResetPath();

            if (enemyAI.Weapon != null)
            {
                enemyAI.Weapon.ReleaseTrigger();
            }

            enemyAI.Anim.SetBool("isAttacking", false);
            return typeof(ChaseState);
        }

        if (enemyAI.Weapon != null)
        {
            FireBullet();
        }


        return null;
    }


    private void MoveToDirection()
    {
        enemyAI.AgentPath.isStopped = false;

        switch (decision)
        {
            case MovementDecision.RANDOM_DIRECTION:
                RandomDirection();
                break;
            case MovementDecision.STRAFE_LEFT:
                StrafeLeft();
                break;
            case MovementDecision.STRAFE_RIGHT:
                StrafeRight();
                break;
        }
    }

    private void RandomDirection()
    {
        if (calculateRandomDir == true)
        {
            float xPos = enemyAI.transform.position.x;
            float zPos = enemyAI.transform.position.z;

            float xMov = xPos + UnityEngine.Random.Range(xPos - 100, xPos + 100);
            float zMov = zPos + UnityEngine.Random.Range(zPos - 100, zPos + 100);

            var target = new Vector3(xMov, enemyAI.transform.position.y, zMov);

            enemyAI.AgentPath.SetDestination(target);

            calculateRandomDir = false;
        }

    }

    private void ChooseMovement()
    {
        var dec = UnityEngine.Random.Range(-1, 4);

        switch (dec)
        {
            case 0:
                decision = MovementDecision.NOTHING;
                break;
            case 1:
                decision = MovementDecision.RANDOM_DIRECTION;
                calculateRandomDir = true;
                break;
            case 2:
                decision = MovementDecision.STRAFE_LEFT;
                break;
            case 3:
                decision = MovementDecision.STRAFE_RIGHT;
                break;
        }
    }

    private void StrafeLeft()
    {
        var offsetPlayer = transform.position - enemyAI.PlayerTarget.transform.position;
        var dir = Vector3.Cross(offsetPlayer, Vector3.up);
        enemyAI.AgentPath.SetDestination(transform.position + dir);
    }

    private void StrafeRight()
    {
        var offsetPlayer = enemyAI.PlayerTarget.transform.position - transform.position;
        var dir = Vector3.Cross(offsetPlayer, Vector3.up);
        enemyAI.AgentPath.SetDestination(transform.position + dir);
    }

    public void UpdateAnimation()
    {
        Vector3 curMove = transform.position - previousPosition;
        curSpeed = curMove.magnitude / Time.deltaTime;
        previousPosition = transform.position;

        if (curSpeed > 6)
        {
            enemyAI.Anim.speed = curSpeed / 5;
        }
        else
        {
            enemyAI.Anim.speed = 1;
        }

        enemyAI.Anim.SetFloat("bodySpeed", curSpeed);
    }



    void FireBullet()
    {
        RaycastHit hitPlayer;
        Ray playerPos = new Ray(enemyAI.Weapon.transform.position, enemyAI.Weapon.transform.forward);

        if (Physics.SphereCast(playerPos, 0.001f, out hitPlayer, enemyAI.AttackRange * 2f))
        {

            if (hitPlayer.transform.tag == "Player")
            {
                enemyAI.Weapon.PressTrigger();
            }
            else
            {
                enemyAI.Weapon.ReleaseTrigger();
            }
        }
    }

}
