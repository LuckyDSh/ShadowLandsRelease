/*
*	TickLuck
*	All rights reserved
*/
using BayatGames.SaveGameFree;
using UnityEngine;
using UnityEngine.UI;

public class CHALLENGE : MonoBehaviour
{
    [SerializeField] private GameObject line;
    [SerializeField] public string challenge_objective;
    [SerializeField] public bool is_complited;
    [SerializeField] public int Goal_to_reach;
    [SerializeField] public int current_progress;
    [SerializeField] private Slider progress_slider;

    private void Awake()
    {
        if (progress_slider == null)
            progress_slider = GetComponentInChildren<Slider>();
        progress_slider.maxValue = Goal_to_reach;
        progress_slider.value = 0;
    }

    private void OnEnable()
    {
        bool temp_bool = SaveGame.Load<bool>("IS_COMPLITED" + challenge_objective);
        if (temp_bool)
        {
            line.SetActive(true);
            current_progress = Goal_to_reach;
            progress_slider.value = progress_slider.maxValue;
        }
        else
        {
            line.SetActive(false);
            is_complited = false;
            current_progress = SaveGame.Load<int>("CHALLENGE_PROGRESS" + challenge_objective);
            progress_slider.value = current_progress;
            if (current_progress >= Goal_to_reach)
                is_complited = true;
        }
    }
}
