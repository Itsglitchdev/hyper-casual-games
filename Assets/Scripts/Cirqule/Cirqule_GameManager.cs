using UnityEngine;
using System;

public class Cirqule_GameManager : MonoBehaviour
{
    private const string HIGHSCORE_KEY = "HighScore_Cirqule";

    public static Cirqule_GameManager Instance;

    public static event Action OnGameInitialized;
    public static event Action<int> OnScoreUpdated;
    public static event Action<int> OnHighScoreUpdated;
    public static event Action<int, int> OnGameOverUIText;
    public static event Action OnVisualBackgroundChanged;

    [Header("ObstacleHolder Reference")]
    [SerializeField] private GameObject obstacleHolder;
    [Header("Particles Reference")]
    [SerializeField] private ParticleSystem playerDiedEffect;

    private bool isGameStart = false;
    private int score = 0;
    private int highScore = 0;
    private int collectablesCount = 0;

    public bool IsGameStart { get => isGameStart; set => isGameStart = value; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        InitializeGame();
    }

    void OnEnable()
    {
        Cirqule_Player.OnPlayerDied += GameOver;
        Cirqule_Player.OnPlayerDiedEffect += PlayerDiedEffect;
        Cirqule_Player.OnCollectableColleted += AddScore;

        Cirqule_UIManager.OnPTapToPlayButtonClicked += GameStarted;
        Cirqule_UIManager.OnPauseButtonClicked += GamePaused;
        Cirqule_UIManager.OnResumeButtonClicked += GameResumed;
        Cirqule_UIManager.OnRestartButtonClicked += GameRestart;
    }

    void OnDisable()
    {
        Cirqule_Player.OnPlayerDied -= GameOver;
        Cirqule_Player.OnCollectableColleted -= AddScore;
        Cirqule_Player.OnPlayerDiedEffect -= PlayerDiedEffect;
        Cirqule_UIManager.OnPTapToPlayButtonClicked -= GameStarted;
        Cirqule_UIManager.OnPauseButtonClicked -= GamePaused;
        Cirqule_UIManager.OnResumeButtonClicked -= GameResumed;
        Cirqule_UIManager.OnRestartButtonClicked -= GameRestart;
    }

    void InitializeGame()
    {
        isGameStart = false;
        score = 0;
        highScore = PlayerPrefs.GetInt(HIGHSCORE_KEY, 0);
        collectablesCount = 0;
        obstacleHolder.SetActive(false);
        OnScoreUpdated?.Invoke(score);
        OnHighScoreUpdated?.Invoke(highScore);
        OnGameInitialized?.Invoke();
    }

    void GameStarted()
    {
        isGameStart = true;
        obstacleHolder.SetActive(true);
    }

    void GamePaused() => isGameStart = false;

    void GameResumed() => isGameStart = true;

    void GameOver()
    {
        isGameStart = false;
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt(HIGHSCORE_KEY, highScore);
            PlayerPrefs.Save();
            OnHighScoreUpdated?.Invoke(highScore);
        }
        OnGameOverUIText?.Invoke(score, highScore);
    }

    void GameRestart()
    {
        SceneLoader.ReloadCurrentScene();
        Debug.Log("Game Restart");
    }

    void AddScore()
    {
        collectablesCount++;
        score += collectablesCount + 1;
        OnScoreUpdated?.Invoke(score);
        if (collectablesCount % 3 == 0)
        {
            OnVisualBackgroundChanged?.Invoke();
        }
    }

    private void PlayerDiedEffect(Vector3 position)
    {
        if (playerDiedEffect != null) { 
            playerDiedEffect.transform.position = position;
            playerDiedEffect.Play();
        }   
    }


}