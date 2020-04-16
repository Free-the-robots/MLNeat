using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowRotate : MonoBehaviour
{

    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 offsetPosition;

    private void LateUpdate()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (target == null)
        {
            Debug.LogWarning("Missing target ref !", this);

            return;
        }

        transform.position = target.TransformPoint(offsetPosition);
        transform.LookAt(target);

    }
}
