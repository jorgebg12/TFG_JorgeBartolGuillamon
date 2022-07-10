using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotatingBell : MonoBehaviour
{
    playerController playerControlls; 
    CharacterController controller;

    float timeToRotate = 1f;
    float rotationSpeed = 2f;
    [HideInInspector] public bool isRotating = false;
    private void OnEnable()
    {

        playerControlls.playerInputActions.characterControls.UseObject.performed += OnUseObject;
    }

    private void OnDisable()
    {
        playerControlls.playerInputActions.characterControls.UseObject.performed -= OnUseObject;
    }
    private void Awake()
    {
        controller = FindObjectOfType<CharacterController>();
        playerControlls = FindObjectOfType<playerController>();
    }
    private void OnUseObject(InputAction.CallbackContext obj)
    {
        GetFacingObject();
    }
    private void GetFacingObject()
    {
        float bodyHeigth = controller.transform.position.y + (controller.height / 3);
        float raycastLenght = controller.radius / 1.5f;

        Vector3 startPoint = new Vector3(controller.transform.position.x, bodyHeigth, controller.transform.position.z) + (controller.transform.forward * controller.radius / 2);

        Collider[] arrayHits;
        arrayHits = Physics.OverlapSphere(startPoint, raycastLenght);

        for (int i = 0; i < arrayHits.Length; i++)
        {
            if (arrayHits[i].gameObject.layer == LayerMask.NameToLayer("push") || arrayHits[i].gameObject.layer== LayerMask.NameToLayer("staticReflector"))
            {
                StartCoroutine(RotateObject(arrayHits[i].transform));
                break;
            }
        }
    }
    IEnumerator RotateObject(Transform objectToRotate)
    {

        isRotating = true;
        EventManager.OnClearLine();
        playerControlls.playerInputActions.characterControls.Disable();


        Quaternion desiredRotation = Quaternion.Euler(objectToRotate.eulerAngles.x, objectToRotate.eulerAngles.y - 90, objectToRotate.eulerAngles.z );

        float rotationTime = 0;
        while (rotationTime <= timeToRotate)
        {
            rotationTime += Time.deltaTime;
            objectToRotate.rotation = Quaternion.RotateTowards(objectToRotate.rotation, desiredRotation, rotationTime * rotationSpeed);
            yield return null;
        }

        objectToRotate.rotation = desiredRotation;

        playerControlls.playerInputActions.characterControls.Enable();
        EventManager.OnRecalculateLine();
        isRotating = false;
    }
}
