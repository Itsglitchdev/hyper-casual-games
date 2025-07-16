using UnityEngine;
using System.Collections;
using System;
using UnityEngine.EventSystems;
using System.Threading;

public class RingO_GameManagerr : MonoBehaviour {

    private const string HIGHSCORE_KEY = "HighScore";

    public static RingO_GameManagerr instance;

    public event Action<int> OnCoinCollected;
    public event Action<int> OnHighScoreChanged;
    public event Action OnBackgroundChange;
    public event Action OnGameInitialized;
    public event Action OnGameOver;
    public event Action<int, int> OnGameOverUIText;

    [SerializeField] private RingO_UIManager ringO_UIManager;
    [SerializeField] private RingO_CoinManager ringO_CoinManager;
    [SerializeField] private RingO_ObstaclesManager ringO_ObstaclesManager;

    public bool isGameStart = false;
    private int score = 0;
    private int coinCounter = 0;
    private int highScore = 0;
    private int consecutiveCoinCount = 0;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        ringO_UIManager.OnPTapToPlayButtonClicked += GameStarted;
        ringO_UIManager.OnPauseButtonClicked += GamePaused;
        ringO_UIManager.OnResumeButtonClicked += GameResumed;
        ringO_UIManager.OnRestartButtonClicked += GameRestart;
    }

    void OnDisable()
    {
        ringO_UIManager.OnPTapToPlayButtonClicked -= GameStarted;
        ringO_UIManager.OnPauseButtonClicked -= GamePaused;
        ringO_UIManager.OnResumeButtonClicked -= GameResumed;
        ringO_UIManager.OnRestartButtonClicked -= GameRestart;
    }

    void Start()
    {
        InitializedGame();
    }

    void InitializedGame()
    {
        score = 0;
        coinCounter = 0;
        consecutiveCoinCount = 0;
        highScore = PlayerPrefs.GetInt(HIGHSCORE_KEY, 0);
        isGameStart = false;
        ringO_CoinManager.enabled = false;
        ringO_ObstaclesManager.enabled = false;
        OnGameInitialized?.Invoke();
        OnHighScoreChanged?.Invoke(highScore);
    }

    private void GameStarted()
    {
        isGameStart = true;
        ringO_CoinManager.enabled = true;
        ringO_ObstaclesManager.enabled = true;
    }

    private void GamePaused() => isGameStart = false;

    private void GameResumed() => isGameStart = true;

    private void GameRestart()
    {
        SceneLoader.ReloadCurrentScene();
        Debug.Log("Game Restart");
    }

    public void GameOver()
    {
        isGameStart = false;
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt(HIGHSCORE_KEY, highScore);
            PlayerPrefs.Save();
            OnHighScoreChanged?.Invoke(highScore);
        }
        consecutiveCoinCount = 0;
        OnGameOverUIText?.Invoke(score, highScore);
        OnGameOver?.Invoke();
    }

    public void AddScore()
    {
        consecutiveCoinCount++;
        coinCounter++;
        int scoreToAdd = consecutiveCoinCount;
        score += scoreToAdd;
        if (coinCounter % 5 == 0)
        { 
            OnBackgroundChange?.Invoke();
        }
        OnCoinCollected?.Invoke(score);
    }

}