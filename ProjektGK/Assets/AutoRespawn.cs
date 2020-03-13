using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRespawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Health health = GetComponent<Health>();
        health.onDeath.AddListener(onDeath);
    }

    void onDeath()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
