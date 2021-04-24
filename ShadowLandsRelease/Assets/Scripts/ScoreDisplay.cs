/*
*	TickLuck
*	All rights reserved
*/
using BayatGames.SaveGameFree;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    #region Variables
    [SerializeField] private Text score_txt;
    #endregion

    #region UnityMethods
    void Start()
    {
        if (SaveGame.Load<int>("SCORE") != 0)
            score_txt.text = SaveGame.Load<int>("SCORE").ToString();
    }
    #endregion
}
