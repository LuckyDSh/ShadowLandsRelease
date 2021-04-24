/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;
using UnityEngine.UI;

public class ColorDestributor : MonoBehaviour
{
    #region Variables
    [SerializeField] private Image[] images_forAccent;
    [SerializeField] private Image[] images_forBase;
    [SerializeField] private Text[] text_forAccent;
    [SerializeField] private Text[] text_forBase;

    // when is_color_changed is set --> COLOR_CONTROLLER swaps the colors for base
    public static bool is_color_changed = false;
    #endregion

    #region UnityMethods
    void Update()
    {
        if (is_color_changed)
        {
            SET_COLOR();
            is_color_changed = false;
        }
    }
    #endregion

    public void SET_COLOR()
    {
        foreach (var item in images_forAccent)
        {
            item.color = COLOR_CONTROLLER.currentColor_forAccent;
        }
        foreach (var item in images_forBase)
        {
            item.color = COLOR_CONTROLLER.currentColor_forBase;
        }
        foreach (var item in text_forAccent)
        {
            item.color = COLOR_CONTROLLER.currentColor_forAccent;
        }
        foreach (var item in text_forBase)
        {
            item.color = COLOR_CONTROLLER.currentColor_forBase;
        }
    }
}
