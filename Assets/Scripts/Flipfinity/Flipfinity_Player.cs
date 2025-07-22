using UnityEngine;

public class Flipfinity_Player : MonoBehaviour
{
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
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("space"))
        {
            ToggleRotation();
        }
    }

    void FixedUpdate()
    {
        CircularMovement();
    }

    void CircularMovement()
    {
        float rotateValue = moveSpeed * Time.fixedDeltaTime * rotationDirection;
        transform.Rotate(0, 0, rotateValue);
    }

    void ToggleRotation()
    { 
        rotationDirection *= -1;
    }


}