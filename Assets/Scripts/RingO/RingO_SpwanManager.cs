using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework;
using System.Collections;

public class RingO_SpwanManager : MonoBehaviour
{

    [Header("Prefabs")]
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject obstaclePrefab;

    [Header("ParentsHolder")]
    [SerializeField] private Transform coinParent;
    [SerializeField] private Transform obstacleParent;

    private readonly float spwanLan1PosY = 2.4f;
    private readonly float spwanLan2PosY = 1.75f;
    private const float minDistanceBetweenCoinsSpawns = 0.75f;
    private const float minDistanceBetweenObstaclesSpawns = 1.5f;
    private int initallySpawnedObstacles = 2;
    private int obstaclesCount = 0;

    private List<GameObject> obstacles = new List<GameObject>();
    private List<GameObject> coins = new List<GameObject>();


    void Start()
    {
        obstaclesCount = 0;
        InstansiateCoin();
        for (int i = 0; i < initallySpawnedObstacles; i++)
        {
            InstansiateObstacle();
        }
    }

    private void OnEnable()
    {
        RingO_GameManagerr.instance.OnCoinCollected += OnCoinCollected;
        RingO_GameManagerr.instance.OnBonusAllObstaclesLanesChange += SwitchObstacleLanes;
        RingO_GameManagerr.instance.OnVisualOneObstacleLaneChange += OneRandomObstacleLaneChange;
        RingO_GameManagerr.instance.OnGameOver += DestroyAllCoins;
        RingO_GameManagerr.instance.OnGameOver += DestroyAllObstacles;
    }

    private void OnDisable()
    {
        RingO_GameManagerr.instance.OnCoinCollected -= OnCoinCollected;
        RingO_GameManagerr.instance.OnBonusAllObstaclesLanesChange -= SwitchObstacleLanes;
        RingO_GameManagerr.instance.OnVisualOneObstacleLaneChange -= OneRandomObstacleLaneChange;
        RingO_GameManagerr.instance.OnGameOver -= DestroyAllCoins;
        RingO_GameManagerr.instance.OnGameOver -= DestroyAllObstacles;
    }

    void InstansiateCoin()
    {
        for (int attempt = 0; attempt < 10; attempt++)
        {
            float coinPosY = Random.value > 0.5f ? spwanLan1PosY : spwanLan2PosY;
            float angleDeg = Random.Range(0f, 360f);
            float angleRad = angleDeg * Mathf.Deg2Rad;
            float x = coinPosY * Mathf.Cos(angleRad);
            float y = coinPosY * Mathf.Sin(angleRad);
            Vector3 pos = new Vector3(x, y, 0);

            bool conflict = false;

            foreach (var obstacle in obstacles)
            {
                if (Vector3.Distance(obstacle.transform.position, pos) < minDistanceBetweenCoinsSpawns)
                {
                    conflict = true;
                    break;
                }
            }

            if (!conflict)
            {
                GameObject coin = Instantiate(coinPrefab, pos, coinPrefab.transform.rotation, coinParent);
                coins.Add(coin);
                return;
            }
        }

        Debug.Log("Failed to spawn coin");

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
        coins.Clear();
    }

    void InstansiateObstacle()
    {

        for (int attempt = 0; attempt < 10; attempt++)
        {
            float posY = Random.value > 0.65f ? spwanLan1PosY : spwanLan2PosY;
            float angleDeg = Random.Range(0f, 360f);
            float angleRad = angleDeg * Mathf.Deg2Rad;

            float x = posY * Mathf.Cos(angleRad);
            float y = posY * Mathf.Sin(angleRad);
            Vector3 pos = new Vector3(x, y, 0);

            bool conflict = false;

            foreach (var coin in coins)
            {
                if (Vector3.Distance(coin.transform.position, pos) < minDistanceBetweenObstaclesSpawns)
                {
                    conflict = true;
                    break;
                }
            }
            foreach (var obstacle in obstacles)
            {
                if (Vector3.Distance(obstacle.transform.position, pos) < minDistanceBetweenObstaclesSpawns)
                {
                    conflict = true;
                    break;
                }
            }

            if (!conflict)
            {
                Quaternion rotation = Quaternion.Euler(0f, 0f, angleDeg + 90f);
                GameObject obstacle = Instantiate(obstaclePrefab, pos, rotation, obstacleParent);
                obstaclesCount++;
                obstacles.Add(obstacle);
                return;
            }
        }
    }

