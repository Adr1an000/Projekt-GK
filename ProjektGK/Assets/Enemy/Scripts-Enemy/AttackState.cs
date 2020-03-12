using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AttackState : EnemyBaseState
{
    private EnemyAI enemyAI;

   

    public float rangeDist = 55f;

    // GoToCover

    Vector3 coverObj; // to store the cover object positions


    private float maxCovDist = 50; // if distance to cover is greater than this, do sth else
    private bool playerInRange = false;

    private int testCoverPos = 10;

    // take cover hide
    Vector3 randomPosition;
    Vector3 coverPoint;

    // bool to find positions behind cover
    bool RandomPoint(Vector3 center, float rangeRandPoint, out Vector3 resultCover)
    {
        for(int i = 0; i < testCoverPos; i++)
        {
            randomPosition = center + UnityEngine.Random.insideUnitSphere * rangeRandPoint;
            Vector3 direction = enemyAI.PlayerTarget.transform.position - randomPosition;
            RaycastHit hitTestCov;
            if(Physics.Raycast(randomPosition, direction.normalized, out hitTestCov, rangeRandPoint, enemyAI.visibleLayer))
            {
                if(hitTestCov.collider.gameObject.layer == 18)
                {
                    resultCover = randomPosition;
                    return true;
                }
            }
        }

        resultCover = Vector3.zero;
        return false;
    }

    public AttackState(EnemyAI _enemyAI) : base(_enemyAI.gameObject)
    {
        enemyAI = _enemyAI;
    }

    void CheckCoverDist()
    {
        // check if cover is in vicinity
        Collider[] colliders = Physics.OverlapSphere(transform.position, maxCovDist, enemyAI.coverLayer);
        Collider nearestCollider = null;
        float minSqrDistance = Mathf.Infinity;

        Vector3 AI_position = enemyAI.transform.position;

        for(int i = 0; i < colliders.Length; i++)
        {
            float sqrDistanceToCenter = (AI_position - colliders[i].transform.position).sqrMagnitude;
            if(sqrDistanceToCenter < minSqrDistance)
            {
                minSqrDistance = sqrDistanceToCenter;
                nearestCollider = colliders[i];

                // to chceck if AI is already close enough to take cover
                float coverDistance = (nearestCollider.transform.position - AI_position).sqrMagnitude;

                if(coverDistance <= maxCovDist * maxCovDist)
                {
                    enemyAI.coverIsClose = true;
                    coverObj = nearestCollider.transform.position;
                    if(coverDistance <= enemyAI.distToCoverObj * enemyAI.distToCoverObj)
                    {
                        enemyAI.coverNotReached = false;
                    }
                    else if(coverDistance > enemyAI.distToCoverObj * enemyAI.distToCoverObj)
                    {
                        enemyAI.coverNotReached = true;
                    }
                }
                if(coverDistance >= maxCovDist * maxCovDist)
                {
                    enemyAI.coverIsClose = false;
                }
            }
        }
        if(colliders.Length < 1)
        {
            enemyAI.coverIsClose = false;
        }
    }

    void FacePlayer()
    {
        Vector3 direction = (enemyAI.PlayerTarget.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * enemyAI.facePlayerFactor);
    }

    void TakeCover()
    {

        if (RandomPoint(enemyAI.transform.position, enemyAI.rangeRandPoint, out coverPoint))
        {

            if(enemyAI.AgentPath.isActiveAndEnabled)
            {

                enemyAI.AgentPath.SetDestination(coverPoint);

                Debug.Log(coverPoint.x + " " + coverPoint.y + " " + coverPoint.z);
                if ((coverPoint - enemyAI.transform.position).sqrMagnitude <= enemyAI.distToCoverPos * enemyAI.distToCoverPos) // 0.75f
                {
                    
                    enemyAI.isHiding = true;
                }
            }
        }
    }

    public override Type StatePerform()
    {
        if(enemyAI.AgentPath.isActiveAndEnabled)
        {
            if(Time.frameCount % enemyAI.frameInterval == 0) // to this only every few frame, no neeed to do it every frame
            {
                float distance = ((enemyAI.PlayerTarget.transform.position - transform.position).sqrMagnitude); // check distance to player

                if (distance < rangeDist * rangeDist)
                {
                    playerInRange = true;
                }
                else
                {
                    playerInRange = false;
                }
            }

            
            if(playerInRange == true)
            {
                CheckCoverDist(); // check if cover is close enough

                if(enemyAI.coverIsClose == true)
                {
                    if(enemyAI.coverNotReached == true)
                    {
                        enemyAI.AgentPath.SetDestination(coverObj); // gor ==t to the cover obj
                        FacePlayer();
                    }

                    if(enemyAI.coverNotReached == false) // when close enough to cover, take cover
                    {
                        TakeCover();
                        FacePlayer();
                    }
                }
                if(enemyAI.coverIsClose == false) // cover is too far away
                {
                    Debug.Log("Cover is too far");
                    // do sth else like attack or chase
                }
            }
        }

        if (Vector3.Distance(enemyAI.transform.position, enemyAI.PlayerTarget.transform.position) > AISettings.MinDistanceFromPlayer + 10f)
        {
            enemyAI.AgentPath.isStopped = true;
            enemyAI.AgentPath.ResetPath();
            enemyAI.Target = enemyAI.PlayerTarget.transform;
            return typeof(ChaseState);
        }

        return null;
    }

}
