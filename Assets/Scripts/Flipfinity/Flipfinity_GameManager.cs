using UnityEngine;

public class Flipfinity_GameManager : MonoBehaviour
{
    
    public static Flipfinity_GameManager instance;
    
    private bool isGameStart;

    public bool IsGameStart => isGameStart;


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
}