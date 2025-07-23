using UnityEngine;

public class Flipfinity_Obstacle : MonoBehaviour
{
    private Vector3 direction;
    private float rotationSpeed = 100f;
    private float moveSpeed = 2.25f;

    void Start()
    {
        transform.position = Vector3.zero;
        direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f).normalized;
    }

    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}