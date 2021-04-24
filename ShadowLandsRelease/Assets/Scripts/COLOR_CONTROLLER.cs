/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class COLOR_CONTROLLER : MonoBehaviour
{
    #region Variables
    [SerializeField] private Color[] playerColors;
    [SerializeField] private Color light_theme_UI;
    [SerializeField] private Color dark_theme_UI;
    [SerializeField] private Color light_theme_land;
    [SerializeField] private Color dark_theme_land;
    [SerializeField] private Color highlight_color1;
    [SerializeField] private Color highlight_color2;


    public static Color[] playerColors_buffer;
    public static Color light_theme_UI_buffer;
    public static Color dark_theme_UI_buffer;
    public static Color light_theme_land_buffer;
    public static Color dark_theme_land_buffer;
    public static Color currentColor_forBase;
    public static Color currentColor_forAccent;
    #endregion

    #region UnityMethods
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        playerColors_buffer = playerColors;
        light_theme_UI_buffer = light_theme_UI;
        dark_theme_UI_buffer = dark_theme_UI;
        light_theme_land_buffer = light_theme_land;
        dark_theme_land_buffer = dark_theme_land;
    }

    void Update()
    {
        if (ColorDestributor.is_color_changed)
        {
            if (currentColor_forBase != light_theme_UI)
            {
                currentColor_forBase = light_theme_UI;
                currentColor_forAccent = dark_theme_UI;
            }
            else if (currentColor_forBase != dark_theme_UI)
            {
                currentColor_forBase = dark_theme_UI;
                currentColor_forAccent = light_theme_UI;
            }
        }
    }
    #endregion
}
