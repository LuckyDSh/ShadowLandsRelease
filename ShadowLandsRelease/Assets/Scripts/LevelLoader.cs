/*
*	TickLuck
*	All rights reserved
*/
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    #region Variables
    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;
    private bool is_gameStarted = false;
    [SerializeField] private float timeFlow = 10f;
    private int index_to_load;
    [SerializeField] private int seconds = 2;
    #endregion

    #region UnityMethods 
    private void Start()
    {
        //is_gameStarted = false; // old way to load the level
        index_to_load = SceneManager.GetActiveScene().buildIndex + 1;
    }

    private void Update()
    {

        Load_new_level_with_progress_bar(index_to_load);

#if False
        SetGameToBeStarted();

        if (is_gameStarted)
        {
            LoadLevel(1);
            is_gameStarted = false;
        }
#endif
    }

    #endregion

    public void Load_new_level_with_progress_bar(int buildIndex)
    {
        slider.value += Time.deltaTime * timeFlow;
        progressText.text = (int)(slider.value * 100f) + "%";

        if (slider.value == slider.maxValue)
            SceneManager.LoadScene(buildIndex);
    }

    public void SetGameToBeStarted()
    {
        Debug.Log("Btn is Pressed");
        is_gameStarted = true;
    }

    public void LoadLevel(int sceneIndex)
    {
        Debug.Log("Loading started ... ");

        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    public IEnumerator LoadAsynchronously(int sceneIndex)
    {
        Debug.Log("Coroutine start ... ");

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        if (loadingScreen != null)
            loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            // making slider go 0-1%
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;
            progressText.text = progress * 100f + "%";
        }

        yield return null;
    }

    public void LoadNextLevel(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
}
