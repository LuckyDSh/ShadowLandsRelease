/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class DRONE_CONTROLLER : MonoBehaviour
{
    #region Variables
    [SerializeField] private Transform initialPosition_transform;
    [SerializeField] private GameObject shield;

    private bool is_UNITY_EDITOR;
    #endregion

    #region UnityMethods
    void Start()
    {
        //initialPosition = transform.position;
        if (shield == null)
        {
            shield = transform.GetChild(0).gameObject;
        }

#if UNITY_EDITOR
        is_UNITY_EDITOR = true;
#else
        is_UNITY_EDITOR = false;
#endif
    }

    void FixedUpdate()
    {
        if (Input.touchCount > 0 || Input.GetMouseButton(0))
        {
            Vector3 direction;
            shield.SetActive(true);
            //Touch touch = Input.GetTouch(0);

            if (Input.touchCount <= 0)
                direction = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            else
                direction = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

            if (is_UNITY_EDITOR)
            {
                Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(rayOrigin, out hitInfo))
                {
                    Vector2 new_direction = hitInfo.point;
                    transform.position = new_direction;
                    Debug.Log("MOVED");
                }
            }

            direction.z = 0;
            transform.position = direction;
        }
        else
        {
            shield.SetActive(false);
            transform.position = initialPosition_transform.position;
            //transform.Translate((Vector2)initialPosition_transform.position * Time.fixedDeltaTime * 100f);
        }
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ball")
        {
            // AudioManager Play Some Sound
            AudioManager.instance.Play("Pop");
            Destroy(collision.gameObject);
        }
    }
}
