using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPropagation : MonoBehaviour
{
    public Transform opositeLightEmisor;

    public string layerRayDetectionName;
    public string layerReceptorName;

    public LineRenderer lineRenderer;
    Vector3[] lightPoints;

    int rayDetectorLayer;
    int receptorLayer;

    void Awake()
    {
        //lineRenderer = FindObjectOfType<LineRenderer>();

        //rayDetection
        rayDetectorLayer = LayerMask.NameToLayer(layerRayDetectionName);
        //receptorDetection
        receptorLayer = LayerMask.NameToLayer(layerReceptorName);
    }
    public void PropagateRay()
    {
        RaycastHit raycasthit;

        Vector3 origin = opositeLightEmisor.position;
        Vector3 direction = opositeLightEmisor.right;

        if (Physics.Raycast(origin, direction, out raycasthit))
        {

            if (raycasthit.collider.transform.gameObject.layer == rayDetectorLayer)
            {
                DrawLightRay(lightPoints = new Vector3[] { opositeLightEmisor.transform.position, raycasthit.point });
                raycasthit.collider.transform.gameObject.GetComponent<LightPropagation>().PropagateRay();
            }
            else if(raycasthit.collider.transform.gameObject.layer == receptorLayer)
            {
                DrawLightRay(lightPoints = new Vector3[] { opositeLightEmisor.transform.position, raycasthit.point });
                raycasthit.collider.transform.gameObject.GetComponent<LightReceptor>().CompletedPuzzle();
            }
            //else
            //    DrawLightRay(lightPoints = new Vector3[] { opositeLightEmisor.transform.position, raycasthit.point });
        }
    }
    void DrawLightRay(Vector3[] points)
    {
        int index = lineRenderer.positionCount;
        lineRenderer.positionCount = index + points.Length;
        foreach(Vector3 point in points)
        {
            lineRenderer.SetPosition(index, point);
            index++;
        }
    }
}
