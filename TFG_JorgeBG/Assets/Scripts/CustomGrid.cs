using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGrid : MonoBehaviour
{
    public GameObject gridContainer;

    Cell[] listOfCells;

    public float scaleOfCells = 0.7f;

    void Start()
    {
        gridContainer = this.gameObject;

        listOfCells = gridContainer.GetComponentsInChildren<Cell>();

        SetUpGrid();
    }

    void SetUpGrid()
    {
        foreach (Cell cell in listOfCells)
        {
            if (cell.transform.childCount > 0)
            {
                cell.isOcupied = true;

            }
        }
    }

    public Vector3 GetCellToMove(Vector3 direction, Transform objectToMove)
    {
        Vector3 newPosi =new Vector3(-100,-100,-100);

        Transform cellParent = objectToMove.parent;


        foreach (Cell cell in listOfCells)
        {
            //Debug.Log("Object position: " + objectLocalPosition + " Cell position" + cell.transform.localPosition);
            if(cell.transform.localPosition.x == (cellParent.localPosition.x+direction.x) && cell.transform.localPosition.z == (cellParent.localPosition.z +direction.z))
            {
                if (cell.transform.childCount == 0)
                {
                    objectToMove.transform.parent = cell.transform;
                    return newPosi = cell.transform.position;
                }
            }
        }
        return newPosi;
    }

    IEnumerator MoveCube(Transform pushableObject, Vector3 targetPosition)
    {

        float step = Time.deltaTime * 2;

        while (pushableObject.position != targetPosition)
        {
            pushableObject.position = Vector3.MoveTowards(pushableObject.position, targetPosition, step);
            yield return null;
        }

    }
}
