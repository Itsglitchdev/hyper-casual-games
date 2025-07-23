using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Flipfinity_UIManager : MonoBehaviour
{

    public static event Action OnPTapToPlayButtonClicked;
    public static event Action OnPauseButtonClicked;
    public static event Action OnResumeButtonClicked;
    public static event Action OnRestartButtonClicked;

    [Header("Buttons")]
    [SerializeField] private Button tapToPlayButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button homeButton;

    [Header("Panels")]
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject inGameScorePanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject resumePanel;
    [SerializeField] private GameObject restartPanel;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI gameOverScoreText;
    [SerializeField] private TextMeshProUGUI gameOverHighScoreText;


    void Start()
    {
        ButtonEventListener();
    }

    void OnEnable()
    {
        Flipfinity_GameManager.OnGameInitialized += InitializeGameUI;
        Flipfinity_GameManager.OnScoreUpdated += UpdateScoreUI;
        Flipfinity_GameManager.OnHighScoreUpdated += UpdateHighScoreUI;
        Flipfinity_GameManager.OnGameOverUIText += GameOverUIText;
        Flipfinity_GameManager.OnGameOver += GameOverUI;
    }

    void OnDisable()
    {
        Flipfinity_GameManager.OnGameInitialized -= InitializeGameUI;
        Flipfinity_GameManager.OnScoreUpdated -= UpdateScoreUI;
        Flipfinity_GameManager.OnHighScoreUpdated -= UpdateHighScoreUI;
        Flipfinity_GameManager.OnGameOverUIText -= GameOverUIText;
        Flipfinity_GameManager.OnGameOver -= GameOverUI;
    }


    void InitializeGameUI()
    {
        startPanel.SetActive(true);
        inGameScorePanel.SetActive(false);
        pausePanel.SetActive(false);
        resumePanel.SetActive(false);
        restartPanel.SetActive(false);
    }

    void ButtonEventListener()
    {
        tapToPlayButton.onClick.AddListener(StartGamePlayUI);
        pauseButton.onClick.AddListener(OnPauseButtonClick);
        resumeButton.onClick.AddListener(OnResumeButtonClick);
        restartButton.onClick.AddListener(OnRestartButtonClick);
        homeButton.onClick.AddListener(OnHomeButtonClick);
    }

    void StartGamePlayUI()
    {
        OnPTapToPlayButtonClicked?.Invoke();
        inGameScorePanel.SetActive(true);
        pausePanel.SetActive(true);
        startPanel.SetActive(false);
        resumePanel.SetActive(false);
        restartPanel.SetActive(false);
    }

    void GameOverUI()
    {
        inGameScorePanel.SetActive(false);
        pausePanel.SetActive(false);
        restartPanel.SetActive(true);
    }

    void OnPauseButtonClick()
    {
        OnPauseButtonClicked?.Invoke();
        resumePanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    void OnResumeButtonClick()
    {
        OnResumeButtonClicked?.Invoke();
        pausePanel.SetActive(true);
        resumePanel.SetActive(false);
    }

    void OnHomeButtonClick()
    {
        SceneLoader.Load(Scene.Loading, Scene.GameHub);
    }

    void OnRestartButtonClick()
    {
        OnRestartButtonClicked?.Invoke();
    }

    void UpdateScoreUI(int score)
    {
        scoreText.text = "Score: " + score.ToString();
    }

    void UpdateHighScoreUI(int highScore)
    {
        highScoreText.text = "High Score: " + highScore.ToString();
    }

    void GameOverUIText(int score, int highScore)
    {
        gameOverScoreText.text = "Score: " + score.ToString();
        gameOverHighScoreText.text = "High Score: " + highScore.ToString();
    }
}