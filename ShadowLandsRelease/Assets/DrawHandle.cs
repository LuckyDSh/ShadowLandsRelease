/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class DrawHandle : MonoBehaviour
{
    #region Variables
    private Vector2 initialPosition;
    [SerializeField] private GameObject tutorial;
    #endregion

    #region UnityMethods
    public void Start()
    {
        initialPosition = transform.position;
        tutorial.SetActive(true);
    }

    void FixedUpdate()
    {
        if (Input.touchCount > 0 || Input.GetMouseButton(0))
        {
            Vector3 direction;
            //Touch touch = Input.GetTouch(0);

            if (Input.touchCount <= 0)
                direction = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            else
                direction = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
#if false
            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                Vector2 direction = hitInfo.point;
                transform.position = direction;
                Debug.Log("MOVED");
            }
#endif
            direction.z = 0;
            transform.position = direction;
        }
        else
        {
            transform.position = initialPosition;
        }
    }
    #endregion

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Piece")
        {
            AudioManager.instance.Play("DrawLand");
            tutorial.SetActive(false);
            Destroy(collision.gameObject);
        }
    }
}
