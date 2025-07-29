using UnityEngine;

public class Cirqule_VisualManager : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] SpriteRenderer innerRing;

    void OnEnable()
    {
        Cirqule_GameManager.OnVisualBackgroundChanged += ChangeColor;
    }

    void OnDisable()
    {
        Cirqule_GameManager.OnVisualBackgroundChanged -= ChangeColor;
    }

    void ChangeColor()
    { 
        Color color = new Color(
            Random.Range(0.4f, 0.7f), 
            Random.Range(0.5f, 0.7f), 
            Random.Range(0.6f, 0.7f), 
            1f
        );
        cam.backgroundColor = color;
        innerRing.color = color;
    }
    
}