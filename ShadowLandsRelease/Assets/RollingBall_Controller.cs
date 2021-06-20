/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class RollingBall_Controller : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject rollingBall;
    #endregion

    #region UnityMethods
    void Start()
    {
        rollingBall.SetActive(false);
    }
    #endregion

    public void ActivateObstacle()
    {
        rollingBall.SetActive(true);
    }
}
