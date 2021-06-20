/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class SUN : MonoBehaviour
{
    #region Variables
    [SerializeField] private SpriteRenderer BG_to_fade;
    [SerializeField] private float fade_speed;
    private bool is_time_to_fade;
    private Color color_buffer;
    private Color initial_color_buffer;
    #endregion

    private void FADE()
    {
        if (color_buffer.a > 0)
        {
            color_buffer.a -= Time.deltaTime * fade_speed;
            BG_to_fade.color = color_buffer;
        }
        else
        {
            color_buffer.a = 0f;
            BG_to_fade.color = color_buffer;
            return;
        }
    }

    private void INCREASE()
    {
        if (color_buffer.a < 1f)
        {
            color_buffer.a += Time.deltaTime * fade_speed;
            BG_to_fade.color = color_buffer;
        }
        else
        {
            color_buffer.a = 1f;
            BG_to_fade.color = color_buffer;
            return;
        }
    }

    #region UnityMethods
    void Start()
    {
        is_time_to_fade = false;
        initial_color_buffer = BG_to_fade.color;
        color_buffer = initial_color_buffer;
    }

    void Update()
    {
        if (is_time_to_fade)
        {
            FADE();
        }
        else
        {
            INCREASE();
        }
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "SUN_TRIGGER")
        {
            if (is_time_to_fade)
            {
                is_time_to_fade = false;
            }
            else
            {
                is_time_to_fade = true;
            }
        }
    }
}
