/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class Disable_OnClick : MonoBehaviour
{
	#region Variables
	#endregion

    #region UnityMethods
   
    void Update()
    {
        if(Input.touchCount > 0)
        {
            gameObject.SetActive(false);
        }
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.SetActive(false);
        }
    }

	#endregion
}
