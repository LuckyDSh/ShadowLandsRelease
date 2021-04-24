/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class BG_Follow : MonoBehaviour
{
    #region Variables
    [Header("Refs")]
    [SerializeField] private Transform target;
    private Rigidbody2D backGroundRb;
    [Header("Parameters")]
    [SerializeField] private Vector3 offSet;
    #endregion

    #region UnityMethods

    private void Start()
    {
        //backGroundRb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (target == null)
            return;

        transform.position = target.position + offSet;

        //backGroundRb.freezeRotation = true;
        //Vector3 newPos = target.position + offSet;
        //newPos.z = 0f;
        //backGroundRb.MovePosition(newPos);
    }
    #endregion
}
