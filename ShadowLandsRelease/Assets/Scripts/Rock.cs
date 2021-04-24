/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class Rock : MonoBehaviour
{
    #region Variables
    #endregion

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            AudioManager.instance.Play("RockFall");
        }
    }
}