    void SwitchObstacleLanes()
    {
        if (obstaclesCount < 7)
        {
            InstansiateObstacle();
        }
        else if (obstaclesCount == 7)
        {
            DestroyRandomObstacle();
            InstansiateObstacle();
        }
        StopAllCoroutines();
        StartCoroutine(SwitchObstacleLanesCoroutine());
    }

    IEnumerator SwitchObstacleLanesCoroutine()
    {
        float duration = 0.25f;
        float elapsed = 0f;

        List<Vector3> startPositions = new List<Vector3>();
        List<Vector3> targetPositions = new List<Vector3>();

        foreach (GameObject obstacle in obstacles)
        {
            if (obstacle == null)
            {
                startPositions.Add(Vector3.zero);
                targetPositions.Add(Vector3.zero);
                continue;
            }

            Vector3 startPos = obstacle.transform.position;
            float radius = Mathf.Sqrt(startPos.x * startPos.x + startPos.y * startPos.y);
            float angle = Mathf.Atan2(startPos.y, startPos.x);

            float newRadius = Mathf.Approximately(radius, spwanLan1PosY) ? spwanLan2PosY : spwanLan1PosY;

            float newX = newRadius * Mathf.Cos(angle);
            float newY = newRadius * Mathf.Sin(angle);
            Vector3 targetPos = new Vector3(newX, newY, 0);

            startPositions.Add(startPos);
            targetPositions.Add(targetPos);
        }

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            for (int i = 0; i < obstacles.Count; i++)
            {
                if (obstacles[i] == null) continue;

                obstacles[i].transform.position = Vector3.Lerp(startPositions[i], targetPositions[i], t);
            }

            yield return null;
        }

        for (int i = 0; i < obstacles.Count; i++)
        {
            if (obstacles[i] == null) continue;
            obstacles[i].transform.position = targetPositions[i];
        }
    }

    void OneRandomObstacleLaneChange()
    { 
        StopAllCoroutines();
        StartCoroutine(OneRandomObstacleLaneChangeCoroutine());
    }

    IEnumerator OneRandomObstacleLaneChangeCoroutine()
    {
        if (obstacles.Count == 0) yield break;

        GameObject selectedObstacle = null;
        int safety = 0;
        while (selectedObstacle == null && safety < 10)
        {
            int index = Random.Range(0, obstacles.Count);
            if (obstacles[index] != null)
            {
                selectedObstacle = obstacles[index];
            }
            safety++;
        }

        if (selectedObstacle == null) yield break;

        // Start lane-switch animation
        float duration = 0.25f;
        float elapsed = 0f;

        Vector3 startPos = selectedObstacle.transform.position;
        float radius = Mathf.Sqrt(startPos.x * startPos.x + startPos.y * startPos.y);
        float angle = Mathf.Atan2(startPos.y, startPos.x);

        float newRadius = Mathf.Approximately(radius, spwanLan1PosY) ? spwanLan2PosY : spwanLan1PosY;

        Vector3 targetPos = new Vector3(
            newRadius * Mathf.Cos(angle),
            newRadius * Mathf.Sin(angle),
            0
        );

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsed / duration);
            if (selectedObstacle != null)
                selectedObstacle.transform.position = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }

        if (selectedObstacle != null)
            selectedObstacle.transform.position = targetPos;
    }

    void DestroyRandomObstacle()
    {
        int randomIndex = Random.Range(0, obstacles.Count - 1);
        GameObject obstacle = obstacles[randomIndex];
        obstacles.RemoveAt(randomIndex);
        obstaclesCount--;
        Destroy(obstacle);
    }

    void DestroyAllObstacles()
    {
        foreach (GameObject obstacle in obstacles)
        {
            Destroy(obstacle);
        }
        obstacles.Clear();

    }
}