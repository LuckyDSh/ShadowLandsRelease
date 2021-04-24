/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class LandWeight : MonoBehaviour
{
    [SerializeField] private float endDistance = 0.6f;
    private Rigidbody2D this_rb;

    private void Start()
    {
        this_rb = GetComponent<Rigidbody2D>();
        this_rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void ConnectedBodyEnd(Rigidbody2D EndBody)
    {
        HingeJoint2D join = gameObject.AddComponent<HingeJoint2D>();
        join.autoConfigureConnectedAnchor = false;
        join.connectedBody = EndBody;
        join.anchor = Vector2.zero;
        join.connectedAnchor = new Vector2(0f, endDistance);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "BLOCK")
        {
            this_rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
