/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class PointOfTime : MonoBehaviour
{
    #region Variables
    public Vector2 position;
    public Quaternion rotation;
    #endregion

    public PointOfTime(Vector2 _position, Quaternion _rotation)
    {
        position = _position;
        rotation = _rotation;
    }
}
