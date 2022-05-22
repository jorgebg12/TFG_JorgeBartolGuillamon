using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public GameObject topCell;
    public GameObject leftCell;
    public GameObject rightCell;
    public GameObject downCell;

    public Transform objectAbove;
    public bool isOcupied;
    public Vector3 cellPosition { get { return this.transform.position; }}

    public void GetNeighbours()
    {
        //RaycastHit cellDetected;
        //Debug.DrawLine(this.transform.position, this.transform.position + Vector3.forward, Color.blue, 500f);
        //if(Physics.Raycast(this.transform.position, this.transform.position+Vector3.forward,out cellDetected,1f))
        //{
        //    Debug.Log(cellDetected.transform.gameObject.name);
        //    topCell = cellDetected.transform.gameObject;
        //}
    }
}
