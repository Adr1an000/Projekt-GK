using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISettings : MonoBehaviour
{
    public float wanderSpeed = 2;
    public float chaseSpeed = 2;
    public float turnSpeed = 2;
    public float attackRange = 2;
    public float AttackSpeed()
    {
        return Random.Range(-1, 2);
    }
    public float frenzyLevel = 2;
    public int detectionRange = 2;
    public LayerMask opponentLayers;
    public LayerMask obstacleLayerMask;
}
