using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject mainMenu;

    public GameObject VictoryMenu;
    public string gameSceneName;
    public string mainMenuSceneName;
    public string victorySceneName;

    private bool isGamePaused;
    
    // Count down system
    public float countdownTime = 60f;
    public Text countdownDisplay;
    private void Start()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        VictoryMenu.SetActive(false);
        isGamePaused = false;

        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == mainMenuSceneName)
        {
            mainMenu.SetActive(true);
        }
        else
        {
            mainMenu.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void LeaveGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        if (!isGamePaused)
        {
            SetGamePaused(true);
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void ResumeGame()
    {
        if (isGamePaused)
        {
            SetGamePaused(false);
        }
    }

    private void SetGamePaused(bool paused)
    {
        Time.timeScale = paused ? 0f : 1f;
        pauseMenu.SetActive(paused);
        isGamePaused = paused;
    }

    public void Victory() 
    {   
        SceneManager.LoadScene(victorySceneName);
        VictoryMenu.SetActive(true);
    }

    //Count down system and did have funtion to stat the contdown yet.
    IEnumerator CountdownToStart()
    {
        while (countdownTime > 0)
        {
            countdownDisplay.text = "countdown: " + countdownTime.ToString();

            yield return new WaitForSecondsRealtime(1f);

            countdownTime--;
        }

        countdownDisplay.text = "countdown finish!";

        // do something after countdown finish.
    }
}
