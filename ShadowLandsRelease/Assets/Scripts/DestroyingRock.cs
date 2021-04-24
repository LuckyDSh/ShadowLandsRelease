/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class DestroyingRock : MonoBehaviour
{
    #region Variables
    [SerializeField] private Rigidbody2D[] pieces;
    private Animator animator;
    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    #endregion

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            animator.enabled = true;
            AudioManager.instance.Play("StoneDrop");

            foreach (var item in pieces)
            {
                item.constraints = RigidbodyConstraints2D.FreezeRotation;
                item.AddForce(-item.transform.up * 0.5f);
            }

            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            PLAYER_MOVEMENT_SYSTEM.is_destroyingRockLand = true;
        }
    }
}
