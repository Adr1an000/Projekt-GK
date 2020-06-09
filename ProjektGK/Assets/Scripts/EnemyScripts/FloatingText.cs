using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// author: Paweł Salicki

public class FloatingText : MonoBehaviour
{
    public float DestroyTime = 1.5f;
    public Vector3 Offset = new Vector3(0, 10, 0);
    public Vector3 RandomizeIntensivity = new Vector3(0.5f, 0, 0);

    public GameObject Target;

    private Quaternion _lookRotation;
    private Vector3 _direction;

    void Start()
    {
        Destroy(gameObject, DestroyTime);

        transform.localPosition += Offset;

        transform.localPosition += new Vector3(Random.Range(-3, 3),
                Random.Range(-3, 3),
                Random.Range(-3, 3));
    }

    void Awake()
    {
        Target = GameObject.Find("Player");
    }

    private void LateUpdate()
    {
        _direction = (Target.transform.position - transform.position).normalized;

        _lookRotation = Quaternion.LookRotation(_direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, 10000);

        transform.Rotate(0, 180, 0);
    }

    void Update()
    {
        if(transform.localScale.x > 0)
        {
            transform.localScale -= new Vector3(Time.deltaTime, Time.deltaTime, 0);
            transform.localPosition += new Vector3(Random.Range(-RandomizeIntensivity.x, RandomizeIntensivity.x),
                Random.Range(-RandomizeIntensivity.y, RandomizeIntensivity.y),
                Random.Range(-RandomizeIntensivity.z, RandomizeIntensivity.z));
        }

    }
}
