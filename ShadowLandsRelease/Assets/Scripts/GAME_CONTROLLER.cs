/*
*	TickLuck
*	All rights reserved
*/
using BayatGames.SaveGameFree;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GAME_CONTROLLER : MonoBehaviour
{
    #region Variables
    public static GAME_CONTROLLER instance;
    [SerializeField] private GameObject GW_UI;
    [SerializeField] private GameObject HELLO_UI;
    [SerializeField] private GameObject pause_menu_UI;
    [SerializeField] private GameObject Settings_Menu;
    [SerializeField] private GameObject Levels_Menu;
    [SerializeField] private GameObject Shop_Menu;
    [SerializeField] private Text score_txt;
    [SerializeField] private Text GW_Menu_score_txt;
    [SerializeField] private long short_vibration_duration;
    [SerializeField] private long long_vibration_duration;
    [SerializeField] private long nano_vibratiuon_duration;

    public static long short_vibration_duration_holder;
    public static long long_vibration_duration_holder;
    public static long nano_vibration_duration_holder;

    public static bool is_EarthZone;
    public static bool is_RockZone;
    public static bool is_SandZone;
    public static bool is_VolcanoZone;

    private int score;
    private float multiplierValue;
    private bool isFirstLaunch = true;

    public static bool is_Vibration_On;
    public static bool is_Sound_On;
    public static bool is_DarkMode_On;

    private readonly int FIRST = 1;
    private readonly int SECOND = 2;
    private readonly int THIRD = 3;
    private int this_scene_index;
    private Vector2 initial_pos_settings;
    private Vector2 initial_pos_shop;

    private SpriteRenderer BG;
    private int location_index_buffer;
    [SerializeField] GameObject[] locationsBGs;
    private AudioManager audioManager;
    #endregion

    #region UnityMethods

    public void Start()
    {
        Time.timeScale = 1f;
        try
        {
            pause_menu_UI = GameObject.FindGameObjectWithTag("PAUSE_MENU");
            pause_menu_UI.SetActive(false);
        }
        catch
        {
            //to continue
        }

        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        try
        {
            BG = GameObject.FindGameObjectWithTag("BG").GetComponent<SpriteRenderer>();
        }
        catch
        {
            //to continue
        }

        if (Levels_Menu != null)
        {
            Levels_Menu.SetActive(false);
        }

        if (Settings_Menu != null)
        {
            initial_pos_settings = Settings_Menu.transform.position;
            Settings_Menu.SetActive(false);
        }

        if (Shop_Menu != null)
        {
            initial_pos_shop = Shop_Menu.transform.position;
            Shop_Menu.SetActive(false);
        }

        is_DarkMode_On = false;
        is_Sound_On = true;
        is_Vibration_On = true;

        short_vibration_duration_holder = short_vibration_duration;
        long_vibration_duration_holder = long_vibration_duration;
        nano_vibration_duration_holder = nano_vibratiuon_duration;

        this_scene_index = SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {

    }
    #endregion

    public void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void UI_On_Off_Switch(GameObject UI)
    {
        if (UI.activeInHierarchy == true)
        {
            UI.GetComponent<MenuDisabler>().Close();
        }
        else
        {
            UI.SetActive(true);
        }
    }

    public void SET_BG_FOR_MAIN_MENU(int location_index)
    {
        if (location_index >= locationsBGs.Length || location_index < 0)
            return;

        //locationsBGs[location_index].transform.position = BG.transform.position;
        locationsBGs[location_index].SetActive(true);

        for (int i = 0; i < locationsBGs.Length; i++)
        {
            if (location_index == i) continue;
            locationsBGs[i].SetActive(false);
        }
        //locationsBGs[location_index_buffer].SetActive(false);
        //location_index_buffer = location_index;
    }

    public void OPEN_EARTH_ZONE()
    {
        is_RockZone = false;
        is_SandZone = false;
        is_VolcanoZone = false;

        is_EarthZone = true;
    }

    public void OPEN_ROCK_ZONE()
    {
        is_EarthZone = false;
        is_SandZone = false;
        is_VolcanoZone = false;

        is_RockZone = true;
    }

    public void OPEN_SAND_ZONE()
    {
        is_RockZone = false;
        is_EarthZone = false;
        is_VolcanoZone = false;

        is_SandZone = true;
    }

    public void OPEN_VOLCANO_ZONE()
    {
        is_RockZone = false;
        is_SandZone = false;
        is_EarthZone = false;

        is_VolcanoZone = true;
    }

    #region SCORING
    internal void UpdateMultiplier(float newValue)
    {
        if (multiplierValue >= newValue)
            return;
        multiplierValue = newValue;
        score = (int)(score * multiplierValue);
        score_txt.text = score.ToString();
    }

    public void DisplayScore()
    {
        GW_Menu_score_txt.text = score.ToString();
    }

    public void UpdateScore(int value)
    {
        score += value;
        if (score < 0)
            score = 0;
        score_txt.text = score.ToString();
    }
    #endregion

    #region SETTINGS

    #region ON/OFF
    public void VIBRATION_ON(GameObject line)
    {
        is_Vibration_On = true;
        Vibration.is_on = true;
        line.SetActive(false);
    }
    public void SOUND_ON(GameObject line)
    {
        is_Sound_On = true;
        audioManager.SoundOn();
        line.SetActive(false);
    }
    public void DARK_MODE_ON(GameObject line)
    {
        is_DarkMode_On = true;
        line.SetActive(false);
    }
    public void VIBRATION_OFF(GameObject line)
    {
        is_Vibration_On = false;
        Vibration.is_on = false;
        line.SetActive(true);
    }
    public void SOUND_OFF(GameObject line)
    {
        is_Sound_On = false;
        audioManager.Silence();
        line.SetActive(true);
    }
    public void DARK_MODE_OFF(GameObject line)
    {
        is_DarkMode_On = false;
        line.SetActive(true);
    }
    #endregion

    public void Vibration_Switch(GameObject line)
    {
        if (is_Vibration_On)
        {
            VIBRATION_OFF(line);
        }
        else
        {
            VIBRATION_ON(line);
        }
    }

    public void Sound_Switch(GameObject line)
    {
        if (is_Sound_On)
        {
            SOUND_OFF(line);
        }
        else
        {
            SOUND_ON(line);
        }
    }

    public void DarkMode_Switch(GameObject line)
    {
        if (is_DarkMode_On)
        {
            DARK_MODE_OFF(line);
        }
        else
        {
            DARK_MODE_ON(line);
        }

        ColorDestributor.is_color_changed = true;
    }
    #endregion

    #region VIBRATIONS
    public static void NANO_VIBRATION()
    {
        Vibration.Vibrate(nano_vibration_duration_holder);
    }
    public static void SHORT_VIBRATION()
    {
        Vibration.Vibrate(short_vibration_duration_holder);
    }
    public static void LONG_VIBRATION()
    {
        Vibration.Vibrate(long_vibration_duration_holder);
    }
    #endregion

    public void START()
    {
        if (HELLO_UI != null)
            HELLO_UI.SetActive(false);
        LOAD_SECOND_SCENE();
    }

    #region OPENING/CLOSING MENUES
    public void OPEN_PAUSE_MENU()
    {
        pause_menu_UI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OPEN_SHOP()
    {
        Shop_Menu.SetActive(true);
        Shop_Menu.transform.position = initial_pos_shop;
    }

    public void OPEN_SETTINGS()
    {
        Settings_Menu.SetActive(true);
        Settings_Menu.transform.position = initial_pos_settings;
        //play open anim
    }

    public void CLOSE_PAUSE_MENU()
    {
        pause_menu_UI.SetActive(false);
        Time.timeScale = 1f;
    }
    #endregion

    public void LOAD_SECOND_SCENE()
    {
        SceneManager.LoadScene(SECOND);
    }

    public void LOAD_FIRST_SCENE()
    {
        if (PLAYER_MOVEMENT_SYSTEM.total_score_num != 0)
            SaveGame.Save("SCORE", PLAYER_MOVEMENT_SYSTEM.total_score_num);

        SceneManager.LoadScene(FIRST);
    }

    public void LOAD_LEVEL(int index)
    {
        if (SceneManager.sceneCountInBuildSettings - 1 < index)
        {
            LOAD_FIRST_SCENE();
            return;
        }

        SceneManager.LoadScene(index);
    }

    public void LOAD_NEXT_LEVEL()
    {
        if (SceneManager.sceneCountInBuildSettings - 1 < this_scene_index + 1)
        {
            LOAD_FIRST_SCENE();
            return;
        }

        if (SaveGame.Load<int>("LEVEL") > 0)
        {
            SAVE_GAME_HOLDER.index_of_opened_level = SceneManager.GetActiveScene().buildIndex + 1;
            SaveGame.Save("LEVEL", SAVE_GAME_HOLDER.index_of_opened_level);
        }


        SceneManager.LoadScene(this_scene_index + 1);
    }

    public void RESTART()
    {
        SceneManager.LoadScene(this_scene_index);
    }
    public void EXIT()
    {
        Application.Quit();
    }
}
