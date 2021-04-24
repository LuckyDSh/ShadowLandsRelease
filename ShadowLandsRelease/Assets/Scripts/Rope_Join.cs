/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class Rope_Join : MonoBehaviour
{
    #region Variables
    [SerializeField] private Rigidbody2D hook;
    [SerializeField] private GameObject link_prefab;
    [SerializeField] private int link_number;
    [SerializeField] private LandWeight land;
    #endregion

    void Start()
    {
        CreateRope();
    }

    private void CreateRope()
    {
        Rigidbody2D rb = hook;

        for (int i = 0; i < link_number; i++)
        {
            GameObject link = Instantiate(link_prefab, transform);
            HingeJoint2D join = link.GetComponent<HingeJoint2D>();
            join.connectedBody = rb;

            if (i < link_number - 1)
                rb = link.GetComponent<Rigidbody2D>();
            else
                land.ConnectedBodyEnd(link.GetComponent<Rigidbody2D>());
        }
    }
}
