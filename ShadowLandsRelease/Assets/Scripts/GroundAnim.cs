/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class GroundAnim : MonoBehaviour
{
    #region Variables
    [SerializeField] private float move_speed = 2f;
    [SerializeField] private bool Updown;
    private bool is_finished;
    #endregion

    public void LevitateUP()
    {
        transform.position = transform.position + transform.up * move_speed * Time.fixedDeltaTime;
    }

    public void LevitateDOWN()
    {
        transform.position = transform.position - transform.up * move_speed * Time.fixedDeltaTime;
    }

    public void LevitateRIGHT()
    {
        transform.position = transform.position + transform.right * move_speed * Time.fixedDeltaTime;
    }

    public void LevitateLEFT()
    {
        transform.position = transform.position - transform.right * move_speed * Time.fixedDeltaTime;
    }

    #region UnityMethods
    void Start()
    {
        is_finished = true;
    }

    void FixedUpdate()
    {
        if (Updown)
        {
            if (is_finished)
            {
                LevitateUP();
            }
            else
            {
                LevitateDOWN();
            }
        }
        else
        {
            if (is_finished)
            {
                LevitateLEFT();
            }
            else
            {
                LevitateRIGHT();
            }
        }
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "GROUND_TRIGGER")
        {
            if (is_finished)
                is_finished = false;
            else
                is_finished = true;
        }
    }
}
