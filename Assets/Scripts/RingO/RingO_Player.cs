using UnityEngine;

public class RingO_Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Transform childPlayer;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 80f;

    private readonly float cicularLan1PosY = 2.4f;
    private readonly float cicularLan2PosY = 1.76f;

    private float lanPosY;
    private float targetLaneY;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lanPosY = childPlayer.localPosition.y;
        targetLaneY = lanPosY;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("space"))
        {
            ToggleLane();
        }

        Vector3 pos = childPlayer.localPosition;
        pos.y = Mathf.Lerp(pos.y, targetLaneY, Time.deltaTime * 10f);
        childPlayer.localPosition = pos;

        lanPosY = pos.y;
    }

    void FixedUpdate()
    {
        CircularMovement();
    }

    void ToggleLane()
    {
        targetLaneY = Mathf.Approximately(targetLaneY, cicularLan1PosY) ? cicularLan2PosY : cicularLan1PosY;
    }

    void CircularMovement()
    {
        float rotateValue = moveSpeed * Time.fixedDeltaTime;
        // Debug.Log(rotateValue);
        transform.Rotate(0, 0, rotateValue);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            RingO_GameManagerr.instance.AddScore(1);
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over");
        }
    }

}
