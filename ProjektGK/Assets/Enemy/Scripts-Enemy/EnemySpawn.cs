using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private GameObject CounterText;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", SpawnTime, SpawnDelay);
        CounterText = GameObject.Find("EnemyCounter");
        CounterText.GetComponent<TextMesh>().text = "10";
    }

    public void SpawnEnemy()
    {
        Instantiate(SpawnedEnemy, transform.position, transform.rotation);
        EnemyFromPortal++;

        CounterText.GetComponent<TextMesh>().text = (MaxEnemySpawn - EnemyFromPortal).ToString();

        if(EnemyFromPortal >= MaxEnemySpawn)
        {
            CancelInvoke("SpawnEnemy");
        }
    }
}
