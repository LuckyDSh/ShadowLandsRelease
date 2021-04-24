/*
*	TickLuck
*	All rights reserved
*/

using UnityEngine;

public class RockFalling_Land : MonoBehaviour
{
    #region Variables
    [SerializeField] public Rigidbody2D[] rocks;
    #endregion

    #region UnityMethods
    void Start()
    {
        foreach (var rock in rocks)
        {
            rock.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
    #endregion
}
