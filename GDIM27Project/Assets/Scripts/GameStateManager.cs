using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour
{
    public enum GameState
    {
        MainMenu,
        InGame,
        Paused,
        GameOver,
        // Add any other states you need
    }

    public static GameStateManager Instance;
    public GameState currentGameState;


    // Count down system
    public float countdownTime = 60f;
    public Text countdownDisplay;

    public GameObject countdownDisplayObject;

    private Coroutine countdownCoroutine;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        // Initial state
        ChangeGameState(GameState.MainMenu);
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        countdownTime = 60f; // reset countdown time
    }

    public void ChangeGameState(GameState newGameState)
    {
        currentGameState = newGameState;

        switch (currentGameState)
        {
            case GameState.MainMenu:
                // Operations needed in the MainMenu state
                if (countdownCoroutine != null)
                {
                    StopCoroutine(countdownCoroutine);
                    countdownCoroutine = null;
                }
                countdownDisplayObject.SetActive(false);
                break;
            case GameState.InGame:
                // Operations needed in the InGame state
                Time.timeScale = 1;

                if (countdownCoroutine == null)
                {
                    countdownCoroutine = StartCoroutine(CountdownToStart());
                }
                countdownDisplayObject.SetActive(true);
                break;
            case GameState.Paused:
                // Operations needed in the Paused state
                Time.timeScale = 0;

                if (countdownCoroutine != null)
                {
                    StopCoroutine(countdownCoroutine);
                    countdownCoroutine = null;
                }
                countdownDisplayObject.SetActive(false);
                break;
            case GameState.GameOver:
                // Operations needed in the GameOver state
                if (countdownCoroutine != null)
                {
                    StopCoroutine(countdownCoroutine);
                    countdownCoroutine = null;
                }
                countdownDisplayObject.SetActive(false);
                break;
                // Handle other states
        }
    }

    //Count down system and did have funtion to stat the contdown yet.
    IEnumerator CountdownToStart()
    {
        while (countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();

            yield return new WaitForSecondsRealtime(1f);

            countdownTime--;
        }

        //countdownDisplay.text = "countdown finish!";
        ChangeGameState(GameState.GameOver);

        // do something after countdown finish.
    }
}
