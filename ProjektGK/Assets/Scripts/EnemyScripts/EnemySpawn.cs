using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// author: Paweł Salicki

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject SpawnedEnemy;

    [SerializeField]
    private float SpawnTime = 5;

    [SerializeField]
    private float SpawnDelay = 5;

    [SerializeField]
    private int MaxEnemySpawn = 10;

    [SerializeField]
    public int EnemyFromPortal = 0;

    public GameObject CounterText;

    public Transform spawnPosition;

    public GameObject PortalGateObject;

    public bool startSpawning = false;
    

    public void InvokeSpawnEnemy()
    {
        if(!startSpawning)
        {
            InvokeRepeating("SpawnEnemy", SpawnTime, SpawnDelay);

            CounterText.GetComponent<TextMesh>().text = MaxEnemySpawn.ToString();

            PortalGateObject.SetActive(true);

            startSpawning = true;
        }
    }

    public void SpawnEnemy()
    {
        var rotate = new Quaternion(transform.rotation.x, transform.rotation.y + 180, transform.rotation.z, transform.rotation.w);
       

        Instantiate(SpawnedEnemy, spawnPosition.position, rotate);
        EnemyFromPortal++;

        CounterText.GetComponent<TextMesh>().text = (MaxEnemySpawn - EnemyFromPortal).ToString();

        if(EnemyFromPortal >= MaxEnemySpawn)
        {
            CancelInvoke("SpawnEnemy");
            PortalGateObject.SetActive(false);
        }
    }
}
