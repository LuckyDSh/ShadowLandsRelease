/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;
using UnityEngine.UI;

public class INFO_HOLDER : MonoBehaviour
{
    #region Variables
    public static INFO_HOLDER instance;
    public static Vector2 position;
    public static float diamonds_num;
    public static float distance;
    [SerializeField] private string soundTrack;

    [Tooltip("Energy section")]
    [Space]
    // For Jump -1
    // For Fall45 -2
    // For Act -5
    [SerializeField] private Slider energy_slider;
    [SerializeField] private Text energy_txt;
    [SerializeField] private int maxEnergy_num;
    [SerializeField] private int currentEnergy_num;
    [SerializeField] private GameObject energy_effect;
    private static bool is_state_changed;

    private static string energy_txt_buffer;
    public static string soundTrack_buffer;
    public bool is_same_theme_found;
    public static int currentEnergy_num_buffer;
    public static int maxEnergy_num_buffer;
    #endregion

    public static bool Change_Energy_State(int num, bool is_changed = true)
    {
        currentEnergy_num_buffer -= num;
        energy_txt_buffer = currentEnergy_num_buffer.ToString() + "/" + maxEnergy_num_buffer.ToString();
        is_state_changed = is_changed;

        return is_changed;
    }

    #region UnityMethods
    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        is_state_changed = false;
        currentEnergy_num_buffer = currentEnergy_num;
        maxEnergy_num_buffer = maxEnergy_num;
        soundTrack_buffer = soundTrack;

        energy_txt.text = currentEnergy_num_buffer.ToString() + "/" + maxEnergy_num_buffer.ToString();
        energy_slider.maxValue = maxEnergy_num_buffer;
        energy_slider.value = maxEnergy_num_buffer;

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
        AudioManager.instance.Play("Forest");
    }

    private void Update()
    {
        if (is_state_changed)
        {
            energy_effect.SetActive(false);
            energy_effect.SetActive(true);
            energy_slider.value = currentEnergy_num_buffer;
            energy_txt.text = energy_txt_buffer;

            is_state_changed = false;
        }
    }
    #endregion
}
