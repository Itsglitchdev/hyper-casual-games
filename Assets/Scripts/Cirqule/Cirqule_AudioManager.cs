using System;
using UnityEngine;

public class Cirqule_AudioManager : MonoBehaviour
{
    public static Cirqule_AudioManager Instance { get; private set; }

    [Header("Reference")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioDiedClip;
    [SerializeField] private AudioClip _audioCollectClip;
    [SerializeField] private AudioClip _audioChangeDirectionClip;
    [SerializeField] private AudioClip _audioUIClickClip;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else
        {
            Destroy(gameObject);
        }

    }

    void OnEnable()
    {
        Cirqule_Player.OnCollectableColleted += PlayCoinCollectSound;
        Cirqule_Player.OnPlayerDied += PlayPlayerDiedSound;
        Cirqule_Player.OnDirectionChanged += PlayDirectionChangeSound;
        Cirqule_UIManager.OnUIButtonClicked += PlayUIClickSound;
        Cirqule_GameMenu.OnUIClicked += PlayUIClickSound;
    }

    void OnDisable()
    {
        Cirqule_Player.OnCollectableColleted -= PlayCoinCollectSound;
        Cirqule_Player.OnPlayerDied -= PlayPlayerDiedSound;
        Cirqule_Player.OnDirectionChanged -= PlayDirectionChangeSound;
        Cirqule_UIManager.OnUIButtonClicked -= PlayUIClickSound;
        Cirqule_GameMenu.OnUIClicked -= PlayUIClickSound;
    }

    private void PlayPlayerDiedSound() => _audioSource.PlayOneShot(_audioDiedClip);
    private void PlayCoinCollectSound() => _audioSource.PlayOneShot(_audioCollectClip);
    private void PlayDirectionChangeSound() => _audioSource.PlayOneShot(_audioChangeDirectionClip);
    private void PlayUIClickSound() => _audioSource.PlayOneShot(_audioUIClickClip);
 
    
}