using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGrid : MonoBehaviour
{
    public GameObject gridContainer;

    List<Transform> listOfCells;

    public float scaleOfCells = 0.7f;

    void Start()
    {
        gridContainer = this.gameObject;
        listOfCells = new List<Transform>();

        foreach(Transform cell in gridContainer.GetComponentsInChildren<Transform>())
        {
            if(cell.tag == "Cell")
            {
                listOfCells.Add(cell);
            }
        }

    }
    public Vector3 GetCellToMove(Vector3 direction, Transform objectToMove)
    {
        Vector3 newPosi =new Vector3(-100,-100,-100);

        Transform cellParent = objectToMove.parent;


        foreach (Transform cell in listOfCells)
        {
            //Debug.Log("Object position: " + objectLocalPosition + " Cell position" + cell.transform.localPosition);
            if(cell.localPosition.x == (cellParent.localPosition.x+direction.x) && cell.localPosition.z == (cellParent.localPosition.z +direction.z))
            {
                if (cell.childCount == 0)
                {
                    objectToMove.transform.parent = cell.transform;
                    return newPosi = cell.position;
                }
            }
        }
        return newPosi;
    }
}
