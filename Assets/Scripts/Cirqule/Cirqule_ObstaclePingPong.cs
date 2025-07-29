using System.Collections;
using UnityEngine;

public class Cirqule_ObstaclePingPong : MonoBehaviour
{
    [SerializeField] private Vector3 direction;
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float distance = 1.4f;
    [SerializeField] private float startDelay = 1f;

    private float localTime = 0f;
    private bool canMove;
    private Vector3 startPos;

    void Start()
    {
        StopAllCoroutines();
        startPos = transform.position;
        canMove = false;
        StartCoroutine(StartMovementAfterDelay());
    }

    void OnEnable()
    {
        Cirqule_GameManager.OnVisualBackgroundChanged += SpeedIncrease;
    }

    void OnDisable()
    {
        Cirqule_GameManager.OnVisualBackgroundChanged -= SpeedIncrease;
    }

    void Update()
    {
        if (Cirqule_GameManager.Instance.IsGameStart == false)
        {
            // canMove = false;
            return;
        }
        if (!canMove) return;
        localTime += Time.deltaTime;
        float offset = Mathf.PingPong(localTime * speed, distance);
        transform.position = startPos + (direction.normalized * offset);
    }

    IEnumerator StartMovementAfterDelay()
    {
        yield return new WaitForSeconds(startDelay);
        canMove = true;

    }

    void SpeedIncrease()
    { 
        speed += 0.03f;
    }



}
