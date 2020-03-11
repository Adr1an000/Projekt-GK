using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{

    public GameObject spawnedEnemy;
    public bool stopSpawning = false;
    public float spawnTime;
    public float spawnDelay;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", spawnTime, spawnDelay);
    }

    public void SpawnEnemy()
    {
        Instantiate(spawnedEnemy, transform.position, transform.rotation);

        if(stopSpawning)
        {
            CancelInvoke("SpawnEnemy");
        }
    }
}
