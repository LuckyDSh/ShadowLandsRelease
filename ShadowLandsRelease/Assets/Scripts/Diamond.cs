/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class Diamond : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Transform this_transform;
    private Transform target;
    private bool is_time_to_move;
    private Rigidbody2D rb;
    private Vector3 direction;

    private void Start()
    {
        is_time_to_move = false;
        this_transform = transform;
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("DIAMOND_POINT").transform;
        direction = (target.position - transform.position).normalized;
    }

    private void Update()
    {
        if (is_time_to_move)
            //this_transform.position = this_transform.position + direction * speed * Time.deltaTime;
            //this_transform.position += new Vector3(-direction.x * speed * Time.deltaTime,
            //    direction.y * speed * Time.deltaTime, 0);
            this_transform.position = Vector2.Lerp(transform.position, target.position, speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            AudioManager.instance.Play("Diamond");
            GAME_CONTROLLER.NANO_VIBRATION();

            PLAYER_MOVEMENT_SYSTEM.is_diamond_picked = true;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;

            is_time_to_move = true;
        }
        if (collision.tag == "DIAMOND_POINT")
        {
            Destroy(gameObject);
        }
    }
}
