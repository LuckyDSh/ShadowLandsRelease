/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class INFO_HOLDER : MonoBehaviour
{
    #region Variables
    public static INFO_HOLDER instance;
    public static Vector2 position;
    public static float diamonds_num;
    public static float distance;
    [SerializeField] private string soundTrack;
    public static string soundTrack_buffer;
    public bool is_same_theme_found;
    #endregion

    #region UnityMethods
    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        soundTrack_buffer = soundTrack;

        foreach (var sound in AudioManager.instance.sounds)
        {
            is_same_theme_found = false;

            if (sound.name != soundTrack)
            {
                AudioManager.instance.Stop(sound.name);
            }
            else
            {
                is_same_theme_found = true;
            }
        }

        if (!is_same_theme_found)
            AudioManager.instance.Play(soundTrack);
    }
    #endregion
}
