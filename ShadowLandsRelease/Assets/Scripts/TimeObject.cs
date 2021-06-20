/*
*	TickLuck
*	All rights reserved
*/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeObject : MonoBehaviour
{
    bool isRewinding = false;

    public float recordTime = 5f;
    [SerializeField] private float runningOut_speed;
    private float recordTime_buffer;

    List<PointOfTime> pointsInTime;

    Rigidbody rb;

    [SerializeField] private Image fill_img;

    //[SerializeField] private Button activation_btn;

    // Use this for initialization
    void Start()
    {
        recordTime_buffer = recordTime;
        pointsInTime = new List<PointOfTime>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    { 
        if (ButtonPressed_Released_checker.buttonPressed && recordTime > 0)
        {
            StartRewind();
            recordTime -= Time.deltaTime * runningOut_speed;
            fill_img.fillAmount = (recordTime / recordTime_buffer);
        }
        else
        {
            StopRewind();
        }

        if (Input.GetKeyDown(KeyCode.Return))
            StartRewind();
        if (Input.GetKeyUp(KeyCode.Return))
            StopRewind();
    }

    void FixedUpdate()
    {
        if (isRewinding)
            Rewind();
        else
            Record();
    }

    void Rewind()
    {
        if (pointsInTime.Count > 0)
        {
            PointOfTime pointInTime = pointsInTime[0];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            pointsInTime.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }

    }

    void Record()
    {
        if (pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }

        pointsInTime.Insert(0, new PointOfTime(transform.position, transform.rotation));
    }

    public void StartRewind()
    {
        isRewinding = true;
        //rb.isKinematic = true;
    }

    public void StopRewind()
    {
        isRewinding = false;
        //rb.isKinematic = false;
    }
}
