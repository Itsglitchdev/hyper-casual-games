using UnityEngine;

public class RingO_Coin : MonoBehaviour
{
   
    private readonly float rotateSpeed = 100f;

    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }
}
