using UnityEngine;
using UnityEngine.EventSystems;

public class Cirqule_Player : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 100f;

    private float rotationDirection = 1f;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            ToggleDirection();
        }
    }

    void FixedUpdate()
    {
        CircularMovement();
    }

    void ToggleDirection()
    {
        rotationDirection *= -1f;
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
            // Destroy(collision.gameObject);
        }
        
        if (collision.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over");
            gameObject.SetActive(false);
        }
    }


}