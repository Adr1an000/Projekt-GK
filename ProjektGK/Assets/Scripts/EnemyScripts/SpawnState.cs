using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// author: Paweł Salicki

public class SpawnState : EnemyBaseState
{
    private EnemyAI enemyAI; // enemyAI

    private bool startAnim = true;

    private float animCounter = 0;

    public SpawnState(EnemyAI _enemyAI) : base(_enemyAI.gameObject)
    {
        enemyAI = _enemyAI;
    }

    public override Type StatePerform()
    {
        animCounter += Time.deltaTime;

        if(startAnim && animCounter > 10)
        {
            enemyAI.Anim.Play("SPAWN");

            startAnim = false;
        }

        if(!startAnim)
        {
            return null;
        }    

        
        if(!(enemyAI.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1))
        {
            return null;
        }

        enemyAI.Anim.SetBool("isSpawned", true);

        return (typeof(PatrolState));
    }
}