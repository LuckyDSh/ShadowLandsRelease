/*
*	TickLuck
*	All rights reserved
*/
using BayatGames.SaveGameFree;
using System.Collections.Generic;
using UnityEngine;

public class Challenge_Controller : MonoBehaviour
{
    [SerializeField] private CHALLENGE[] challenges_set;
    [SerializeField] private List<string> challenges_objective;
    private static int Jump_score;
    private static int AvoidFall_score;
    private static int LandSpecialLand_score;
    private static int Land_score;
    private static int LongJump_score;
    private static int CutHangedLand_score;
    private static int FindEasterEgg_score;
    private static int CatchDiamond_score;
    private static int LandOnShadow_score;

    void Start()
    {
        DontDestroyOnLoad(gameObject);

        foreach (var item in challenges_set)
        {
            if (item != null)
                challenges_objective.Add(item.challenge_objective);
        }

        #region Config
        foreach (var challenge in challenges_set)
        {
            if (challenge != null)
                if (challenge.challenge_objective == "Jump")
                {
                    if (!challenge.is_complited)
                    {
                        challenge.current_progress = Jump_score;

                        if (challenge.current_progress >= challenge.Goal_to_reach)
                        {
                            challenge.is_complited = true;
                        }

                        SaveGame.Save("IS_COMPLITED" + challenge.challenge_objective, challenge.is_complited);
                        SaveGame.Save("CHALLENGE_PROGRESS" + challenge.challenge_objective, challenge.current_progress);
                    }
                }
                else if (challenge.challenge_objective == "AvoidFall")
                {
                    if (!challenge.is_complited)
                    {
                        challenge.current_progress = AvoidFall_score;

                        if (challenge.current_progress >= challenge.Goal_to_reach)
                        {
                            challenge.is_complited = true;
                        }

                        SaveGame.Save("IS_COMPLITED" + challenge.challenge_objective, challenge.is_complited);
                        SaveGame.Save("CHALLENGE_PROGRESS" + challenge.challenge_objective, challenge.current_progress);
                    }
                }
                else if (challenge.challenge_objective == "LandSpecialLand")
                {
                    if (!challenge.is_complited)
                    {
                        challenge.current_progress = LandSpecialLand_score;

                        if (challenge.current_progress >= challenge.Goal_to_reach)
                        {
                            challenge.is_complited = true;
                        }

                        SaveGame.Save("IS_COMPLITED" + challenge.challenge_objective, challenge.is_complited);
                        SaveGame.Save("CHALLENGE_PROGRESS" + challenge.challenge_objective, challenge.current_progress);
                    }
                }
                else if (challenge.challenge_objective == "Land")
                {
                    if (!challenge.is_complited)
                    {
                        challenge.current_progress = Land_score;

                        if (challenge.current_progress >= challenge.Goal_to_reach)
                        {
                            challenge.is_complited = true;
                        }

                        SaveGame.Save("IS_COMPLITED" + challenge.challenge_objective, challenge.is_complited);
                        SaveGame.Save("CHALLENGE_PROGRESS" + challenge.challenge_objective, challenge.current_progress);
                    }
                }
                else if (challenge.challenge_objective == "LongJump")
                {
                    if (!challenge.is_complited)
                    {
                        challenge.current_progress = LongJump_score;

                        if (challenge.current_progress >= challenge.Goal_to_reach)
                        {
                            challenge.is_complited = true;
                        }

                        SaveGame.Save("IS_COMPLITED" + challenge.challenge_objective, challenge.is_complited);
                        SaveGame.Save("CHALLENGE_PROGRESS" + challenge.challenge_objective, challenge.current_progress);
                    }
                }
                else if (challenge.challenge_objective == "CutHangedLand")
                {
                    if (!challenge.is_complited)
                    {
                        challenge.current_progress = CutHangedLand_score;

                        if (challenge.current_progress >= challenge.Goal_to_reach)
                        {
                            challenge.is_complited = true;
                        }

                        SaveGame.Save("IS_COMPLITED" + challenge.challenge_objective, challenge.is_complited);
                        SaveGame.Save("CHALLENGE_PROGRESS" + challenge.challenge_objective, challenge.current_progress);
                    }
                }
                else if (challenge.challenge_objective == "FindEasterEgg")
                {
                    if (!challenge.is_complited)
                    {
                        challenge.current_progress = FindEasterEgg_score;

                        if (challenge.current_progress >= challenge.Goal_to_reach)
                        {
                            challenge.is_complited = true;
                        }

                        SaveGame.Save("IS_COMPLITED" + challenge.challenge_objective, challenge.is_complited);
                        SaveGame.Save("CHALLENGE_PROGRESS" + challenge.challenge_objective, challenge.current_progress);
                    }
                }
                else if (challenge.challenge_objective == "CatchDiamond")
                {
                    if (!challenge.is_complited)
                    {
                        challenge.current_progress = CatchDiamond_score;

                        if (challenge.current_progress >= challenge.Goal_to_reach)
                        {
                            challenge.is_complited = true;
                        }

                        SaveGame.Save("IS_COMPLITED" + challenge.challenge_objective, challenge.is_complited);
                        SaveGame.Save("CHALLENGE_PROGRESS" + challenge.challenge_objective, challenge.current_progress);
                    }
                }
                else if (challenge.challenge_objective == "LandOnShadow")
                {
                    if (!challenge.is_complited)
                    {
                        challenge.current_progress = LandOnShadow_score;

                        if (challenge.current_progress >= challenge.Goal_to_reach)
                        {
                            challenge.is_complited = true;
                        }

                        SaveGame.Save("IS_COMPLITED" + challenge.challenge_objective, challenge.is_complited);
                        SaveGame.Save("CHALLENGE_PROGRESS" + challenge.challenge_objective, challenge.current_progress);
                    }
                }
        }
        #endregion

    }

    public void Jump()
    {
        Jump_score++;
    }
    public void AvoidFall()
    {
        AvoidFall_score++;
    }
    public void LandSpecialLand()
    {
        LandSpecialLand_score++;
    }
    public void Land()
    {
        Land_score++;
    }
    public void LongJump()
    {
        LongJump_score++;
    }
    public void CutHangedLand()
    {
        CutHangedLand_score++;
    }
    public void FindEasterEgg()
    {
        FindEasterEgg_score++;
    }
    public void CatchDiamond()
    {
        CatchDiamond_score++;
    }
    public void LandOnShadow()
    {
        LandOnShadow_score++;
    }

    private void OnApplicationQuit()
    {
        SAVE_CHALLENGE_PROGRESS();
    }

    public void SAVE_CHALLENGE_PROGRESS()
    {
        foreach (var challenge in challenges_set)
        {
            SaveGame.Save("IS_COMPLITED" + challenge.challenge_objective, challenge.is_complited);
            SaveGame.Save("CHALLENGE_PROGRESS" + challenge.challenge_objective, challenge.current_progress);
        }
    }
}
