/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class StoneToDrag : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "BLOCK")
        {
            gameObject.tag = "Ground";
        }
    }
}
