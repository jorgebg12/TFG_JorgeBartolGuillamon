using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchElement : MonoBehaviour
{
    public RotationState rotationState;
    public Vector3 desiredRotation;
    public Vector3 currentRotation;

    public GameObject sphereChangeMode;
    public GameObject rotatingObject;

    public Transform leftObject;
    public Transform centralObject;
    public Transform rightObject;

    float timeToRotate = 1f;
    float rotationSpeed = 2f;
    [HideInInspector] public bool isRotating = false;

    public enum RotationState
    {
        rotateLeft,//Mode_1
        rotateRight,//Mode_2
        stay//Mode_3
    }
    private void Start()
    {
        SetMaterials();
    }
    public void RotateObject()
    {
        if (rotationState == RotationState.rotateLeft)
        {
            if(!isRotating)
                StartCoroutine(RotateLeft());
        }
        else if (rotationState == RotationState.rotateRight)
        {
            if (!isRotating)
                StartCoroutine(RotateRight());
        }
        else
        {
            if (!isRotating)
                StartCoroutine(DontRotate());
        }
    }

    void SetMaterials()
    {
        if (rotationState == RotationState.rotateLeft)
        {
            sphereChangeMode.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Rotate_Mode_1");
        }
        else if (rotationState == RotationState.rotateRight)
        {
            sphereChangeMode.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Rotate_Mode_2");
        }
        else
        {
            sphereChangeMode.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Rotate_Mode_3");
        }
    }
    public void ChangeRotationMode()
    {

        if (rotationState == RotationState.rotateLeft)
        {
            rotationState = RotationState.rotateRight;
            sphereChangeMode.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Rotate_Mode_2");
        }
        else if (rotationState == RotationState.rotateRight)
        {
            rotationState = RotationState.stay;
            sphereChangeMode.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Rotate_Mode_3");
        }
        else
        {
            rotationState = RotationState.rotateLeft;
            sphereChangeMode.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Rotate_Mode_1");
        }
        //if (rotationState == RotationState.stay)
        //    rotationState = RotationState.rotateLeft;
        //else
        //    rotationState++;
    }
    public bool CheckRotation()
    {
        //currentRotation = QuaternionAbs(rotatingObject.transform.localRotation);
        //Quaternion desired = QuaternionAbs(Quaternion.Euler(desiredRotation));
        currentRotation = rotatingObject.transform.localEulerAngles;

        //Debug.Log(this.name + " " + "current" + currentRotation + "  desired" + desiredRotation + " : " +(currentRotation.Equals(desiredRotation)));
        
        return currentRotation.z == desiredRotation.z;
    }

    #region Rotate functions
    IEnumerator RotateLeft()
    {
        isRotating = true;
        
        Quaternion desiredCenterRotation=Quaternion.Euler(centralObject.eulerAngles.x, centralObject.eulerAngles.y, centralObject.eulerAngles.z+90);
        Quaternion desiredLeftRotation = Quaternion.Euler(leftObject.eulerAngles.x, leftObject.eulerAngles.y, leftObject.eulerAngles.z + 90);

        //Debug.Log("Actual" + centralObject.eulerAngles);
        float rotationTime = 0;
        while(rotationTime <= timeToRotate)
        {
            rotationTime += Time.deltaTime;
            centralObject.rotation = Quaternion.RotateTowards(centralObject.rotation, desiredCenterRotation, rotationTime * rotationSpeed);
            leftObject.rotation =    Quaternion.RotateTowards(leftObject.rotation, desiredLeftRotation, rotationTime * rotationSpeed);
            yield return null;
        }

        centralObject.rotation = desiredCenterRotation;
        leftObject.rotation = desiredLeftRotation;

        isRotating = false;
    }
    IEnumerator RotateRight()
    {
        isRotating = true;

        Quaternion desiredCenterRotation = Quaternion.Euler(centralObject.eulerAngles.x, centralObject.eulerAngles.y, centralObject.eulerAngles.z - 90);
        Quaternion desiredRightRotation = Quaternion.Euler(rightObject.eulerAngles.x, rightObject.eulerAngles.y, rightObject.eulerAngles.z - 90);

        //Debug.Log("Actual" + centralObject.eulerAngles);
        float rotationTime = 0;
        while (rotationTime <= timeToRotate)
        {
            rotationTime += Time.deltaTime;
            centralObject.rotation = Quaternion.RotateTowards(centralObject.rotation, desiredCenterRotation, rotationTime * rotationSpeed);
            rightObject.rotation = Quaternion.RotateTowards(rightObject.rotation, desiredRightRotation, rotationTime * rotationSpeed);
            yield return null;
        }

        centralObject.rotation = desiredCenterRotation;
        rightObject.rotation = desiredRightRotation;

        isRotating = false;
    }
    IEnumerator DontRotate()
    {
        isRotating = true;
        yield return new WaitForSeconds(timeToRotate);
        isRotating = false;
    }
    #endregion
    Quaternion QuaternionAbs(Quaternion q)
    {
        q.w = Mathf.Abs(q.w);
        q.x = Mathf.Abs(q.x);
        q.y = Mathf.Abs(q.y);
        q.z = Mathf.Abs(q.z);

        return q;
    }
}
