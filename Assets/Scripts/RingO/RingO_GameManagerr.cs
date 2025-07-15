using UnityEngine;
using System.Collections;
using System;

public class RingO_GameManagerr : MonoBehaviour {

    public static RingO_GameManagerr instance;

    public Action OnCoinCollected;

    public bool isGameStart = false;
    private int score = 0;

    
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

    void Start()
    {
        InitializedGame();
    }

    void InitializedGame()
    {
        score = 0;
        isGameStart = true;
    } 

    public void AddScore(int value)
    {
        score += value;
        OnCoinCollected?.Invoke();
    }

   
}