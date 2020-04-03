using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{

    //values that will be set in the Inspector
    public Transform Target;
    public float RotationSpeed;
    public GameObject bone;
    public GameObject fovStartPoint;
    public float maxAngle = 90;
    //values for internal use
    private Quaternion _lookRotation;
    private Vector3 _direction;

    // Update is called once per frame
    void LateUpdate()
    {
        if (EnemyInFieldOfView(fovStartPoint))
        {
            //find the vector pointing from our position to the target
            _direction = (Target.position - bone.transform.position).normalized;

            //create the rotation we need to be in to look at the target
            _lookRotation = Quaternion.LookRotation(_direction);

        //rotate us over time according to speed until we are in the required rotation
        bone.transform.rotation = Quaternion.Slerp(bone.transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);

        // bone.transform.Rotate(0, 30, 0);

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

    /*
    public GameObject player;

    public GameObject fovStartPoint;

    public GameObject rorateBone;

    public float lookSpeed = 200;



    public float maxAngleReset = 90;

    public bool canLean = false;

    public bool arms = false;

    private Quaternion lookAt;
    private Quaternion targetRotation;
    
    void Update()
    {

        if (EnemyInFieldOfView(fovStartPoint))
        {
            Vector3 direction = player.transform.position - rorateBone.transform.position;

            if (!canLean)
            {
                direction = new Vector3(direction.x, 0, direction.z);
            }

            targetRotation = Quaternion.LookRotation(direction);
            lookAt = Quaternion.RotateTowards(
            rorateBone.transform.rotation, targetRotation, Time.deltaTime * lookSpeed);
            rorateBone.transform.rotation = lookAt;
        }
        else if (EnemyInFieldOfViewNoResetPoint(fovStartPoint))
        {
            return;
        }
        else
        {
            if (arms)
            {
                Quaternion targetRotation = Quaternion.Euler(90, 0, 0);
                rorateBone.transform.localRotation = Quaternion.RotateTowards(
                rorateBone.transform.localRotation, targetRotation, Time.deltaTime * lookSpeed);
            }
            else
            {
                Quaternion targetRotation = Quaternion.Euler(0, 0, 0);
                rorateBone.transform.localRotation = Quaternion.RotateTowards(
                rorateBone.transform.localRotation, targetRotation, Time.deltaTime * lookSpeed);
            }
        }
    }


    bool EnemyInFieldOfViewNoResetPoint(GameObject looker)
    {
        Vector3 targetDir = player.transform.position - looker.transform.position;
        float angle = Vector3.Angle(targetDir, looker.transform.forward);

        if (angle < maxAngleReset)
        {
            return true;
        }
        else
        {
            return false;
        }
       */


}