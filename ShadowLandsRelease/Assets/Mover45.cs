/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class Mover45 : MonoBehaviour
{
    #region Variables
    [SerializeField] private float speed;
    [SerializeField] private Transform[] points;
    private Rigidbody2D rb;
    private bool is_on_change_direction;
    #endregion

    #region UnityMethods
    void Start()
    {
        is_on_change_direction = true;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (is_on_change_direction)
            transform.position = transform.position + transform.up * speed * Time.fixedDeltaTime;
        else
            transform.position = transform.position - transform.up * speed * Time.fixedDeltaTime;
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Point")
        {
            if (is_on_change_direction)
                is_on_change_direction = false;
            else
                is_on_change_direction = true;
        }
    }
}
