/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class BOOM_Controller : MonoBehaviour
{
    #region Variables
    public float radius;
    public float force = 200f;
    [SerializeField]
    private GameObject explosionEffect;
    #endregion

    public void Explode()
    {
        // if (explosionEffect != null)
        GameObject effect = Instantiate(explosionEffect, transform.position, transform.rotation);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D nearbyObj in colliders)
        {
            Rigidbody2D rb = nearbyObj.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = (Vector2)transform.position - rb.position;
                //rb.AddForce(direction * force, ForceMode2D.Impulse);
            }
        }

        Destroy(effect, 2f);
        Destroy(gameObject);
    }
}
