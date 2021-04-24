/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class Disabler : MonoBehaviour
{
    #region Variables
    public float minDistance;
    private Transform target;
    private SpriteRenderer meshRenderer;
    #endregion

    #region UnityMethods

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        meshRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (target != null)
            if (Vector3.Distance(transform.position, target.position) <= minDistance)
            {
                if (meshRenderer != null)
                    meshRenderer.enabled = true;
            }
            else
                meshRenderer.enabled = false;
    }

    #endregion
}
