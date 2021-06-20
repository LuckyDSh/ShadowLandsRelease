/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    [SerializeField] private float rotationSpeed = 5f;
    float angle = 10f;

    void FixedUpdate()
    {
        //angle += transform.rotation.z * Time.fixedDeltaTime * rotationSpeed;
        //angle = Mathf.Lerp(transform.rotation.z, angle, rotationSpeed);

        //obj.GetComponent<Rigidbody2D>().MoveRotation(angle);
        obj.transform.RotateAround(transform.position, -Vector3.forward, rotationSpeed * Time.fixedDeltaTime);
    }
}
