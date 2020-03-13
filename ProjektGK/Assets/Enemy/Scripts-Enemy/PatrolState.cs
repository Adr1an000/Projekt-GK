using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PatrolState : EnemyBaseState
{
    private Vector3? destination; // enemy destination

    [SerializeField]
    private float stopDistance = 2.0f; // stop distance from obstacle

    private readonly LayerMask layerMask = LayerMask.NameToLayer("Walls"); // layer mask

    private EnemyAI enemyAI; // enemyAI

    private Quaternion desiredRotation; // targetRotation;

    private Vector3 direction;

    private Quaternion startingAngle = Quaternion.AngleAxis(-60.0f, Vector3.up); // start angle of view

    private Quaternion stepAngle = Quaternion.AngleAxis(5.0f, Vector3.up); // stop angle of view

    private int failureRandomDestinationCounter = 0;

    //constructor
    public PatrolState(EnemyAI _enemyAI) : base(_enemyAI.gameObject)
    {
        enemyAI = _enemyAI;
    }

    //this operates like Update() function
    public override Type StatePerform()
    {
        //check for target
        Transform chaseTarget = CheckForTarget();
        if (chaseTarget != null)
        {
           enemyAI.SetTarget(chaseTarget);

           return (typeof(ChaseState));
        }

        //if no target wnader aimlessly
        if (destination.HasValue == false || Vector3.Distance(transform.position, destination.Value) <= stopDistance)
        {
            FindRandomDestination();
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * AISettings.TurnSpeed);

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
            transform.Translate(Vector3.forward * Time.deltaTime * AISettings.PatrolSpeed);
        }

        return null;
    }

    private Transform CheckForTarget()
    {
        // martwy kod, kocept Adriana jest taki, by rzucala sie horda na gracza. to na dole to bardziej pod skradanke

        /*
       RaycastHit hit;
       var angle = transform.rotation * startingAngle;
       var direction = angle * Vector3.forward;
       var pos = transform.position;

       for (int i = 0; i < 30; i++)
       {
           if (Physics.Raycast(pos, direction, out hit, AISettings.DetectionRange))
           {
               var enemy = hit.collider.GetComponent<TeamRecognition>();

               if ((enemy != null && enemy.Team != gameObject.GetComponent<TeamRecognition>().Team))
               {
                   Debug.Log("FOUND PLAYER");
                   Debug.DrawRay(pos, direction * hit.distance, Color.red);
                   return enemy.transform;
               }
               else
               {
                   Debug.DrawRay(pos, direction * hit.distance, Color.yellow);
               }
           }
           else
           {
               Debug.DrawRay(transform.position, direction * AISettings.DetectionRange, Color.white);
           }
           direction = stepAngle * direction;
       }
       */

        if (Vector3.Distance(transform.position, enemyAI.PlayerTarget.transform.position) < AISettings.MinDistanceFromPlayer)
        {
            return enemyAI.PlayerTarget.transform;
        }

        return null;
    }

    private void RotateRight()
    {
        transform.Rotate(0.0f, 45f, 0.0f);
    }

    private bool IsForwardBlocked()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        return Physics.SphereCast(ray, 0.5f, AISettings.ObstacleReverseRange, layerMask);
    }

    void FindRandomDestination()
    {
        Vector3 testPostion = (transform.position + (transform.forward * 4.0f)) + new Vector3(UnityEngine.Random.Range(-4.5f, 4.5f), 0.0f, UnityEngine.Random.Range(-4.5f, 4.5f));

        destination = new Vector3(testPostion.x, testPostion.y, 0.0f);

        direction = Vector3.Normalize(destination.Value - transform.position);
        direction = new Vector3(direction.x, 0f, direction.z);
        desiredRotation = Quaternion.LookRotation(direction);
    }
}