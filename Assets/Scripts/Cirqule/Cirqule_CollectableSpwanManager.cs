using System.Collections.Generic;
using UnityEngine;

public class Cirqule_CollectableSpwanManager : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private GameObject[] collectableObjects;

    private int lastCollectableIndex = -1;
    private int currentCollectableIndex = -1;


    private void Start()
    {
        CollectableObjects();
        // GetRandomCollectable();
    }

    void OnEnable()
    {
        Cirqule_UIManager.OnPTapToPlayButtonClicked += GetRandomCollectable;
        Cirqule_Player.OnCollectableColleted += OnCollectCollectable;
    }

    void OnDisable()
    {
        Cirqule_UIManager.OnPTapToPlayButtonClicked -= GetRandomCollectable;
        Cirqule_Player.OnCollectableColleted -= OnCollectCollectable;
    }


    void CollectableObjects()
    {
        for (int i = 0; i < collectableObjects.Length; i++)
        {
            collectableObjects[i].SetActive(false);
        }
    }

    private void GetRandomCollectable()
    {
        List<int> possibleIndices = new List<int>();

        for (int i = 0; i < collectableObjects.Length; i++)
        {
            if (i != lastCollectableIndex)
            {
                possibleIndices.Add(i);
            }
        }

        int randomIndex = possibleIndices[Random.Range(0, possibleIndices.Count)];
        currentCollectableIndex = randomIndex;
        collectableObjects[currentCollectableIndex].SetActive(true);
    }

    private void OnCollectCollectable()
    {
        if (currentCollectableIndex != -1)
        {
            collectableObjects[currentCollectableIndex].SetActive(false);
            lastCollectableIndex = currentCollectableIndex;
            currentCollectableIndex = -1;
            GetRandomCollectable();
        }


    }

}