/*
*	TickLuck
*	All rights reserved
*/
using BayatGames.SaveGameFree;
using UnityEngine;

public class SAVE_GAME_HOLDER : MonoBehaviour
{
    #region Variables
    public static int index_of_opened_level;
    #endregion

    #region UnityMethods
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (SaveGame.Load<int>("LEVEL") > 0)
        {
            index_of_opened_level = SaveGame.Load<int>("LEVEL");
        }
    }
    #endregion
}
