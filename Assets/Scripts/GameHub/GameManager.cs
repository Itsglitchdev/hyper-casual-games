using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [Header("GameButtons")]
    [SerializeField] private Button ringOGameButton;

    void Start()
    {
        ButtonEventListners();
    }

    void ButtonEventListners()
    {
        ringOGameButton.onClick.AddListener(OnRingOButtonCLicked);
    }

    void OnRingOButtonCLicked()
    { 
        SceneLoader.Load(Scene.Loading, Scene.RingO);
    }

}
