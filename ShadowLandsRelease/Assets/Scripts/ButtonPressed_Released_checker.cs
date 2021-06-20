/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPressed_Released_checker : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static bool buttonPressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonPressed = false;
    }
}
