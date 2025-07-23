using UnityEngine;
using System;

public class Flipfinity_GameManager : MonoBehaviour
{
    private const string HIGHSCORE_KEY = "HighScore_Flipfinity";

    public static Flipfinity_GameManager instance;

    public static event Action OnGameInitialized;
    public static event Action OnGameOver;
    public static event Action OnVisualBackgroundChanged;
    public static event Action<int> OnScoreUpdated;
    public static event Action<int> OnHighScoreUpdated;
    public static event Action<int, int> OnGameOverUIText;

    [SerializeField] private Flipfinity_Player flipfinity_Player;
    [SerializeField] private Flipfinity_SpwanManager flipfinity_SpwanManager;

    [SerializeField] private ParticleSystem coinCollectEffect;
    [SerializeField] private ParticleSystem playerDiedEffect;

    private bool isGameStart;

    private int score = 0;
    private int highScore = 0;
    private int coinCount = 0;

    public bool IsGameStart => isGameStart;
    public int CoinCount => coinCount;


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
        Flipfinity_Player.OnPlayerDied += GameOver;
        Flipfinity_Player.OnCoinCollected += AddScore;
        Flipfinity_Player.onCoinEffect += PlayCoinCollectEffect;
        Flipfinity_Player.onScoreEffect += PlayPlayerDiedEffect;

        Flipfinity_UIManager.OnPTapToPlayButtonClicked += GameStarted;
        Flipfinity_UIManager.OnPauseButtonClicked += GamePaused;
        Flipfinity_UIManager.OnResumeButtonClicked += GameResumed;
        Flipfinity_UIManager.OnRestartButtonClicked += GameRestart;
    }

    void OnDisable()
    {
        Flipfinity_Player.OnPlayerDied -= GameOver;
        Flipfinity_Player.OnCoinCollected -= AddScore;
        Flipfinity_Player.onCoinEffect -= PlayCoinCollectEffect;
        Flipfinity_Player.onScoreEffect -= PlayPlayerDiedEffect;

        Flipfinity_UIManager.OnPTapToPlayButtonClicked -= GameStarted;
        Flipfinity_UIManager.OnPauseButtonClicked -= GamePaused;
        Flipfinity_UIManager.OnResumeButtonClicked -= GameResumed;
        Flipfinity_UIManager.OnRestartButtonClicked -= GameRestart;
    }

    void Start()
    {
        InitializedGame();
    }

    void InitializedGame()
    {
        score = 0;
        coinCount = 0;
        highScore = PlayerPrefs.GetInt(HIGHSCORE_KEY, 0);
        isGameStart = false;
        flipfinity_Player.enabled = false;
        flipfinity_SpwanManager.enabled = false;
        OnScoreUpdated?.Invoke(score);
        OnHighScoreUpdated?.Invoke(highScore);
        OnGameInitialized?.Invoke();
    }

    private void GameStarted()
    {
        isGameStart = true;
        flipfinity_Player.enabled = true;
        flipfinity_SpwanManager.enabled = true;
    }

    private void GamePaused()
    {
        isGameStart = false;
        flipfinity_Player.enabled = false;
        flipfinity_SpwanManager.enabled = false;

    }

    private void GameResumed()
    {
        isGameStart = true;
        flipfinity_Player.enabled = true;
        flipfinity_SpwanManager.enabled = true;
    }


    private void GameOver()
    {
        isGameStart = false;
        flipfinity_Player.enabled = false;
        flipfinity_SpwanManager.enabled = false;
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt(HIGHSCORE_KEY, highScore);
            PlayerPrefs.Save();
            OnHighScoreUpdated?.Invoke(highScore);
        }
        OnGameOverUIText?.Invoke(score, highScore);
        OnGameOver?.Invoke();
    }

    private void AddScore()
    {
        coinCount++;
        score += coinCount + 1;
        OnScoreUpdated?.Invoke(score);
        if (coinCount % 2 == 0)
        {
            OnVisualBackgroundChanged?.Invoke();
        }
    }

    private void GameRestart()
    {
        SceneLoader.ReloadCurrentScene();
        Debug.Log("Game Restart");
    }

    private void PlayCoinCollectEffect(Vector3 position)
    {
        if (coinCollectEffect == null) return;

        coinCollectEffect.transform.position = position;
        coinCollectEffect.Play();
    }
    
    private void PlayPlayerDiedEffect(Vector3 position)
    {
        if (playerDiedEffect == null) return;

        playerDiedEffect.transform.position = position;
        playerDiedEffect.Play();
    }
    
    
}