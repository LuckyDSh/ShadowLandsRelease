/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class GroundAnim : MonoBehaviour
{
    #region Variables
    [SerializeField] private float move_speed = 2f;
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

    #region UnityMethods
    void Start()
    {
        is_finished = true;
    }

    void FixedUpdate()
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
