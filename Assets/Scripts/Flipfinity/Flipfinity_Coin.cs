using UnityEngine;

public class Flipfinity_Coin : MonoBehaviour
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

        // Space.World is needed here because Translate uses local space by default, which means movement is affected by the object's rotation. By specifying Space.World, we ensure the object moves in a fixed world direction.
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);

        // transform.position += direction * Time.deltaTime * moveSpeed;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}