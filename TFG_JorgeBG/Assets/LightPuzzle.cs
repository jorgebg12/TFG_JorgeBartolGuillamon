using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPuzzle : MonoBehaviour
{

    public GameObject lightEmisor;

    public LineRenderer lineRenderer;
    private Transform[] pointsLine;

    public int rayDetectorLayer;
    public LayerMask layerRayDetection;
    void Start()
    {
        rayDetectorLayer = LayerMask.NameToLayer("rayDetection");
    }
    void Update()
    {
        
    }
}
