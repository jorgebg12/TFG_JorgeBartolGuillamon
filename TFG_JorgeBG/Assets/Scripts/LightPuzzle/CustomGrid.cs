using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGrid : MonoBehaviour
{
    public GameObject gridContainer;
    List<Transform> listOfCells;

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
    public Vector3 GetCellToMove(Vector2 direction, Transform objectToMove)
    {
        Vector3 newPosi =new Vector3(-100,-100,-100);

        Transform cellParent = objectToMove.parent;

        int xDir;
        int zDir;
        //if(direction.x > 0 && direction.z > 0)
        //{
        //    xDir = 0;
        //    zDir = 1;
        //}
        //else if (direction.x > 0 && direction.z < 0)
        //{
        //    xDir = 1;
        //    zDir = 0;
        //}
        //else if (direction.x < 0 && direction.z > 0)
        //{
        //    xDir = -1;
        //    zDir = 0;
        //}
        //else if (direction.x < 0 && direction.z < 0)
        //{
        //    xDir = 0;
        //    zDir = -1;
        //}
        //else
        //{
        //    xDir = 0;
        //    zDir = 0;
        //}
        if(direction.x > 0 && direction.y > 0)
        {
            xDir = 0;
            zDir = 1;
        }
        else if (direction.x > 0 && direction.y < 0)
        {
            xDir = 1;
            zDir = 0;
        }
        else if (direction.x < 0 && direction.y > 0)
        {
            xDir = -1;
            zDir = 0;
        }
        else if (direction.x < 0 && direction.y < 0)
        {
            xDir = 0;
            zDir = -1;
        }
        else
        {
            xDir = 0;
            zDir = 0;
        }
        //Debug.Log("xdir: " + xDir + " zdir" + zDir);
        Vector3 desiredCell = new Vector3(cellParent.localPosition.x + xDir, cellParent.localPosition.y, cellParent.localPosition.z + zDir);

        foreach (Transform cell in listOfCells)
        {
            //Debug.Log("Object position: " + objectLocalPosition + " Cell position" + cell.transform.localPosition);
            //if(cell.localPosition.x == (cellParent.localPosition.x + xDir) && cell.localPosition.z == (cellParent.localPosition.z + zDir))
            if(cell.localPosition == desiredCell)
            {
                //Debug.Log("Found: " + cell.childCount + "Name: " + cell.name);
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
