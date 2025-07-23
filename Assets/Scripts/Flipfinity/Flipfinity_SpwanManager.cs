using System.Collections;
using UnityEngine;

public class Flipfinity_SpwanManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject obstaclePrefab;

    [Header("ParentsHolder")]
    [SerializeField] private Transform coinParent;
    [SerializeField] private Transform obstacleParent;

    [Header("Spawning Settings")]
    [SerializeField] private float minSpawnDelay = 1.5f;
    [SerializeField] private float maxSpawnDelay = 3f;

    private bool isSpawning = false;    
    private int lastCoinMilestone = 0;
    

    void OnEnable()
    {
        isSpawning = true;       
        StartCoroutine(SpawningLoop()); 
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator SpawningLoop()
    {
        while (isSpawning)
        { 
            float waitTime = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(waitTime);

            int coinCount = Flipfinity_GameManager.instance.CoinCount;
            int milestone = coinCount / 4;

            if (coinCount > 0 && coinCount % 4 == 0 && milestone > lastCoinMilestone)
            {
                lastCoinMilestone = milestone;
            }

            int obstacleCount = Mathf.Max(1, lastCoinMilestone + 1);
            int coinCountToSpawn = 1;

            for (int i = 0; i < coinCountToSpawn; i++)
            {
                InstansiateCoin();
                yield return new WaitForSeconds(Random.Range(0.25f, 0.35f)); 
            }

            for (int i = 0; i < obstacleCount; i++)
            {
                InstansiateObstacle();
                yield return new WaitForSeconds(Random.Range(0.1f, 0.3f)); 
            }
        }
    }

    void InstansiateCoin()
    {
        Instantiate(coinPrefab, Vector3.zero, coinPrefab.transform.rotation, coinParent);
    }

    void InstansiateObstacle()
    {
        GameObject obstacle = Instantiate(obstaclePrefab, Vector3.zero, obstaclePrefab.transform.rotation, obstacleParent);
        SpriteRenderer sprite = obstacle.GetComponent<SpriteRenderer>();
        sprite.color = new Color(
            Random.Range(0f, 0.4f),
            Random.Range(0f, 0.4f),
            Random.Range(0f, 0.4f)
        );

    }

}