using UnityEngine;

public class RingO_Coin : MonoBehaviour
{
   
    private readonly float rotateSpeed = 100f;

    void Update()
    {
        if(RingO_GameManagerr.instance.isGameStart == false)  return;
        
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }
}
