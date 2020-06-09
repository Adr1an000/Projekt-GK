using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// author: Paweł Salicki

public class FaceNavMeshMovement : MonoBehaviour
{
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
    }

    void Update()
    {
        if (!agent.isStopped)
        {
            var targetPosition = agent.pathEndPosition;
            var targetPoint = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
            var _direction = (targetPoint - transform.position).normalized;
            var _lookRotation = Quaternion.LookRotation(_direction);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, _lookRotation, 360);
        }
    }
}
