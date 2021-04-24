/*
*	TickLuck
*	All rights reserved
*/
using System.Collections;
using UnityEngine;

public class MenuDisabler : MonoBehaviour
{
    #region Variables
    private Animator animator;
    #endregion

    #region UnityMethods
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("is_on_close", false);
    }
    #endregion

    public void OnEnable()
    {
        AudioManager.instance.Play("UI_Transition");
    }

    public void Close()
    {
        StartCoroutine("Close_enum");
    }

    private IEnumerator Close_enum()
    {
        AudioManager.instance.Play("UI_Transition");
        animator.SetBool("is_on_close", true);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("is_on_close", false);
        gameObject.SetActive(false);
    }
}
