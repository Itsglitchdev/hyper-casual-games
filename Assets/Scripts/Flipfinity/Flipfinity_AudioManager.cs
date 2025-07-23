using UnityEngine;

public class Flipfinity_AudioManager : MonoBehaviour
{


    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip coinCollectSound;
    [SerializeField] private AudioClip playerDiedSound;
    [SerializeField] private AudioClip directionChangeSound;

    void OnEnable()
    {
        Flipfinity_Player.OnCoinCollected += OnCoinCollectSound;
        Flipfinity_Player.OnPlayerDied += OnPlayerDiedSound;
        Flipfinity_Player.OnDirectionChanged += OnDirectionChangeSound;
    }

    void OnDisable()
    {
        Flipfinity_Player.OnCoinCollected -= OnCoinCollectSound;
        Flipfinity_Player.OnPlayerDied -= OnPlayerDiedSound;
        Flipfinity_Player.OnDirectionChanged -= OnDirectionChangeSound;
    }


    private void OnCoinCollectSound() => audioSource.PlayOneShot(coinCollectSound);
    private void OnPlayerDiedSound() => audioSource.PlayOneShot(playerDiedSound);
    private void OnDirectionChangeSound() => audioSource.PlayOneShot(directionChangeSound);

}