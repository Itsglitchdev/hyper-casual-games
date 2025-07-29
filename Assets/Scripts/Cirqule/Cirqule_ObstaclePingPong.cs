using UnityEngine;

public class Cirqule_ObstaclePingPong : MonoBehaviour
{
    [SerializeField] private Vector3 direction;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float distance = 1.25f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        float offset = Mathf.PingPong(Time.time * speed, distance);
        transform.localPosition = startPos + (direction.normalized * offset);
    }
    
}
