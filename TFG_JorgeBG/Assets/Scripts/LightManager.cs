using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{

    public LightPuzzle[] emisors;
    public LightReceptor[] receptors;
    void Start()
    {
        emisors = FindObjectsOfType<LightPuzzle>();
        receptors = FindObjectsOfType<LightReceptor>();

        GenerateEmisionLight();

    }

    private void OnEnable()
    {
        EventManager.RecalculateLine += GenerateEmisionLight;
        EventManager.ClearLine += ClearLinePoints;
        EventManager.ReceptorHit += CheckSolved;
    }
    private void OnDisable()
    {
        EventManager.RecalculateLine -= GenerateEmisionLight;
        EventManager.ClearLine -= ClearLinePoints;
        EventManager.ReceptorHit -= CheckSolved;

    }

    public void GenerateEmisionLight()
    {
        foreach(LightPuzzle emisor in emisors)
        {
            emisor.GenerateLight();
        }
    }

    public void ClearLinePoints()
    {
        foreach(LightReceptor receptor in receptors)
        {
            receptor.isCompleted = false;
        }

        foreach (LightPuzzle emisor in emisors)
        {
            emisor.ClearLineRenderer();
        }
    }

    public void CheckSolved()
    {
        int count = 0;
        foreach (LightReceptor receptor in receptors)
        {
            if (receptor.isCompleted)
                count++;
        }
        if (count == receptors.Length)
            EventManager.OnCompleteLevel();
    }
}
