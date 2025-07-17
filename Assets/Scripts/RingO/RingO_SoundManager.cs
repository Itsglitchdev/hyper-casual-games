using UnityEngine;

public class RingO_SoundManager : MonoBehaviour
{

    public static RingO_SoundManager instance;

    [SerializeField] private AudioSource audioSource;

    [Header("AudioClip")]
    [SerializeField] AudioClip coinCollectSound;
    [SerializeField] AudioClip playerDiedSound;
    [SerializeField] AudioClip laneChangeSound;

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


    public void PlayCoinCollectSound()
    {
        audioSource.PlayOneShot(coinCollectSound);

    }

    public void PlayPlayerDiedSound()
    {
        audioSource.PlayOneShot(playerDiedSound);
    }
    
    public void PlayLaneChangeSound()
    {
        audioSource.PlayOneShot(laneChangeSound);
    }
    


}