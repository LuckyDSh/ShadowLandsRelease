/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class CameraMovementController : MonoBehaviour
{
    #region Variables
    [SerializeField] private Transform _target;
    public Transform Target { get { return _target; } set { _target = value; } }
    private float XSpeed;
    //[SerializeField] private float sideWaysSpeed;
    #endregion

    #region UnityMethods
    void Start()
    {
        XSpeed = transform.position.x - _target.position.x;
    }

    void LateUpdate()
    {
        if (_target)
            transform.position = new Vector3(_target.position.x + XSpeed,
                transform.position.y, transform.position.z);
    }
    #endregion
}
