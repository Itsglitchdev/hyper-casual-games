using System.Collections.Generic;
using UnityEngine;


public class RingO_ObstaclesManager : MonoBehaviour
{
    [Header("Obstacles")]
    [SerializeField] private GameObject obstaclePrefab;

    private readonly float obstacleLan1PosY = 2.4f;
    private readonly float obstacleLan2PosY = 1.76f;

    private List<GameObject> obstacles = new List<GameObject>();
    private int initallySpawned = 2;
    private int obstacleCount = 0;

    void Start()
    {
        for (int i = 0; i < initallySpawned; i++)
        {
            InstansiateObstacle();
        }
    }


    void InstansiateObstacle()
    {
        float posY = Random.value > 0.65f ? obstacleLan1PosY : obstacleLan2PosY;

        float angleDeg = Random.Range(0f, 360f);
        float angleRad = angleDeg * Mathf.Deg2Rad;

        float x = posY * Mathf.Cos(angleRad);
        float y = posY * Mathf.Sin(angleRad);

        Vector3 pos = new Vector3(x, y, 0);
        Quaternion rotation = Quaternion.Euler(0f, 0f, angleDeg + 90f);
        GameObject obstacle = Instantiate(obstaclePrefab, pos , rotation, transform);

        obstacles.Add(obstacle);
    }

}