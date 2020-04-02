using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyBaseState
{
    private EnemyAI enemyAI;

    private float decisionTimer;

    private float decisionTimerMax = 0.25F;

    private bool calculateRandomDir = false;

    private enum MovementDecision { NOTHING, RANDOM_DIRECTION, STRAFE_LEFT, STRAFE_RIGHT };

    MovementDecision decision = MovementDecision.NOTHING;

    // shooting
    private float timerShots;

    private float timeBtwShots = 0.25f;

    private float fireRadius = 25f;

    private Vector3 previousPosition;
    private float curSpeed;

    public AttackState(EnemyAI _enemyAI) : base(_enemyAI.gameObject)
    {
        enemyAI = _enemyAI;
    }

    private void FacePlayer()
    {
        float angle = Quaternion.FromToRotation(Vector3.up, enemyAI.PlayerTarget.transform.position - transform.position).eulerAngles.y;

        if (angle > 60 && angle < 300)
        {
            Vector3 direction = (enemyAI.PlayerTarget.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * enemyAI.FacePlayerFactor);
        }
        Debug.Log("ANGLE: " + angle);
      
        
    }

    float atan2Approximation(float y, float x) // http://http.developer.nvidia.com/Cg/atan2.html
    {
        float t0, t1, t2, t3, t4;
        t3 = Mathf.Abs(x);
        t1 = Mathf.Abs(y);
        t0 = Mathf.Max(t3, t1);
        t1 = Mathf.Min(t3, t1);
        t3 = 1f / t0;
        t3 = t1 * t3;
        t4 = t3 * t3;
        t0 = -0.013480470f;
        t0 = t0 * t4 + 0.057477314f;
        t0 = t0 * t4 - 0.121239071f;
        t0 = t0 * t4 + 0.195635925f;
        t0 = t0 * t4 - 0.332994597f;
        t0 = t0 * t4 + 0.999995630f;
        t3 = t0 * t3;
        t3 = (Mathf.Abs(y) > Mathf.Abs(x)) ? 1.570796327f - t3 : t3;
        t3 = (x < 0) ? 3.141592654f - t3 : t3;
        t3 = (y < 0) ? -t3 : t3;
        return t3;
    }

    public override Type StatePerform()
    {
        //   Debug.Log("ATTACK");

        UpdateAnimation();

        float distance = Vector3.Distance(transform.position, enemyAI.PlayerTarget.transform.position);

        decisionTimer += Time.deltaTime;

        

        if(decisionTimer > decisionTimerMax)
        {
            decisionTimer = 0;
            ChooseMovement();
        }

    //    MoveToDirection();

        if(distance < enemyAI.AttackRange / 3)
        {
            Vector3 dirToPlayer = transform.position - enemyAI.PlayerTarget.transform.position;
            Vector3 newPos = transform.position + dirToPlayer;

            enemyAI.AgentPath.SetDestination(newPos);
        }
        else
        {
            FacePlayer();
        }

        if (distance > enemyAI.AttackRange)
        {
            enemyAI.AgentPath.isStopped = true;
            enemyAI.AgentPath.ResetPath();

            enemyAI.Anim.SetBool("isAttacking", false);
            return typeof(ChaseState);
        }

        return null;
    }


    private void MoveToDirection()
    {

        switch(decision)
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
        if(calculateRandomDir == true)
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
        var dec = UnityEngine.Random.Range(0, 3);

        switch(dec)
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

        enemyAI.Anim.SetFloat("bodySpeed", curSpeed);

        //Debug.Log(curSpeed);
    }
    /*
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
    */
}
