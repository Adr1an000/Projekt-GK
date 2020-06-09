//author: Adrian Skutela

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public Respawnable item;
    public Vector3 position;
    public float delay;
     
    public void Respawn()
    {
        Debug.Log("respawning in 10s");
        StartCoroutine(SpawnAfterDelay());
    }
    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        Spawn();
    }

    private void Spawn()
    {
        Respawnable instance = Instantiate(item, transform, false);
        instance.transform.localPosition = position;
        instance.objectSpawner = gameObject.GetComponent<ObjectSpawner>();
    }

}