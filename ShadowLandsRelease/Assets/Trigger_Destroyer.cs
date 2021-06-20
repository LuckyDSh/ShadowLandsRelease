/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class Trigger_Destroyer : MonoBehaviour
{
    #region Variables
    [SerializeField] private string trigger_name;
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == trigger_name)
        {
            Destroy(gameObject);
        }
    }
}
