/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class Balls_System : MonoBehaviour
{
    #region Variables
    [SerializeField] private SpriteRenderer[] lands_to_color;
    [SerializeField] private Collider2D[] border_colliders;
    private Color new_color;
    #endregion

    #region UnityMethods
    void Start()
    {
        new_color = Color.white;
        new_color.a = 150f;
    }
    #endregion

    public void Activate()
    {
        foreach (var border in border_colliders)
        {
            border.enabled = false;
        }
        foreach (var land in lands_to_color)
        {
            land.color = new_color;
        }
    }
}
