using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RingO_UIManager : MonoBehaviour
{
    public event Action OnPTapToPlayButtonClicked;
    public event Action OnPauseButtonClicked;
    public event Action OnResumeButtonClicked;
    public event Action OnRestartButtonClicked;

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
    [SerializeField] private TextMeshProUGUI tapToPlayText;
    [SerializeField] private TextMeshProUGUI gameOverScoreText;
    [SerializeField] private TextMeshProUGUI gameOverHighScoreText;

    void Start()
    {
        ButtonEventHandler();
    }

    void OnEnable()
    {
        RingO_GameManagerr.instance.OnGameInitialized += InitializeGameUI;
        RingO_GameManagerr.instance.OnCoinCollected += UpdateScoreUI;
        RingO_GameManagerr.instance.OnHighScoreChanged += UpdateHighScoreUI;
        RingO_GameManagerr.instance.OnGameOver += GameOverUI;
        RingO_GameManagerr.instance.OnGameOverUIText += GameOverUIText;
    }

    void OnDisable()
    {
        RingO_GameManagerr.instance.OnGameInitialized -= InitializeGameUI;
        RingO_GameManagerr.instance.OnCoinCollected -= UpdateScoreUI;
        RingO_GameManagerr.instance.OnHighScoreChanged -= UpdateHighScoreUI;
        RingO_GameManagerr.instance.OnGameOver -= GameOverUI;
        RingO_GameManagerr.instance.OnGameOverUIText -= GameOverUIText;
    }

    void InitializeGameUI()
    {
        startPanel.SetActive(true);
        inGameScorePanel.SetActive(false);
        pausePanel.SetActive(false);
        resumePanel.SetActive(false);
        restartPanel.SetActive(false);
    }


    void ButtonEventHandler()
    {
        tapToPlayButton.onClick.AddListener(StartGameplayUI);
        pauseButton.onClick.AddListener(OnPauseButtonClick);
        resumeButton.onClick.AddListener(OnResumeButtonClick);
        restartButton.onClick.AddListener(OnRestartButtonClick);
        homeButton.onClick.AddListener(OnHomeButtonClick);
    }

    void StartGameplayUI()
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