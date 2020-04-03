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
    
    void Start()
    {
        InvokeRepeating("SpawnEnemy", SpawnTime, SpawnDelay);
        CounterText = GameObject.Find("EnemyCounter");
        CounterText.GetComponent<TextMesh>().text = MaxEnemySpawn.ToString();
    }

    public void SpawnEnemy()
    {
        var rotate = new Quaternion(transform.rotation.x, transform.rotation.y + 180, transform.rotation.z, transform.rotation.w);
        var position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2);

        Instantiate(SpawnedEnemy, position, rotate);
        EnemyFromPortal++;

        CounterText.GetComponent<TextMesh>().text = (MaxEnemySpawn - EnemyFromPortal).ToString();

        if(EnemyFromPortal >= MaxEnemySpawn)
        {
            CancelInvoke("SpawnEnemy");
        }
    }
}
