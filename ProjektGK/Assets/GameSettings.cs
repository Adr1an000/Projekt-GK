using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [SerializeField]
    public float EnemySpeed = 2f;

 //   public static float EnemySpeed => Instance.enemySpeed;

    [SerializeField]
    public float AggroRadius = 4f;

 //   public static float AggroRadius => Instance.aggroRadius;

    [SerializeField]
    public float AttackRange = 3f;

 //   public static float AttackRange => Instance.attackRange;

    /*
    [SerializeField] private GameObject enemyProjectilePrefab;
    public static GameObject EnemyProjectilePrefab => Instance.enemyProjectilePrefab;
    */

    public static GameSettings Instance { get; private set; }

    private void Awake()
    {
        if(Instance!= null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}
