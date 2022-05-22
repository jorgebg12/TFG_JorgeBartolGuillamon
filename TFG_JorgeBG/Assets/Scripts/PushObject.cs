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

    float moveDistance = 1;
    int pushLayer;

    Transform pushableObject;
    BoxCollider colliderObject;
    Vector3 targetPosition;
    Vector3 direction;
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerControllerScript = GetComponent<playerController>();
        customGrid = FindObjectOfType<CustomGrid>();

        controller.detectCollisions = false;
        pushLayer = LayerMask.NameToLayer("push");

        playerControllerScript.playerInputActions.characterControls.push.started += StartPush;
        playerControllerScript.playerInputActions.characterControls.push.performed += PerformPush;

    }
    private void Update()
    {

    }
    // Debug raycast hit in front of the player
    void DrawRay(Vector3 startPoint, Vector3 endPoint)
    {
        Debug.DrawLine(startPoint, endPoint, Color.blue, 5);

        //Debug.Log("Start point: " + startPoint + " End Point: " + endPoint);
        //GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //sphere.transform.position = startPoint;
        //sphere.GetComponent<Collider>().isTrigger = true;
        //sphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        //GameObject sphere1 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //sphere1.transform.position = endPoint;
        //sphere1.GetComponent<Collider>().isTrigger = true;
        //sphere1.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }
    //Click of the push action bind
    private void StartPush(InputAction.CallbackContext ctx)
    {
        pushActive = ctx.ReadValueAsButton();
        GetFacingObject2();
    }
    private void PerformPush(InputAction.CallbackContext ctx)
    {
        //Move cube
        if (pushableObject != null)
        {
            StartMovement(playerControllerScript.movementInput);
        }
    }
    
    private void StartMovement(Vector2 playerInput)
    {
        if (playerInput != Vector2.zero)
        {

            GetPushDirection();
            targetPosition = customGrid.GetCellToMove(direction, pushableObject);

            if (targetPosition != new Vector3(-100,-100,-100) && movingObject == false)
            {
                playerControllerScript.playerInputActions.characterControls.Disable();
                movingObject = true;
                StartCoroutine(MoveCube());
            }
        }
    }

    private Vector3 GetPushDirection()
    {
        direction = Vector3.zero;

        float xInput = playerControllerScript.movementInput.x;
        float yInput = playerControllerScript.movementInput.y;

        if (xInput > 0) // X positive
        {
            if (yInput > 0)//Top_right
            {
                return direction = new Vector3(0, 0, moveDistance);
            }
            else //Down_right
                return direction = new Vector3(moveDistance, 0, 0);
        }
        else // X negative
        {
            if (yInput > 0)//Top_right
            {
                return direction = new Vector3(-moveDistance, 0, 0);
            }
            else //Down_right
                return direction = new Vector3(0, 0, -moveDistance);
        }
    }
    //private void Movement()
    //{
    //    float step = Time.deltaTime * 2;

    //    pushableObject.position = Vector3.MoveTowards(pushableObject.position, targetPosition, step);

    //    if (pushableObject.position == targetPosition)
    //    {
    //        playerControllerScript.playerInputActions.characterControls.Enable();
    //        movingObject = false;
    //    }
    //    //else if (Physics.Raycast(pushableObject.position, direction, (colliderObject.size.x/2)+colliderObject.contactOffset))
    //    //{
    //    //    playerControllerScript.playerInputActions.characterControls.Enable();
    //    //    movingObject = false;
    //    //}
    //}

    IEnumerator MoveCube() 
    {

        playerControllerScript.playerInputActions.characterControls.Disable();
        targetPosition = new Vector3(targetPosition.x, pushableObject.transform.position.y, targetPosition.z);
        float step = Time.deltaTime * 2;
        while (pushableObject.position != targetPosition)
        {
            pushableObject.position = Vector3.MoveTowards(pushableObject.position, targetPosition, step);
            yield return null;
        }


        movingObject = false;

        playerControllerScript.playerInputActions.characterControls.Enable();
    }
    private void GetFacingObject()
    {
        float bodyHeigth = transform.position.y + controller.height /2;
        float raycastLenght = controller.radius;

        Debug.Log(raycastLenght);
        Vector3 startPoint = new Vector3(transform.position.x, bodyHeigth, transform.position.z) + (transform.forward * controller.radius);
        Vector3 endPoint = startPoint + transform.forward;

        Collider[] arrayHits;
        arrayHits = Physics.OverlapSphere(startPoint, raycastLenght);

        for (int i = 0; i < arrayHits.Length; i++)
        {

            Debug.Log(arrayHits[i].transform.name);
            if (arrayHits[i].gameObject.layer == pushLayer)
            {
                Debug.Log(arrayHits[i].transform.name);
                pushableObject = arrayHits[i].transform;
                colliderObject = pushableObject.GetComponent<BoxCollider>();

                Debug.Log(pushableObject.name);
                break;
            }
        }
        targetPosition = Vector3.zero;
    }

    private void GetFacingObject2()
    {
        float bodyHeigth = transform.position.y + controller.height / 3;
        float raycastLenght = controller.radius ;
        
        Vector3 startPoint = new Vector3(transform.position.x, bodyHeigth, transform.position.z);

        RaycastHit[] arrayHits;
        
        arrayHits = Physics.RaycastAll(startPoint, transform.forward,raycastLenght);

        for (int i = 0; i < arrayHits.Length; i++)
        {

            if (arrayHits[i].transform.gameObject.layer == pushLayer)
            {
                pushableObject = arrayHits[i].transform;
                break;
            }
        }
    }
}
