using UnityEngine;
using System.Collections;
using System;
using UnityEngine.EventSystems;
using System.Threading;

public class RingO_GameManagerr : MonoBehaviour
{

    private const string HIGHSCORE_KEY = "HighScore";

    public static RingO_GameManagerr instance;

    public event Action<int> OnCoinCollected;
    public event Action<int> OnHighScoreChanged;
    public event Action OnBackgroundChange;
    public event Action OnGameInitialized;
    public event Action OnBonusAllObstaclesLanesChange;
    public event Action OnVisualOneObstacleLaneChange;
    public event Action OnGameOver;
    public event Action<int, int> OnGameOverUIText;

    [SerializeField] private RingO_UIManager ringO_UIManager;
    [SerializeField] private RingO_SpwanManager ringO_SpwanManager;
    [SerializeField] private RingO_Player ringO_Player;

    [SerializeField] private ParticleSystem coinCollectParticle;
    [SerializeField] private ParticleSystem playerDiedParticle;

    public bool isGameStart = false;
    private int score = 0;
    private int bonusCount = 0;
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
        bonusCount = 1;
        coinCounter = 0;
        consecutiveCoinCount = 1;
        highScore = PlayerPrefs.GetInt(HIGHSCORE_KEY, 0);
        isGameStart = false;
        ringO_Player.enabled = false;
        ringO_SpwanManager.enabled = false;
        OnGameInitialized?.Invoke();
        OnHighScoreChanged?.Invoke(highScore);
    }

    private void GameStarted()
    {
        isGameStart = true;
        ringO_Player.enabled = true;
        ringO_SpwanManager.enabled = true;
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
        coinCounter++;
        if (consecutiveCoinCount == bonusCount)
        {
            score += bonusCount;
            consecutiveCoinCount = 0;
            bonusCount++;
            consecutiveCoinCount++;
            if (bonusCount > 4)
            {
                OnBonusAllObstaclesLanesChange?.Invoke();
            }
        }
        else
        {
            score++;
            consecutiveCoinCount++;
        }

        if (coinCounter % 4 == 0)
        {
            OnVisualOneObstacleLaneChange?.Invoke();
            OnBackgroundChange?.Invoke();
        }
        OnCoinCollected?.Invoke(score);
    }

    public void PlayCoinCollectEffect(Vector3 position)
    {
        if (coinCollectParticle == null) return;

        coinCollectParticle.transform.position = position;
        coinCollectParticle.Play();
    }
    
    public void PlayPlayerDiedEffect(Vector3 position)
    {
        if (playerDiedParticle == null) return;

        playerDiedParticle.transform.position = position;
        playerDiedParticle.Play();
    }

}