using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AttackState : EnemyBaseState
{
    private EnemyAI enemyAI;

    Vector3 coverObj; // to store the cover object positions

    private float maxCovDist = 500; // if distance to cover is greater than this, do sth else

    private bool playerInRange = false;

    private int testCoverPos = 10;

    Vector3 randomPosition;

    Vector3 coverPoint;

    // shooting
    private float timerShots;

    private float timeBtwShots = 0.25f;

    private float fireRadius = 25f;

    // bool to find positions behind cover
    bool RandomPoint(Vector3 center, float rangeRandPoint, out Vector3 resultCover)
    {
        for(int i = 0; i < testCoverPos; i++)
        {
            randomPosition = center + UnityEngine.Random.insideUnitSphere * rangeRandPoint;
            Vector3 direction = enemyAI.PlayerTarget.transform.position - randomPosition;
            RaycastHit hitTestCov;
            if(Physics.Raycast(randomPosition, direction.normalized, out hitTestCov, rangeRandPoint, enemyAI.VisibleLayer))
            {
                if(hitTestCov.collider.gameObject.layer == 18) // 18-COVER LAYER
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
        Collider[] colliders = Physics.OverlapSphere(enemyAI.transform.position, maxCovDist, enemyAI.CoverLayer);
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
                Debug.Log(coverDistance);

                if (coverDistance <= maxCovDist * maxCovDist)
                {
                    enemyAI.CoverIsClose = true;
                    coverObj = nearestCollider.transform.position;
                    if(coverDistance < enemyAI.DistToCoverObj)
                    {
                        Debug.Log("Cover not reached false");
                        enemyAI.CoverNotReached = false;
                    }
                    else
                    {
                        Debug.Log("Cover not reached true");
                        enemyAI.CoverNotReached = true;
                    }
                }
                if(coverDistance >= maxCovDist * maxCovDist)
                {
                    enemyAI.CoverIsClose = false;
                }
            }
        }
        if(colliders.Length < 1)
        {
            enemyAI.CoverIsClose = false;
        }
    }

    void FacePlayer()
    {
        Vector3 direction = (enemyAI.PlayerTarget.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * enemyAI.FacePlayerFactor);
    }

    void TakeCover()
    {
        if (RandomPoint(enemyAI.transform.position, enemyAI.RangeRandPoint, out coverPoint))
        {
            if(enemyAI.AgentPath.isActiveAndEnabled)
            {
                enemyAI.AgentPath.SetDestination(coverPoint);

                Debug.Log(coverPoint.x + " " + coverPoint.y + " " + coverPoint.z);
                if ((coverPoint - enemyAI.transform.position).sqrMagnitude <= enemyAI.DistToCoverPos)
                {
                    enemyAI.IsHiding = true;
                }
            }
        }
    }

    public override Type StatePerform()
    {
        if(enemyAI.AgentPath.isActiveAndEnabled)
        {
            if(Time.frameCount % enemyAI.FrameInterval == 0) // to this only every few frame, no neeed to do it every frame
            {
                float distance = Vector3.Distance(enemyAI.transform.position, enemyAI.PlayerTarget.transform.position);

                if (distance < enemyAI.MinDistanceFromPlayer)
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
                FireBullet();

                CheckCoverDist(); // check if cover is close enough

                if(enemyAI.CoverIsClose == true)
                {
                    if(enemyAI.CoverNotReached == true)
                    {
                        enemyAI.AgentPath.SetDestination(coverObj); // gor ==t to the cover obj
                        FacePlayer();
                    }
                    else // when close enough to cover, take cover
                    {
                        TakeCover();
                        FacePlayer();
                    }
                }
                if(enemyAI.CoverIsClose == false) // cover is too far away
                {
                    
                }
            }
        }

        if (Vector3.Distance(enemyAI.transform.position, enemyAI.PlayerTarget.transform.position) > enemyAI.MinDistanceFromPlayer)
        {
            enemyAI.AgentPath.isStopped = true;
            enemyAI.AgentPath.ResetPath();
            enemyAI.Target = enemyAI.PlayerTarget.transform;
            return typeof(ChaseState);
        }

        return null;
    }

    void FireBullet()
    {
        RaycastHit hitPlayer;
        Ray playerPos = new Ray(enemyAI.transform.position, enemyAI.transform.forward);

        if(Physics.SphereCast(playerPos, 0.25f, out hitPlayer, fireRadius))
        {
            if(timerShots <= 0 && hitPlayer.transform.tag == "Player")
            {
                // shoot here code
                enemyAI.weapon.PressTrigger();

                timerShots = timeBtwShots;
            }
            else
            {
                enemyAI.weapon.ReleaseTrigger();
                timerShots -= Time.deltaTime;
            }
        }
        else
        {
            enemyAI.weapon.ReleaseTrigger();
        }
    }

}
