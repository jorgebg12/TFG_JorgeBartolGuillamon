using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PushObject : MonoBehaviour
{
    CharacterController controller;
    playerController playerControllerScript;
    CustomGrid customGrid;

    bool pushActive;
    bool movingObject = false;

    int pushLayer;

    Transform pushableObject;
    BoxCollider colliderObject;
    Vector3 targetPosition;
    Vector3 direction;

    Vector3 gizmoSpherePosition;
    float gizmoSphereRadius;

    private void Awake()
    {
        controller = FindObjectOfType<CharacterController>();
        playerControllerScript = FindObjectOfType<playerController>();
        customGrid = FindObjectOfType<CustomGrid>();

        controller.detectCollisions = false;
        pushLayer = LayerMask.NameToLayer("push");
    }
    private void OnEnable()
    {
        playerControllerScript.playerInputActions.characterControls.push.started += StartPush;
        playerControllerScript.playerInputActions.characterControls.push.performed += PerformPush;
    }
    private void OnDisable()
    {
        playerControllerScript.playerInputActions.characterControls.push.started -= StartPush;
        playerControllerScript.playerInputActions.characterControls.push.performed -= PerformPush;
    }
    private void StartPush(InputAction.CallbackContext ctx)
    {
        pushActive = ctx.ReadValueAsButton();
        GetFacingObject2();
    }
    private void PerformPush(InputAction.CallbackContext ctx)
    {
        //Move cube
        if (pushableObject != null && pushActive)
        {
            StartCoroutine(StartCorroutineMovement());
            //StartMovement(playerControllerScript.movementInput);
        }
    }
    private void StartMovement(Vector2 playerInput)
    {
        if (playerInput != Vector2.zero)
        {

            EventManager.OnClearLine();
            GetPushDirection(playerInput);
            Debug.Log("Direction: " + direction);
            targetPosition = customGrid.GetCellToMove(direction, pushableObject);

            if (targetPosition != new Vector3(-100,-100,-100) && movingObject == false)
            {
                playerControllerScript.playerInputActions.characterControls.Disable();
                movingObject = true;
                StartCoroutine(MoveCube());
            }
        }
    }
    IEnumerator StartCorroutineMovement()
    {

        Vector2 input = playerControllerScript.movementInput;
        while (input.x ==0 || input.y == 0)
        {
            
            input = playerControllerScript.movementInput;
            yield return null;
        }
        EventManager.OnClearLine();
        GetPushDirection(input);
        targetPosition = customGrid.GetCellToMove(direction, pushableObject);

        if (targetPosition != new Vector3(-100,-100,-100) && movingObject == false)
        {
           playerControllerScript.playerInputActions.characterControls.Disable();
           movingObject = true;
           StartCoroutine(MoveCube());
        }
        
    }
    private Vector3 GetPushDirection(Vector2 input)
    {
        direction = Vector3.zero;

        //float xInput = playerControllerScript.movementInput.x;
        //float yInput = playerControllerScript.movementInput.y;
        float xInput = input.x;
        float yInput = input.y;

        //Debug.Log(xInput + " " + yInput);

        if (xInput > 0) // X positive
        {
            if (yInput > 0)//Top_right
            {
                return direction = new Vector3(0, 0, 1);
            }
            else //Down_right
                return direction = new Vector3(1, 0, 0);
        }
        else // X negative
        {
            if (yInput > 0)//Top_right
            {
                return direction = new Vector3(-1, 0, 0);
            }
            else //Down_right
                return direction = new Vector3(0, 0, -1);
        }
    }
    IEnumerator MoveCube() 
    {
        playerControllerScript.playerInputActions.characterControls.Disable();
        playerControllerScript.gameObject.transform.SetParent(pushableObject.transform);
        playerControllerScript.characterController.enabled = false;

        targetPosition = new Vector3(targetPosition.x, pushableObject.transform.position.y, targetPosition.z);
        float step = Time.deltaTime * 2;
        while (pushableObject.position != targetPosition)
        {
            pushableObject.position = Vector3.MoveTowards(pushableObject.position, targetPosition, step);
            yield return null;
        }

        EventManager.OnRecalculateLine();
        movingObject = false;
        pushableObject = null;

        playerControllerScript.characterController.enabled = true;
        playerControllerScript.gameObject.transform.SetParent(null);
        playerControllerScript.playerInputActions.characterControls.Enable();



    }
    private void GetFacingObject2()
    {
        float bodyHeigth = controller.transform.position.y + (controller.height /3);
        float raycastLenght = controller.radius/1.5f;

        Vector3 startPoint = new Vector3(controller.transform.position.x, bodyHeigth, controller.transform.position.z) + (controller.transform.forward * controller.radius/2);

        

        Collider[] arrayHits;
        arrayHits = Physics.OverlapSphere(startPoint, raycastLenght);
        
        for (int i = 0; i < arrayHits.Length; i++)
        {
            //Debug.Log(arrayHits[i].transform.name);
            if (arrayHits[i].gameObject.layer == pushLayer)
            {
                //Debug.Log(arrayHits[i].transform.name);
                pushableObject = arrayHits[i].transform;
                colliderObject = pushableObject.GetComponent<BoxCollider>();

                //Debug.Log(pushableObject.name);
                break;
            }
        }
        targetPosition = Vector3.zero;
    }

    //OLD
    private void GetFacingObject()
    {
        float bodyHeigth = controller.transform.position.y + controller.height/3;
        float raycastLenght = controller.radius/2 ;
        
        Vector3 startPoint = new Vector3(controller.transform.position.x, bodyHeigth, controller.transform.position.z);

        RaycastHit[] arrayHits;
        
        arrayHits = Physics.RaycastAll(startPoint, controller.transform.forward, raycastLenght);

        for (int i = 0; i < arrayHits.Length; i++)
        {

            if (arrayHits[i].transform.gameObject.layer == pushLayer)
            {
                pushableObject = arrayHits[i].transform;
                break;
            }
        }
    }
    private void OnDrawGizmos()
    {
        //Gizmos.DrawSphere(gizmoSpherePosition,gizmoSphereRadius);
    }
}
