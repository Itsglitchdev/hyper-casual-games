using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cirqule_Player : MonoBehaviour
{

    public static event Action OnCollectableColleted;
    public static event Action OnDirectionChanged;
    public static event Action OnPlayerDied;
    public static event Action<Vector3> OnPlayerDiedEffect;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 125f;

    private float rotationDirection = 1f;

    void Start()
    {

    }

    void Update()
    {
        if(Cirqule_GameManager.Instance.IsGameStart == false) return;

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            ToggleDirection();
        }
    }

    void FixedUpdate()
    {
        if(Cirqule_GameManager.Instance.IsGameStart == false) return;

        CircularMovement();
    }

    void ToggleDirection()
    {
        rotationDirection *= -1f;
        OnDirectionChanged?.Invoke();
    }

    void CircularMovement()
    {
        float rotateValue = moveSpeed * rotationDirection * Time.fixedDeltaTime;
        transform.Rotate(0, 0, rotateValue);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectable"))
        {
            collision.gameObject.SetActive(false);
            OnCollectableColleted?.Invoke();
        }

        if (collision.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over");
            gameObject.SetActive(false);
            OnPlayerDiedEffect?.Invoke(collision.transform.position);
            OnPlayerDied?.Invoke();
        }
    }


}