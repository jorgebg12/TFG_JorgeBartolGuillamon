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
    playerController playerController;

    public int rayDetectorLayer;
    public LayerMask layerRayDetection;
    void Start()
    {
        rayDetectorLayer = LayerMask.NameToLayer("rayDetection");
        playerController = FindObjectOfType<playerController>();

        playerController.playerInputActions.characterControls.UseObject.started += OnUseObject;
    }

    void ConectLightPoints(Vector3[] points)
    {
        lineRenderer.SetPositions(points);
    }

    private void OnUseObject(InputAction.CallbackContext obj)
    {

        RaycastHit raycasthit;

        //Debug.DrawLine(lightEmisor.position, lightEmisor.position + lightEmisor.right,Color.blue,600f);

        if(Physics.Raycast(lightEmisor.position, lightEmisor.right,out raycasthit))
        {
            

            if (raycasthit.collider.transform.gameObject.layer == rayDetectorLayer)
            {
                pointsLine = new Vector3[] { lightEmisor.transform.position, raycasthit.point };
                ConectLightPoints(pointsLine);
                raycasthit.collider.transform.gameObject.GetComponent<LightPropagation>().PropagateRay();
            }
        }
    }

}
