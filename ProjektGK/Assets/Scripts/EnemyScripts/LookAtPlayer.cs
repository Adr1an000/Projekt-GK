using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// author: Paweł Salicki, Dawid Musialik

public class LookAtPlayer : MonoBehaviour
{
    public GameObject Target;
    public GameObject bone;
    public GameObject fovStartPoint;

    public float RotationSpeed;
    public float maxAngle = 120;

    public bool arm = false;

    private Quaternion _lookRotation;
    private Vector3 _direction;

    void Awake()
    {
        Target = GameObject.Find("Player");
    }

    void LateUpdate()
    {
        if (EnemyInFieldOfView(fovStartPoint))
        {
            _direction = (Target.transform.position - bone.transform.position).normalized;

            _lookRotation = Quaternion.LookRotation(_direction);

            bone.transform.rotation = Quaternion.Slerp(bone.transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);

            if (arm)
            {
                bone.transform.Rotate(90, -20, 0);
            }
        }

    }

    bool EnemyInFieldOfView(GameObject looker)
    {
        Vector3 targetDir = Target.transform.position - looker.transform.position;

        float angle = Vector3.Angle(targetDir, looker.transform.forward);

        if (angle < maxAngle)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}