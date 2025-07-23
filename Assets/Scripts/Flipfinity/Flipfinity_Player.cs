using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Flipfinity_Player : MonoBehaviour
{

    public static event Action OnPlayerDied;
    public static event Action OnCoinCollected;
    public static event Action OnDirectionChanged;
    public static event Action<Vector3> onCoinEffect;
    public static event Action<Vector3> onScoreEffect;


    [Header("Player")]
    [SerializeField] private Transform childPlayer1;
    [SerializeField] private Transform childPlayer2;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 100f;

    private int rotationDirection = 1;

    void Start()
    {

    }

    void Update()
    {
        if(Flipfinity_GameManager.instance.IsGameStart == false)  return;

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            if (EventSystem.current.IsPointerOverGameObject())  return;

            ToggleDirection();
        }
    }

    void FixedUpdate()
    {
        if(Flipfinity_GameManager.instance.IsGameStart == false)  return;

        CircularMovement();
    }

    void CircularMovement()
    {
        float rotateValue = moveSpeed * Time.fixedDeltaTime * rotationDirection;
        transform.Rotate(0, 0, rotateValue);
    }

    void ToggleDirection()
    {
        OnDirectionChanged?.Invoke();
        rotationDirection *= -1;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            OnCoinCollected?.Invoke();
            onCoinEffect?.Invoke(collision.transform.position);
            Destroy(collision.gameObject);
        }
        
        if (collision.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over");
            OnPlayerDied?.Invoke();
            onScoreEffect?.Invoke(collision.transform.position);
            gameObject.SetActive(false);
        }
    }

}