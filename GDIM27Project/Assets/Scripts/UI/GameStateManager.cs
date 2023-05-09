using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject mainMenu;
    public string gameSceneName = "GameScene";
    public string mainMenuSceneName = "MainMenuScene";

    private bool isGamePaused;

    private void Start()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        isGamePaused = false;
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
}
