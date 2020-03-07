using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PatrolState : EnemyBaseState
{
    private Vector3? destination;
    private float stopDistance = 1.0f;
    private float turnSpeed = 1.0f;

    private readonly LayerMask _layerMask = LayerMask.NameToLayer("Walls");
    private float _rayDistance = 3.5f;

    private EnemyAI enemy;


    private Quaternion desiredRotation; //targetRotation;


private Vector3 direction;

private Quaternion startingAngle = Quaternion.AngleAxis(-60.0f, Vector3.up);
private Quaternion stepAngle = Quaternion.AngleAxis(5.0f, Vector3.up);


    //constructor
    public PatrolState(EnemyAI _enemyAI) : base(_enemyAI.gameObject)
    {
        enemy = _enemyAI;

        Debug.Log("Konstruktor patrol state");
    }

    //this operates like Update() function
    public override Type Tick()
    {
       // Debug.Log("Metoda Tick wchodze");

        //check for target
        Transform chaseTarget = CheckForTarget();
        if (chaseTarget != null)
        {
            enemy.SetTarget(chaseTarget);

         //   return (typeof(ChaseState));
        }

        //if no target wnader aimlessly
        if (destination.HasValue == false || Vector3.Distance(transform.position, destination.Value) <= stopDistance)
        {
            FindRandomDestination();
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * enemy.aISettings.turnSpeed);

        if (IsForwardBlocked())
        {
            FindRandomDestination();
        }
        else
        {
            transform.Translate(Vector3.forward * Time.deltaTime * enemy.aISettings.wanderSpeed);
        }

      //  Debug.Log(transform.position.x);
        return null;
    }

    private Transform CheckForTarget()
    {
        RaycastHit hit;
        var angle = transform.rotation * startingAngle;
        var direction = angle * Vector3.forward;
        var pos = transform.position;

        for(int i = 0; i < 24; i++)
        {
            if (Physics.Raycast(pos, direction, out hit, 10.0f))
            {
                var enemy = hit.collider.GetComponent<EnemyAI>();
                if(enemy != null && enemy.Team != gameObject.GetComponent<EnemyAI>().Team)
                {
                    Debug.DrawRay(pos, direction * hit.distance, Color.red);
                    Debug.Log("RED");
                    return enemy.transform;
                }
                else
                {
                    Debug.DrawRay(pos, direction * hit.distance, Color.yellow);
                    Debug.Log("YELLOW");
                }
            }
            else
            {
                Debug.Log("WHITE");
                Debug.DrawRay(transform.position, direction * 10f, Color.blue);
            }
            direction = stepAngle * direction;
        }

        return null;
    }

    private bool IsForwardBlocked()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        return Physics.SphereCast(ray, 0.5f, _rayDistance, _layerMask);
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