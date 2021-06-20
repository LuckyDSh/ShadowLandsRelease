/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class Rolling_Obstacle : MonoBehaviour
{
    #region Variables
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    #endregion

    #region UnityMethods
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        transform.position = transform.position + transform.right * moveSpeed * Time.fixedDeltaTime;
    }
    #endregion
}
