using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AISettings
{
    [SerializeField]
    public static float PatrolSpeed { get; set; } = 2.0f;

    [SerializeField]
    public static float ChaseSpeed { get; set; } = 4.0f;

    [SerializeField]
    public static float TurnSpeed { get; set; } = 2.0f;

    [SerializeField]
    public static float AttackRange { get; set; } = 10.0f;

    [SerializeField]
    public static float DetectionRange { get; set; } = 2.0f;

    [SerializeField]
    public static LayerMask opponentLayers;

    [SerializeField]
    public static LayerMask obstacleLayerMask;

    public static float AttackSpeed()
    {
        return Random.Range(-1, 2);
    }
}
