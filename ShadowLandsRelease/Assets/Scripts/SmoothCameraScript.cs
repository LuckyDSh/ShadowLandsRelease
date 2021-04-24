/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class SmoothCameraScript : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] public Vector3 offset;
    [SerializeField] private float smoothSpeed = 0.125f;

    public Transform Target { get { return target; } set { target = value; } }

    private void FixedUpdate()
    {
        if (target == null)
            return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z + offset.z);
        //transform.LookAt(target);
    }
}
