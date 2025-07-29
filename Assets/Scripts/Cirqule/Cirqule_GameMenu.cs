using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cirqule_GameMenu : MonoBehaviour
{
    private const string HIGHSCORE_KEY = "HighScore_Cirqule";

    public static Action OnUIClicked;

    [Header("Reference")]
    [SerializeField] private Button startButton;
    [SerializeField] private TextMeshProUGUI highScoreTextUI;

    void Start()
    {
        startButton.onClick.AddListener(OnStartButtonClicked);
        int savedHighScore = PlayerPrefs.GetInt(HIGHSCORE_KEY, 0);
        UpdateHighScore(savedHighScore);
    }

    void OnStartButtonClicked()
    {
        OnUIClicked?.Invoke();
        SceneLoader.Load(Scene.Loading, Scene.Cirqule);
    }
    
    private void UpdateHighScore(int highScore)
    {
        highScoreTextUI.text = "High Score: " + highScore.ToString();
    }

}
