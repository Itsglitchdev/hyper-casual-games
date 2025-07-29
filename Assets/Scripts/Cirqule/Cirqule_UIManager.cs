using TMPro;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Cirqule_UIManager : MonoBehaviour
{
    public static event Action OnPTapToPlayButtonClicked;
    public static event Action OnPauseButtonClicked;
    public static event Action OnResumeButtonClicked;
    public static event Action OnRestartButtonClicked;
    public static event Action OnUIButtonClicked;

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
        Cirqule_Player.OnPlayerDied += GameOverUI;

        Cirqule_GameManager.OnGameInitialized += InitializedGameUI;
        Cirqule_GameManager.OnScoreUpdated += UpdateScoreUI;
        Cirqule_GameManager.OnHighScoreUpdated += UpdateHighScoreUI;
        Cirqule_GameManager.OnGameOverUIText += GameOverUIText;

    }

    void OnDisable()
    {
        Cirqule_Player.OnPlayerDied -= GameOverUI;

        Cirqule_GameManager.OnGameInitialized -= InitializedGameUI;
        Cirqule_GameManager.OnScoreUpdated -= UpdateScoreUI;
        Cirqule_GameManager.OnHighScoreUpdated -= UpdateHighScoreUI;
        Cirqule_GameManager.OnGameOverUIText -= GameOverUIText;
    }

    void ButtonEventListener()
    {
        tapToPlayButton.onClick.AddListener(StartGameplayUI);
        pauseButton.onClick.AddListener(OnPauseButtonClick);
        resumeButton.onClick.AddListener(OnResumeButtonClick);
        restartButton.onClick.AddListener(OnRestartButtonClick);
        homeButton.onClick.AddListener(OnHomeButtonClick);
    }

    void InitializedGameUI()
    {
        startPanel.SetActive(true);
        inGameScorePanel.SetActive(false);
        pausePanel.SetActive(false);
        resumePanel.SetActive(false);
        restartPanel.SetActive(false);
    }

    void StartGameplayUI()
    {
        OnUIButtonClicked?.Invoke();
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
        OnUIButtonClicked?.Invoke();
        OnPauseButtonClicked?.Invoke();
        resumePanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    void OnResumeButtonClick()
    {
        OnUIButtonClicked?.Invoke();
        OnResumeButtonClicked?.Invoke();
        pausePanel.SetActive(true);
        resumePanel.SetActive(false);
    }

    void OnHomeButtonClick()
    {
        OnUIButtonClicked?.Invoke();
        SceneLoader.Load(Scene.Loading, Scene.Cirqule_GameMenu);
    }

    void OnRestartButtonClick()
    {
        OnUIButtonClicked?.Invoke();
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