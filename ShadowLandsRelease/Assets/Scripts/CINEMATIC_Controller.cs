/*
*	TickLuck
*	All rights reserved
*/
using System.Collections;
using UnityEngine;

public class CINEMATIC_Controller : MonoBehaviour
{
    #region Variables
    [Header("Camera")]
    [Space]
    [SerializeField] private GameObject MainCamera;
    [SerializeField] private Transform MainCamera_initialPos;
    [SerializeField] private Transform point1_UP;
    [SerializeField] private Transform point2_DOWN;
    [Header("Other")]
    [Space]
    [SerializeField] private GameObject Shadow;
    [SerializeField] private GameObject ShineEffect;
    [SerializeField] private GameObject Sun;
    [SerializeField] private GameObject Sun_broken;
    [SerializeField] private GameObject Sun_broken_completely;
    [SerializeField] private GameObject Sun_broken_pieces;
    [SerializeField] private GameObject[] Sun_broken_completely_pieces;
    [SerializeField] private GameObject[] Story_texts;
    [SerializeField] private GameObject Final_text_pannel;
    [SerializeField] private MoveSideWays[] People_animators;
    [Header("BOOM")]
    [Space]
    [SerializeField] private BOOM_Controller[] Boom_points_for_parts;
    [SerializeField] private BOOM_Controller Boom_point_main;
    [Header("Parameters")]
    [Space]
    [SerializeField] private float move_speed;
    [SerializeField] private float hold_duration;
    [SerializeField] private float up_duration;
    [SerializeField] private float down_duration;
    private bool is_time_to_go_up;
    private bool is_time_to_go_down;

    //For testing purposes
    [Header("For Testing Purposes...")]
    public float distance_buffer_UP;
    public float distance_buffer_DOWN;
    #endregion

    #region UnityMethods
    void Start()
    {
        foreach (var text in Story_texts)
        {
            text.SetActive(false);
        }
        foreach (var anim in People_animators)
        {
            anim.is_anim_on = false;
            anim.is_time_to_move = false;
        }

        Sun_broken_completely.SetActive(false);

        foreach (var item in Sun_broken_completely_pieces)
        {
            item.SetActive(false);
        }
        is_time_to_go_up = false;
        is_time_to_go_down = false;
        MainCamera_initialPos = MainCamera.transform;
        Shadow.SetActive(false);
        StartCoroutine("Cinematic");
    }

    void FixedUpdate()
    {
        if (is_time_to_go_up)
        {
            MoveCamera_UP();
        }
        if (is_time_to_go_down)
        {
            MoveCamera_DOWN();
        }
    }
    #endregion

    public void MoveCamera_UP()
    {
        distance_buffer_UP = Vector2.Distance(MainCamera.transform.position, point1_UP.position);

        if (Vector2.Distance(MainCamera.transform.position, point1_UP.position) >= 0.1f)
        {
            MainCamera.transform.Translate(MainCamera.transform.up * Time.fixedDeltaTime * move_speed);
        }
        else
        {
            is_time_to_go_up = false;
        }
    }
    public void MoveCamera_DOWN()
    {
        distance_buffer_DOWN = Vector2.Distance(MainCamera.transform.position, point2_DOWN.position);

        if (Vector2.Distance(MainCamera.transform.position, point2_DOWN.position) >= 0.1f)
        {
            MainCamera.transform.Translate(-MainCamera.transform.up * Time.fixedDeltaTime * move_speed);
        }
        else
        {
            is_time_to_go_down = false;
        }
    }

    public IEnumerator CameraShake()
    {
        MainCamera.transform.position -= Vector3.left * 2;
        yield return new WaitForSeconds(0.2f);
        MainCamera.transform.position -= Vector3.right * 2;
        yield return new WaitForSeconds(0.2f);
        MainCamera.transform.position -= Vector3.left * 2;
        yield return new WaitForSeconds(0.2f);
        MainCamera.transform.position -= Vector3.right * 2;
        yield return new WaitForSeconds(0.2f);
    }

    public IEnumerator STAGE01()
    {
        //Arrange Initial position and open the first message
        MainCamera.transform.position = new Vector3(point1_UP.position.x, point1_UP.position.y, MainCamera_initialPos.position.z);
        Story_texts[0].SetActive(true);

        yield return new WaitForSeconds(hold_duration); // FIRST STAGE
    }
    public IEnumerator STAGE02()
    {
        //Text change + start of moving down
        Story_texts[0].SetActive(false);

        is_time_to_go_down = true;

        Story_texts[1].SetActive(true);

        yield return new WaitForSeconds(down_duration);// SECOND STAGE
    }
    public IEnumerator STAGE03()
    {
        //Text rearrengement + Shaking Effect 
        is_time_to_go_up = true;

        Story_texts[1].SetActive(false);
        //StartCoroutine("CameraShake");
        Story_texts[2].SetActive(true);

        yield return new WaitForSeconds(up_duration / 2);

        Story_texts[2].SetActive(false);
        Story_texts[3].SetActive(true);

        yield return new WaitForSeconds(up_duration / 2); // THIRD STAGE

    }
    public IEnumerator STAGE04()
    {
        //Set pieces and Explode effect + Text change
        Sun_broken.SetActive(true);
        Sun.SetActive(false);

        yield return new WaitForSeconds(up_duration / 2);

        Sun_broken.SetActive(false);
        Sun_broken_pieces.SetActive(true);
        Boom_point_main.Explode();

        yield return new WaitForSeconds(up_duration / 3);

        Sun_broken_pieces.SetActive(false);
        foreach (var item in Sun_broken_completely_pieces)
        {
            item.SetActive(true);
        }

        foreach (var point in Boom_points_for_parts)
        {
            point.Explode();
        }

        Story_texts[3].SetActive(false);
        Shadow.SetActive(true);
        yield return new WaitForSeconds(up_duration / 2); // FOURTH STAGE
    }
    public IEnumerator STAGE05()
    {
        Story_texts[4].SetActive(true);

        foreach (var anim in People_animators)
        {
            anim.is_anim_on = true;
            anim.is_time_to_move = true;
        }

        is_time_to_go_down = true;

        yield return new WaitForSeconds(down_duration);
        Story_texts[4].SetActive(false);

        Final_text_pannel.SetActive(true);
    }

    public IEnumerator Cinematic()
    {
        StartCoroutine("STAGE01");
        yield return new WaitForSeconds(hold_duration);
        StartCoroutine("STAGE02");
        yield return new WaitForSeconds(hold_duration);
        StartCoroutine("STAGE03");
        yield return new WaitForSeconds(hold_duration);
        StartCoroutine("STAGE04");
        yield return new WaitForSeconds(hold_duration);
        StartCoroutine("STAGE05");
    }
}
