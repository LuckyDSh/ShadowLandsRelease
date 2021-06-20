/*
*	TickLuck
*	All rights reserved
*/
using BayatGames.SaveGameFree;
using DragonBones;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PLAYER_MOVEMENT_SYSTEM : MonoBehaviour
{
    #region Variables
    [Header("MOVEMENT SECTION")]
    [Space]
    [SerializeField] private float forward_speed = 5f;
    [SerializeField] private float jump_force = 10f;
    [SerializeField] private float down45_force = 10f;
    [SerializeField] private float up_force = 5f;
    [SerializeField] private float new_gravity_scale;
    [SerializeField] private float action_duration = 3f;
    [SerializeField] private float time_slowDown_amount;
    [SerializeField] private bool[] is_On_Jumps;
    [SerializeField] private GameObject fireTurbin;
    [SerializeField] private GameObject drone;
    private BoxCollider2D this_collider;

    [Header("DEATH SECTION")]
    [Space]
    [SerializeField] GameObject[] bodyParts;
    private static bool is_Player_Dead;

    [Header("UI SECTION")]
    [Space]
    [SerializeField] private GameObject GO_UI;
    [SerializeField] private GameObject GW_UI;
    [SerializeField] private GameObject tap_to_play_UI;
    [SerializeField] private UnityEngine.Animation animation_for_accelerationBtn;
    [SerializeField] private GameObject acceleration_btn;
    [SerializeField] private Text distance__num_txt;
    [SerializeField] private Text score_num_txt;
    [SerializeField] private GameObject[] acceleration_UI;
    [SerializeField] private GameObject Shadow;

    [Header("MAP MANAGEMENT SECTION")]
    [Space]
    [Tooltip("Place the sets in order to apear in the map")]
    [SerializeField] private GameObject[] lands_sets;

    [Header("REWARD SECTION")]
    [Space]
    [SerializeField] private Text diamonds_collected_txt;
    [SerializeField] private GameObject reward_txt;
    [SerializeField] private int REWARD_FOR_LONG_JUMP;
    [SerializeField] private int REWARD_FOR_CHECKPOINT;
    [SerializeField] private int REWARD_FOR_NICE_FALL;
    [SerializeField] private int REWARD_FOR_LAND_INTERACTION;
    //private Challenge_Controller challenge_Controller;
    private Text _reward_txt;
    public static int total_score_num;
    public static int collected_diamonds_num;
    public static bool is_diamond_picked;

    private SmoothCameraScript cameraMovementController;
    private UnityArmatureComponent animator;
    private UnityEngine.Transform this_transform;
    private Rigidbody2D this_rb;
    private int index_of_current_landset;
    private bool is_Acceleration_pressed_one_time;
    private bool is_fall_pressed;
    public static float distance_traveled;
    private float current_speed;
    private readonly float SPEED_OFFSET_FOR_DISTANCE_COUNTER = 0.75f;

    //[Header("Obstacles")]
    //private RollingBall_Controller[] RollingBalls; 

    //for movement
    [Tooltip("If set to true - moving RIGHT, else LEFT")]
    [SerializeField] private bool is_on_direction_change;
    private bool is_Grounded;
    private bool is_Jumped;
    private bool moving_right;
    private bool is_crouch_zone_triggered;
    private bool is_run_zone_triggered;
    private bool is_up_zone_triggered;
    private bool is_stone_to_drag_triggered;
    public static bool is_hangedLandCut;
    public static bool is_destroyingRockLand;
    private bool is_strongLanding;
    private bool is_started;

    //for animation
    private bool is_Running;
    private bool is_On_Hill_Running;
    [HideInInspector] public Collider2D upCollidder_buffer;
    //[SerializeField] private RockFalling_Land RockFalling_Land_buffer;
    //private bool is_On_Jump;
    //private List<bool> is_On_Jumps_list;
    #endregion

    #region UnityMethods
    void Start()
    {
        #region INICIALISATION
        is_Player_Dead = false;
        is_hangedLandCut = false;
        is_started = false;
        is_strongLanding = false;
        is_run_zone_triggered = false;
        is_up_zone_triggered = false;
        is_crouch_zone_triggered = false;
        current_speed = forward_speed;
        index_of_current_landset = 0;
        collected_diamonds_num = 0;
        is_Acceleration_pressed_one_time = false;
        is_fall_pressed = false;
        //is_On_Jump = false;
        is_Running = false;
        is_diamond_picked = false;
        animator = GetComponent<UnityArmatureComponent>();
        this_collider = GetComponent<Collider2D>() as BoxCollider2D;
        //is_on_direction_change = false;
        cameraMovementController = Camera.main.GetComponent<SmoothCameraScript>();
        this_transform = transform;
        this_rb = GetComponent<Rigidbody2D>();
        is_Grounded = true;
        is_Jumped = false;
        _reward_txt = reward_txt.GetComponent<Text>();
        Shadow.SetActive(false);
        GO_UI.SetActive(false);
        GW_UI.SetActive(false);
        fireTurbin.SetActive(false);
        animator.animation.Play("idle");

        //if (challenge_Controller == null)
        //{
        //    challenge_Controller =
        //        GameObject.FindGameObjectWithTag("CHALLENGES_CONTROLLER").GetComponent<Challenge_Controller>();
        //}

        if (SaveGame.Load<int>("DISTANCE") > 0)
            distance_traveled = SaveGame.Load<int>("DISTANCE"); //DISTANCE
        else
            distance_traveled = 0f;

        for (int i = 1; i < lands_sets.Length; i++)
        {
            lands_sets[i].SetActive(false);
        }

        foreach (var item in acceleration_UI)
        {
            item.SetActive(false);
        }

        foreach (var part in bodyParts)
        {
            part.GetComponent<PolygonCollider2D>().enabled = false;
            part.GetComponent<Rigidbody2D>().isKinematic = true;
        }

        if (SaveGame.Load<int>("SCORE") != 0)
        {
            total_score_num = SaveGame.Load<int>("SCORE");
        }

        //foreach (var item in is_On_Jumps)
        //{
        //    is_On_Jumps_list.Add(item);
        //}
        #endregion

        //we can activate level using button
        AudioManager.instance.Stop("Running");
        try
        {
            Vector2 newPosition = SaveGame.Load<Vector2>("POSITION");
            if (newPosition != Vector2.zero)
            {
                transform.position = newPosition;
            }
        }
        catch
        {
            //to continue
        }


        //StartRunning();
    }

    void Update()
    {
        if (!is_Player_Dead)
        {
            if (INFO_HOLDER.currentEnergy_num_buffer <= 0)
            {
                StartCoroutine("DEATH");
            }

            if (is_hangedLandCut)
            {
                //challenge_Controller.CutHangedLand();
                //challenge_Controller.LandSpecialLand();
                is_hangedLandCut = false;
            }

            if (is_destroyingRockLand)
            {
                //challenge_Controller.LandSpecialLand();
                is_destroyingRockLand = false;
            }

            distance_traveled += Time.deltaTime * current_speed * SPEED_OFFSET_FOR_DISTANCE_COUNTER;
            score_num_txt.text = total_score_num.ToString();

            DIAMOND_PICKED();
        }
    }

    void FixedUpdate()
    {
        if (!is_Player_Dead)
            if (is_started)
                if (is_Running)
                    if (!is_on_direction_change)
                        MOVE_LEFT();
                    else
                        MOVE_RIGHT();
    }
    #endregion

    #region MOVEMENT

    public void ACT()
    {
        Debug.Log("ACT...");
        INFO_HOLDER.Change_Energy_State(5);

        if (is_up_zone_triggered)
        {
            UP();
            is_up_zone_triggered = false;

            Debug.Log("UP...");
        }
        else if (is_run_zone_triggered)
        {
            ACCELERATE();
            is_destroyingRockLand = true; // used for specialLandTrigger 
            is_run_zone_triggered = false;
        }
        else if (is_stone_to_drag_triggered)
        {
            PUSH();
            is_stone_to_drag_triggered = false;
        }
        else if (is_crouch_zone_triggered)
        {
            CROUCH();
            is_crouch_zone_triggered = false;
        }
    }

    public void CROUCH()
    {
        StartCoroutine("Crouch_enum");
    }

    public IEnumerator Crouch_enum()
    {
        animator.animation.Play("crouch");
        this_collider.size = new Vector2(this_collider.size.x, this_collider.size.y / 2);

        yield return new WaitForSeconds(action_duration);

        animator.animation.Stop("crouch");
        this_collider.size = new Vector2(this_collider.size.x, this_collider.size.y * 2);

        foreach (var item in acceleration_UI)
        {
            item.SetActive(false);
        }
    }

    public void UP()
    {
        animator.animation.Stop("Hang"); // ----------------------------------------->
        this_rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        this_rb.gravityScale = 1f;
        this_rb.AddForce(this_transform.up * up_force, ForceMode2D.Impulse);

        //challenge_Controller.AvoidFall();

        is_Running = true;

        foreach (var item in acceleration_UI)
        {
            item.SetActive(false);
        }
    }

    private IEnumerator UP_enum()
    {


        yield return new WaitForSeconds(action_duration);


    }

    private IEnumerator ACCELERATE_enum()
    {
        if (is_Grounded && !is_Acceleration_pressed_one_time)
        {
            #region ACT

            forward_speed *= 2f;
            animator.animation.Play("accelerate"); // ----------------------------------------->


            animation_for_accelerationBtn.Play(animation_for_accelerationBtn.name);

            is_Acceleration_pressed_one_time = true;
            #endregion  

            yield return new WaitForSeconds(action_duration);

            #region DEACT
            foreach (var item in acceleration_UI)
            {
                item.SetActive(false);
            }

            forward_speed /= 2f;
            animator.animation.Stop("accelerate"); // ----------------------------------------->


            animation_for_accelerationBtn.Stop(animation_for_accelerationBtn.name);

            is_Acceleration_pressed_one_time = false;
            #endregion
        }
    }

    private void INCREMENT_CURRENT_SPEED()
    {
        current_speed /= SPEED_OFFSET_FOR_DISTANCE_COUNTER; // offset is a fraction thats why we devide
    }

    private void DECREMENT_CURRENT_SPEED()
    {
        current_speed *= SPEED_OFFSET_FOR_DISTANCE_COUNTER; // offset is a fraction thats why we multiply
    }

    public void ACCELERATE()
    {
        StartCoroutine("ACCELERATE_enum");
    }

    public void JUMP()
    {
        if (is_Grounded)
        {
            INFO_HOLDER.Change_Energy_State(1);
            StartCoroutine("JUMP_enum");
        }
    }

    private IEnumerator JUMP_enum()
    {
        AudioManager.instance.Play("Jump");
        AudioManager.instance.Play("FireTurbin");
        this_rb.gravityScale = 1f;

        fireTurbin.SetActive(true);

        if (moving_right)
            this_rb.AddForce(new Vector3(jump_force, jump_force * 1.5f, 0));
        else
            this_rb.AddForce(new Vector3(-jump_force, jump_force * 1.5f, 0));

        //play random jump animation
        animator.animation.Stop("run"); // ----------------------------------------->
        animator.animation.Play("jumpup"); // ----------------------------------------->

        //challenge_Controller.Jump();

        INCREMENT_CURRENT_SPEED();

        Time.timeScale = time_slowDown_amount;
        Shadow.SetActive(true);

        is_Jumped = true;
        is_Grounded = false;

        yield return new WaitForSeconds(0.1f);

        animator.animation.Stop("jumpup");
        animator.animation.Play("freeFly");
    }

    public void FALL_45()
    {
        if (is_Jumped)
        {
            INFO_HOLDER.Change_Energy_State(2);
            animator.animation.Play("jumpdown"); // ----------------------------------------->
            AudioManager.instance.Play("Fall45");

            if (!is_on_direction_change)
                this_rb.AddForce(new Vector3(-down45_force, -down45_force * 2f, 0));
            else
                this_rb.AddForce(new Vector3(down45_force, -down45_force * 2f, 0));

            INCREMENT_CURRENT_SPEED();
            //challenge_Controller.Land();

            is_fall_pressed = true;
        }
    }
    public void MOVE_LEFT()
    {
        moving_right = false;
        this_transform.Translate(-this_transform.right * forward_speed * Time.fixedDeltaTime);
    }
    public void MOVE_RIGHT()
    {
        moving_right = true;
        this_transform.Translate(this_transform.right * forward_speed * Time.fixedDeltaTime);
    }
    private void ChangeDirection()
    {
        if (!is_on_direction_change)
            is_on_direction_change = true;
        else
            is_on_direction_change = false;

        this_transform.Rotate(new Vector3(0, 180, 0));
    }
    #endregion

    #region GAME_ORGANIZATION

    public void DIAMOND_PICKED()
    {
        if (is_diamond_picked)
        {
            collected_diamonds_num++;
            diamonds_collected_txt.text = collected_diamonds_num.ToString();
            //challenge_Controller.CatchDiamond();
            is_diamond_picked = false;
        }
    }

    private void UpdateMap()
    {
        index_of_current_landset++;
        int temp = index_of_current_landset;

        if (temp < lands_sets.Length && temp >= 0)
        {
            //inable the next landset
            lands_sets[temp].SetActive(true);
            //disable old landset
            lands_sets[--temp].SetActive(false);
        }
    }

    public void DISPLAY_STATS()
    {
        distance__num_txt.text = ((int)distance_traveled).ToString();
    }

    public void START()
    {
        tap_to_play_UI.SetActive(false);
        is_started = true;
        AudioManager.instance.Play("Running");
        StartRunning();
    }

    public void StartRunning()
    {
        is_Running = true;
        animator.animation.Play("run"); // ----------------------------------------->
    }

    public void StopRunning()
    {
        is_Running = false;
        animator.animation.Stop("run"); // ----------------------------------------->
    }

    private void Change_Camera_Position()
    {
        cameraMovementController.offset *= -1;
    }
    public void WIN()
    {
        if (GW_UI != null)
            GW_UI.SetActive(true);

        tap_to_play_UI.SetActive(false);
        AudioManager.instance.Play("LevelUp");

        //disable the camera movement
        // Disable Player after crossing the check
        cameraMovementController.Target = null;
        Destroy(gameObject, 1f);
        SaveGame.Save("SCORE", total_score_num);
    }

    public IEnumerator DEATH()
    {
        AudioManager.instance.Stop("FireTurbin");

        // disable the camera movement
        cameraMovementController.Target = null;
        animator.animation.Play("explosion", 1);
        is_Player_Dead = true;
        fireTurbin.SetActive(false);

        foreach (var part in bodyParts)
        {
            part.GetComponent<PolygonCollider2D>().enabled = true;
            part.GetComponent<Rigidbody2D>().isKinematic = false;
        }

        drone.SetActive(false);

        yield return new WaitForSeconds(1f);

        if (GO_UI != null)
            GO_UI.SetActive(true);

        Destroy(gameObject, 1f);
        //SaveGame.Save("DISTANCE", distance_traveled);
    }

    public IEnumerator CHECKPOINT_enum(SpriteRenderer sprite)
    {
        Color temp = sprite.color;
        temp.a = 50f;
        sprite.color = temp;

        //INFO_HOLDER.position = transform.position;
        SaveGame.Save("POSITION", INFO_HOLDER.position);

        yield return new WaitForSeconds(0.5f);

        temp.a = 255f;
        sprite.color = temp;
    }

    public void CHECKPOINT(Collider2D collision)
    {
        //we take the sprite from the child, cause Checkpoint it self is just a trigger obj
        StartCoroutine("CHECKPOINT_enum", collision.gameObject.GetComponentInChildren<SpriteRenderer>());
    }

    public void SAVE_RESULTS()
    {
        INFO_HOLDER.diamonds_num = collected_diamonds_num;
        SaveGame.Save("DIAMONDS", INFO_HOLDER.diamonds_num);

        INFO_HOLDER.distance = distance_traveled;
        SaveGame.Save("DISTANCE", INFO_HOLDER.distance);
    }

    public IEnumerator Activate_Reward_text(int score)
    {
        reward_txt.SetActive(true);
        //reward_txt.transform.position = this_transform.position;
        _reward_txt.text = score.ToString();
        total_score_num += score;

        yield return new WaitForSeconds(1f);

        reward_txt.SetActive(false);
    }
    #endregion

    public IEnumerator PUSH_enum()
    {
        if (is_Grounded && !is_Acceleration_pressed_one_time)
        {
            #region ACT

            forward_speed *= 2f;
            animator.animation.Stop("push");
            animator.animation.Play("pushcircle"); // ----------------------------------------->

            animation_for_accelerationBtn.Play(animation_for_accelerationBtn.name);

            is_Acceleration_pressed_one_time = true;
            #endregion  

            yield return new WaitForSeconds(action_duration);

            #region DEACT
            foreach (var item in acceleration_UI)
            {
                item.SetActive(false);
            }

            forward_speed /= 2f;
            animator.animation.Stop("pushcircle"); // ----------------------------------------->


            animation_for_accelerationBtn.Stop(animation_for_accelerationBtn.name);

            is_Acceleration_pressed_one_time = false;
            #endregion
        }
    }

    public void PUSH()
    {
        StartCoroutine("PUSH_enum");
    }

    private IEnumerator LAND_enum()
    {
        animator.animation.Stop("freeFly"); // ----------------------------------------->
        animator.animation.Stop("jumpup"); // ----------------------------------------->
        animator.animation.Stop("jumpdown"); // ----------------------------------------->
        INCREMENT_CURRENT_SPEED();

        #region Activate Reward text
        if (!is_fall_pressed && is_Jumped)
        {
            StartCoroutine("Activate_Reward_text", REWARD_FOR_LONG_JUMP);
            GAME_CONTROLLER.SHORT_VIBRATION();
            //challenge_Controller.LongJump();
            animator.animation.Play("stronglanding"); // ----------------------------------------->
            is_strongLanding = true;
        }
        else
            StartCoroutine("Activate_Reward_text", REWARD_FOR_NICE_FALL);
        #endregion

        #region Animation
        if (!is_strongLanding)
        {
            //animator.animation.Play("freefall"); // ----------------------------------------->
            //yield return new WaitForSeconds(0.3f);
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            is_strongLanding = false;
        }

        animator.animation.Play("run"); // ----------------------------------------->
        #endregion
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (is_started)
        {
            if (collision.collider.tag == "Ground")
            {
                if (!is_Player_Dead)
                {
                    fireTurbin.SetActive(false);
                    this_rb.gravityScale = new_gravity_scale;

                    DECREMENT_CURRENT_SPEED();
                    DECREMENT_CURRENT_SPEED();

                    AudioManager.instance.Stop("FireTurbin");
                    AudioManager.instance.Play("Land");
                    AudioManager.instance.Play("Running");

                    if (acceleration_btn != null)
                        acceleration_btn.SetActive(true);

                    //animator.SetBool("is_LongJump", false);

                    Shadow.SetActive(false);

                    StartCoroutine("LAND_enum");

                    #region Constrains FreezeAll and bools 
                    this_rb.constraints = RigidbodyConstraints2D.FreezeAll;
                    is_fall_pressed = false;
                    is_up_zone_triggered = false;
                    is_Jumped = false;
                    is_Grounded = true;
                    #endregion

                    #region Time and Constrains
                    Time.timeScale = 1f;
                    this_rb.constraints = RigidbodyConstraints2D.None;
                    this_rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                    #endregion
                }
            }
            if (collision.collider.tag == "WALL")
            {
                if (!is_Player_Dead)
                {
                    ChangeDirection();
                    UpdateMap();
                    Change_Camera_Position();
                }
            }

            if (collision.collider.tag == "ROCK")
            {
                StartCoroutine("DEATH");
            }
            if (collision.collider.tag == "StoneToDrag")
            {
                if (!is_Player_Dead)
                {
                    foreach (var item in acceleration_UI)
                    {
                        item.SetActive(true);
                    }
                    animator.animation.Stop("run");
                    animator.animation.Play("push");

                    is_stone_to_drag_triggered = true;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (is_started)
            if (collision.collider.tag == "Ground")
            {
                animator.animation.Stop("run"); // ----------------------------------------->
                animator.animation.Play("freeFly");

                AudioManager.instance.Stop("Running");

                is_Jumped = true;
                is_Grounded = false;
            }
    }

    private void DROP_THE_ROCK(Rigidbody2D rb)
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.AddForce(rb.transform.up * 1f, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "DEAD_ZONE")
        {
            if (!is_Player_Dead)
                StartCoroutine("DEATH");
        }
        if (collision.tag == "WIN_ZONE")
        {
            if (!is_Player_Dead)
                WIN();
        }
        if (collision.tag == "RUN_TRIGGER")
        {
            if (!is_Player_Dead)
            {
                foreach (var item in acceleration_UI)
                {
                    item.SetActive(true);
                }

                is_run_zone_triggered = true;

                foreach (var rock in collision.gameObject.GetComponentInParent<RockFalling_Land>().rocks)
                {
                    DROP_THE_ROCK(rock);
                }
            }
        }
        if (collision.tag == "CROUCH_TRIGGER")
        {
            if (!is_Player_Dead)
            {
                foreach (var item in acceleration_UI)
                {
                    item.SetActive(true);
                }
                is_crouch_zone_triggered = true;
            }
        }
        if (collision.tag == "SAVE_CLIMB")
        {
            //foreach (var item in acceleration_UI)
            //{
            //    item.SetActive(true);
            //}
            //is_up_zone_triggered = true;
            //upCollidder_buffer = collision;

            //is_Running = false;
            //this_transform.position = upCollidder_buffer.transform.position;
            //this_rb.constraints = RigidbodyConstraints2D.FreezeAll;
            //this_rb.gravityScale = 0f;
            //animator.SetBool("is_Hanged", true);
        }
        if (collision.tag == "CHECKPOINT")
        {
            if (!is_Player_Dead)
            {
                StartCoroutine("Activate_Reward_text", REWARD_FOR_CHECKPOINT);
                CHECKPOINT(collision);
            }
        }
        if (collision.tag == "SHADOW")
        {
            AudioManager.instance.Play("Shadow");
            //challenge_Controller.LandOnShadow();
        }
        if (collision.tag == "EASTER_EGG")
        {
            //challenge_Controller.FindEasterEgg();
        }
        if (collision.tag == "ROLLING_TRIGGER")
        {
            collision.gameObject.GetComponent<RollingBall_Controller>().ActivateObstacle();
        }
        if (collision.tag == "BALL_TRIGGER")
        {
            collision.gameObject.GetComponent<Balls_System>().Activate();
        }
        if (collision.tag == "Energy")
        {
            if (!is_Player_Dead)
            {
                INFO_HOLDER.Change_Energy_State(-5);
                AudioManager.instance.Play("Energy");
                Destroy(collision.gameObject);
            }
        }
    }

    public IEnumerator FreezeRD2D_for_some_time()
    {
        this_rb.constraints = RigidbodyConstraints2D.FreezeAll;

        yield return new WaitForSeconds(2f);

        this_rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}