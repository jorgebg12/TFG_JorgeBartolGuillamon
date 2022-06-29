using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LightPuzzle : MonoBehaviour
{

    public Transform lightEmisor;

    public LineRenderer lineRenderer;
    private Vector3[] pointsLine;
    //playerController playerController;

    int rayDetectorLayer;
    public string layerRayDetection;
    void OnEnable()
    {
        //EventManager.RecalculateLine += GenerateLight;
        //EventManager.ClearLine += ClearLineRenderer;

        //playerController.playerInputActions.characterControls.UseObject.started += OnUseObject;
    }
    void OnDisable()
    {
        //EventManager.RecalculateLine -= GenerateLight;
        //EventManager.ClearLine -= ClearLineRenderer;

        //playerController.playerInputActions.characterControls.UseObject.started -= OnUseObject;
    }
    void Awake()
    {
        rayDetectorLayer = LayerMask.NameToLayer(layerRayDetection);
        //playerController = FindObjectOfType<playerController>();

    }
    private void Start()
    {
        //EventManager.OnRecalculateLine();
    }
    //void OnUseObject(InputAction.CallbackContext obj)
    //{
    //    RaycastHit raycasthit;
    //    //Debug.DrawLine(lightEmisor.position, lightEmisor.position + lightEmisor.right,Color.blue,600f);
    //    ClearLineRenderer();

    //    if (Physics.Raycast(lightEmisor.position, lightEmisor.right, out raycasthit))
    //    {
    //        if (raycasthit.collider.transform.gameObject.layer == rayDetectorLayer)
    //        {
    //            pointsLine = new Vector3[] { lightEmisor.transform.position, raycasthit.point};
    //            DrawLightRay(pointsLine);
    //            raycasthit.collider.transform.gameObject.GetComponent<LightPropagation>().PropagateRay();
    //            return;
    //        }
    //    }
    //}
    void DrawLightRay(Vector3[] points)
    {
        int index = lineRenderer.positionCount;
        lineRenderer.positionCount = index + points.Length;
        foreach (Vector3 point in points)
        {
            lineRenderer.SetPosition(index, point);
            index++;
        }
    }
    public void ClearLineRenderer()
    {
        lineRenderer.positionCount = 0;
    }
    public void GenerateLight()
    {
        RaycastHit raycasthit;
        //Debug.DrawLine(lightEmisor.position, lightEmisor.position + lightEmisor.right,Color.blue,600f);
        ClearLineRenderer();

        if (Physics.Raycast(lightEmisor.position, lightEmisor.right, out raycasthit))
        {
            if (raycasthit.collider.transform.gameObject.layer == rayDetectorLayer)
            {
                
                DrawLightRay(pointsLine = new Vector3[] { lightEmisor.transform.position, raycasthit.point });
                raycasthit.collider.transform.gameObject.GetComponent<LightPropagation>().PropagateRay();
                return;
            }
        }
    }

}
