/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class MoveSideWays : MonoBehaviour
{
    #region Variables
    [SerializeField] private float Move_speed = 20f;
    private Rigidbody2D this_RB;
    private Transform this_Transform;
    private bool is_on_direction_change = false;
    [HideInInspector] public bool is_time_to_move;
    [HideInInspector] public bool is_anim_on;
    #endregion

    #region UnityMethods
    void Start()
    {
        is_anim_on = false;
        is_time_to_move = false;
        this_Transform = transform;
        this_RB = GetComponent<Rigidbody2D>();
        is_on_direction_change = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            is_time_to_move = true;
            GetComponent<Animator>().SetBool("is_Running", true);
        }
        if (is_anim_on)
        {
            GetComponent<Animator>().SetBool("is_Running", true);
            is_anim_on = false;
        }
    }

    void FixedUpdate()
    {
        if (is_time_to_move)
        {
            if (!is_on_direction_change)
                this_Transform.Translate(this_Transform.right * Move_speed * Time.fixedDeltaTime);
            else
                this_Transform.Translate(-this_Transform.right * Move_speed * Time.fixedDeltaTime);
        }
    }
    #endregion

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" || collision.collider.tag == "Ground")
            ChangeDirection();
    }

    private void ChangeDirection()
    {
        if (!is_on_direction_change)
            is_on_direction_change = true;
        else
            is_on_direction_change = false;

        this_Transform.Rotate(transform.up, -180f);
    }
}
