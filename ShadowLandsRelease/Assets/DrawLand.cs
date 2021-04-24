/*
*	TickLuck
*	All rights reserved
*/
using System.Collections.Generic;
using UnityEngine;

public class DrawLand : MonoBehaviour
{
    #region Variables
    [SerializeField] private Collider2D[] colliders_to_acticate;
    [SerializeField] private List<GameObject> pieces;
    #endregion

    #region UnityMethods
    void Start()
    {
        foreach (var item in colliders_to_acticate)
        {
            item.enabled = false;
        }
    }

    void Update()
    {
        foreach (var item in pieces)
        {
            if (item == null)
                pieces.Remove(item);
        }

        if (pieces.Count < 1)
        {
            foreach (var item in colliders_to_acticate)
            {
                item.enabled = true;
            }
        }
    }
    #endregion
}
