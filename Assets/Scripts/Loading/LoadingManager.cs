using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{

    [Header("UI References")]
    [SerializeField] private TMP_Text gameTipText;
    [SerializeField] private Slider loadingSlider;

    private readonly float minimumLoadingTime = 3.5f;

    void Start()
    {

        StartCoroutine(LoadingTargetScene());
    }

    private IEnumerator LoadingTargetScene()
    {
        string targetScene = SceneLoader.targetSceneName;
        string tip = GetGameTip(targetScene);
        gameTipText.text = tip;

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetScene);
        asyncLoad.allowSceneActivation = false;

        float elapsed  = 0f;

        while (elapsed < minimumLoadingTime)
        {
            elapsed += Time.deltaTime;
            
            float progress = Mathf.Clamp01(elapsed / minimumLoadingTime);
            float loadProgress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            loadingSlider.value = Mathf.Min(progress, loadProgress);

            if (elapsed >= minimumLoadingTime && asyncLoad.progress >= 0.9f)
            {
                break; 
            }

            yield return null;
        }
        asyncLoad.allowSceneActivation = true;
        yield return new WaitForSeconds(0.2f);

    }

    private string GetGameTip(string sceneName) { 

        switch (sceneName) {

            case "RingO":
                return "Game Tip : Things aren’t what they seem... Tap to switch lanes, but beware obstacles flip in a blink!";
            case "Flipfinity":
                return "Game Tip : Timing is everything..tap to change direction at just the right moment and stay in the rhythm.";    
            default:
                return "Sure? Are you give up? I think you can, but u are tha same.";
        }

    }

}