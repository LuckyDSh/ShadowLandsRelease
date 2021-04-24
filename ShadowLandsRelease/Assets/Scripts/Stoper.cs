/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class Stoper : MonoBehaviour
{
    #region Variables
    [SerializeField] private string object_block_name;
    private Rigidbody2D rb;
    #endregion

    #region UnityMethods
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    #endregion

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == object_block_name)
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
