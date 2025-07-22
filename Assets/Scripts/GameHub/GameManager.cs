using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [Header("GameButtons")]
    [SerializeField] private Button ringOGameButton;
    [SerializeField] private Button flipFinityGameButton;


    void Start()
    {
        ButtonEventListners();
    }

    void ButtonEventListners()
    {
        ringOGameButton.onClick.AddListener(OnRingOButtonCLicked);
        flipFinityGameButton.onClick.AddListener(OnFlipFinityGameButtonCLicked);
    }

    void OnRingOButtonCLicked()
    {
        SceneLoader.Load(Scene.Loading, Scene.RingO);
    }
    
    void OnFlipFinityGameButtonCLicked()
    {
        SceneLoader.Load(Scene.Loading, Scene.Flipfinity);
    }

}
