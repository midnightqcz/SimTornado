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

   
}
