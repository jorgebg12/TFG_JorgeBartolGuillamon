using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchElement : MonoBehaviour
{
    public RotationState rotationState;
    public Vector3 desiredRotation;

    public GameObject rotationModeChanger;

    public Transform leftObject;
    public Transform centralObject;
    public Transform rightObject;

    float timeToRotate = 2f;
    float rotationSpeed = 2f;
    bool isRotating = false;


    public enum RotationState
    {
        Mode1,
        Mode2,
        Mode3
    }
    public void RotateObject()
    {
        if (rotationState == RotationState.Mode1)
        {
            if(!isRotating)
                StartCoroutine(RotateLeft());
        }
        else if (rotationState == RotationState.Mode2)
        {
            if (!isRotating)
                StartCoroutine(RotateRight());
        }
    }
    void ChangeRotationMode()
    {
        if(rotationState == RotationState.Mode1)
        {
            rotationState = RotationState.Mode2;
        }
        else if (rotationState ==RotationState.Mode2)
        {
            rotationState = RotationState.Mode3;
        }
        else
        {
            rotationState = RotationState.Mode1;
        }
    }
    IEnumerator RotateLeft()
    {
        Quaternion desiredCenterRotation=Quaternion.Euler(centralObject.eulerAngles.x, centralObject.eulerAngles.y, centralObject.eulerAngles.z+90);
        Quaternion desiredLeftRotation = Quaternion.Euler(leftObject.eulerAngles.x, leftObject.eulerAngles.y, leftObject.eulerAngles.z + 90);

        Debug.Log("Actual" + centralObject.eulerAngles);
        float rotationTime = 0;
        isRotating = true;
        while(rotationTime <= timeToRotate)
        {
            rotationTime += Time.deltaTime * rotationSpeed;
            centralObject.rotation = Quaternion.Slerp(centralObject.rotation, desiredCenterRotation, rotationTime);
            leftObject.rotation = Quaternion.Slerp(leftObject.rotation, desiredLeftRotation, rotationTime);
            yield return null;
        }

        centralObject.rotation = desiredCenterRotation;
        leftObject.rotation = desiredLeftRotation;
        isRotating = false;

    }
    IEnumerator RotateRight()
    {
        yield return null;
    }
}
