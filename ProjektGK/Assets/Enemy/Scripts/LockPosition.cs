using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPosition : MonoBehaviour
{
    public Transform TargetTransform;

    void LateUpdate()
    {
        transform.position = TargetTransform.position;
        transform.localScale = TargetTransform.localScale;
    }
}
