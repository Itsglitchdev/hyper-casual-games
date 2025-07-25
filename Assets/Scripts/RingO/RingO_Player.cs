using UnityEngine;
using UnityEngine.EventSystems;

public class RingO_Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Transform childPlayer;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 80f;

    private readonly float cicularLan1PosY = 2.13f;
    private readonly float cicularLan2PosY = 1.53f;

    
    // private readonly float cicularLan1PosY = 2.4f;
    // private readonly float cicularLan2PosY = 1.75f;

    private float lanPosY;
    private float targetLaneY;

    void Start()
    {
        lanPosY = childPlayer.localPosition.y;
        targetLaneY = lanPosY;
        gameObject.SetActive(true);
    }

    void Update()
    {

        if(RingO_GameManagerr.instance.isGameStart == false)  return;

        if (EventSystem.current.IsPointerOverGameObject())
            return;

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
        if(RingO_GameManagerr.instance.isGameStart == false)  return;
        CircularMovement();
    }

    void ToggleLane()
    {
        RingO_SoundManager.instance.PlayLaneChangeSound();
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
            RingO_SoundManager.instance.PlayCoinCollectSound();
            RingO_GameManagerr.instance.PlayCoinCollectEffect(collision.transform.position);
            RingO_GameManagerr.instance.AddScore();
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over");
            RingO_SoundManager.instance.PlayPlayerDiedSound();
            RingO_GameManagerr.instance.PlayPlayerDiedEffect(collision.transform.position);
            gameObject.SetActive(false);
            RingO_GameManagerr.instance.GameOver();

        }
    }

}
