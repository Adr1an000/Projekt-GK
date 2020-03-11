using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnedEnemy;

    [SerializeField]
    private float spawnTime;

    [SerializeField]
    private float spawnDelay;

    [SerializeField]
    private int maxEnemySpawn = 10;

    private int enemyFromPortal;

    private GameObject counterText;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", spawnTime, spawnDelay);
        counterText = GameObject.Find("EnemyCounter");
        counterText.GetComponent<TextMesh>().text = "10";
    }

    public void SpawnEnemy()
    {
        Instantiate(spawnedEnemy, transform.position, transform.rotation);
        enemyFromPortal++;

        counterText.GetComponent<TextMesh>().text = (maxEnemySpawn - enemyFromPortal).ToString();

        if(enemyFromPortal >= maxEnemySpawn)
        {
            CancelInvoke("SpawnEnemy");
        }
    }
}
