using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// author: Paweł Salicki

public class PatrolState : EnemyBaseState
{
    private Vector3? destination; // enemy destination

    private EnemyAI enemyAI; // enemyAI

    private Quaternion desiredRotation; // targetRotation;

    private Vector3 direction;

    private int failureRandomDestinationCounter = 0;

    private Vector3 previousPosition;
    private float curSpeed;

    public PatrolState(EnemyAI _enemyAI) : base(_enemyAI.gameObject)
    {
        enemyAI = _enemyAI;
    }

    public override Type StatePerform()
    {
        UpdateAnimation();

        //check for target
        Transform chaseTarget = CheckForTarget();

        if (chaseTarget != null)
        {
           return (typeof(ChaseState));
        }

        //if no target wnader aimlessly
        if (destination.HasValue == false || Vector3.Distance(transform.position, destination.Value) <= enemyAI.ObstacleReverseRange)
        {
            FindRandomDestination();
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * enemyAI.TurnSpeed);

        if (IsForwardBlocked())
        {
            FindRandomDestination();

            failureRandomDestinationCounter++;

            if(failureRandomDestinationCounter >= 500 * Time.deltaTime)
            {
                RotateRight();
            }
        }
        else
        {
            failureRandomDestinationCounter = 0;
            transform.Translate(Vector3.forward * Time.deltaTime * enemyAI.PatrolSpeed);
        }

        return null;
    }

    private Transform CheckForTarget()
    {

        if (Vector3.Distance(transform.position, enemyAI.PlayerTarget.transform.position) < enemyAI.MinDistanceFromPlayer)
        {
            return enemyAI.PlayerTarget.transform;
        }

        return null;
    }

    private void RotateRight()
    {
        transform.Rotate(0.0f, 45.0f, 0.0f);
    }

    private bool IsForwardBlocked()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        return Physics.SphereCast(ray, 0.5f, enemyAI.ObstacleReverseRange, enemyAI.layerMask);
    }

    void FindRandomDestination()
    {
        Vector3 testPostion = (transform.position + (transform.forward * 4.0f)) + new Vector3(UnityEngine.Random.Range(-4.5f, 4.5f), 0.0f, UnityEngine.Random.Range(-4.5f, 4.5f));

        destination = new Vector3(testPostion.x, testPostion.y, 0.0f);

        direction = Vector3.Normalize(destination.Value - transform.position);
        direction = new Vector3(direction.x, 0f, direction.z);
        desiredRotation = Quaternion.LookRotation(direction);
    }

    public void UpdateAnimation()
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