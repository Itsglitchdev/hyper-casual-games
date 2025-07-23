using UnityEngine;

public class RingO_VisualManager : MonoBehaviour
{

    [SerializeField] private Camera cam;
    [SerializeField] private SpriteRenderer innerRing;

    void OnEnable()
    {
        RingO_GameManagerr.instance.OnBackgroundChange += ApplyColor;
    }

    void OnDisable()
    {
        RingO_GameManagerr.instance.OnBackgroundChange -= ApplyColor;
    }

    void ApplyColor()
    {
        Color color = new Color(
            Random.Range(0.3f, 0.7f), 
            Random.Range(0.3f, 0.7f), 
            Random.Range(0.3f, 0.7f), 
            1f
        );
        cam.backgroundColor = color;
        innerRing.color = color;
    }
    

}
