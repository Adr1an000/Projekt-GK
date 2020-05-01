using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPortalActivate : MonoBehaviour
{
    public GameObject enemyPortal;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            enemyPortal.GetComponent<EnemySpawn>().InvokeSpawnEnemy();
        }
    }
}
