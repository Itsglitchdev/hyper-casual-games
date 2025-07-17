using System.Collections.Generic;
using UnityEngine;

public class RingO_CoinManager : MonoBehaviour
{

    [Header("Prefabs")]
    [SerializeField] private GameObject coinPrefab;

    private readonly float coinLan1PosY = 2.4f;
    private readonly float coinLan2PosY = 1.75f;

    private List<GameObject> coins = new List<GameObject>();

    void Start()
    {
        InstansiateCoin();
    }

    void OnEnable()
    {
        RingO_GameManagerr.instance.OnCoinCollected += OnCoinCollected;
        RingO_GameManagerr.instance.OnGameOver += DestroyAllCoins;
    }

    void OnDisable()
    {
        RingO_GameManagerr.instance.OnCoinCollected -= OnCoinCollected;
        RingO_GameManagerr.instance.OnGameOver -= DestroyAllCoins;
    }

    void InstansiateCoin()
    {

        float coinPosY = Random.value > 0.6f ? coinLan1PosY : coinLan2PosY;

        float angleDeg = Random.Range(0f, 360f);
        float angleRad = angleDeg * Mathf.Deg2Rad;

        float x = coinPosY * Mathf.Cos(angleRad);
        float y = coinPosY * Mathf.Sin(angleRad);

        Vector3 pos = new Vector3(x, y, 0);
        GameObject coin = Instantiate(coinPrefab, pos, coinPrefab.transform.rotation, transform);

        coins.Add(coin);
    }


    void RemoveCoin()
    {
        coins.Remove(coins[0]);
    }

    void OnCoinCollected(int value)
    {
        RemoveCoin();
        InstansiateCoin();
    }
    
    void DestroyAllCoins()
    {
        foreach (GameObject coin in coins)
        {
            Destroy(coin);
        }
    }
}
