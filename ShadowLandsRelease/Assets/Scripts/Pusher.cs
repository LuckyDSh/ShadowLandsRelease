/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class Pusher : MonoBehaviour
{
    #region Variables
    private Rigidbody2D target;
    private float power;
    #endregion

    public void Start()
    {
        target = gameObject.GetComponent<Rigidbody2D>();
    }

    public void PUSH_DOWN()
    {
        target.constraints = RigidbodyConstraints2D.FreezePositionX;
        target.AddForce(-transform.up * power);

    }
}
