using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnState : EnemyBaseState
{
    private EnemyAI enemyAI; // enemyAI

    public SpawnState(EnemyAI _enemyAI) : base(_enemyAI.gameObject)
    {
        enemyAI = _enemyAI;
    }

    public override Type StatePerform()
    {

        Debug.Log("SPAWN STATE");

        if(!(enemyAI.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1))
        {
            Debug.Log("JEST ANIMACJA SPAWN");

            return null;
        }

        Debug.Log("KONIEC ANIMACJA SPAWN");

        enemyAI.Anim.SetBool("isSpawned", true);

        return (typeof(PatrolState));
    }
}