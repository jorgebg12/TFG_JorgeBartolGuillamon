using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PushObject : MonoBehaviour
{
    CharacterController controller;
    playerController playerControllerScript;

    bool pushActive;
    bool movingObject = false;

    int dragForce = 1;
    int pushLayer;

    Transform pushableObject;
    Vector3 targetPosition;
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerControllerScript = GetComponent<playerController>();
        controller.detectCollisions = false;
        pushLayer = LayerMask.NameToLayer("push");

        playerControllerScript.playerInputActions.characterControls.push.started += StartPush;
        playerControllerScript.playerInputActions.characterControls.push.performed += PerformPush;
        //playerControllerScript.playerInputActions.characterControls.push.canceled += CancelPush;

    }

    void Update()
    {
        //Debug.Log("angle " + Vector3.Angle(transform.forward, pushableObject.position- transform.position));
        if (movingObject)
        {
            float step = Time.deltaTime;
            Debug.Log(pushableObject.position + " " + targetPosition);
            pushableObject.position = Vector3.MoveTowards(pushableObject.position, targetPosition, step);

            if (pushableObject.position == targetPosition)
            {
                playerControllerScript.playerInputActions.characterControls.Enable();
                movingObject = false;

            }
        }
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
        GetFacingObject();
    }
    private void PerformPush(InputAction.CallbackContext ctx)
    {
        //Move cube
        if (pushableObject != null)
        {
            StartMovement(playerControllerScript.movementInput);
        }
    }
    private void CancelPush(InputAction.CallbackContext ctx)
    {
        pushActive = ctx.ReadValueAsButton();
        pushableObject = null;
    }
    //private void OnControllerColliderHit(ControllerColliderHit hit)
    //{
    //    GameObject objectHit = hit.transform.gameObject;

    //    if (objectHit.layer == pushLayer)
    //    {
    //        if (Vector3.Angle(transform.forward, objectHit.transform.position - transform.position) < 40f)
    //        {
    //            if (pushActive)
    //            {

    //                //Rigidbody rigidbody = hit.collider.attachedRigidbody;

    //                //Vector3 directionPush = hit.gameObject.transform.position - transform.position;
    //                //directionPush.y = 0;
    //                //directionPush.Normalize();

    //                //rigidbody.AddForceAtPosition(directionPush * 5f, transform.position,ForceMode.Impulse);

    //                MoveObjectWithoutRigidbody(objectHit);


    //            }
    //        }
    //    }
    //}
    private Vector3 GetPushDirection(Transform target)
    {
        Vector3 direction;

        float xInput = playerControllerScript.movementInput.x;
        float yInput = playerControllerScript.movementInput.y;

        if (xInput > 0)
        {
            if (yInput > 0)//Top_right
            {
                direction = new Vector3(0, 0, -dragForce);
            }
            else //Down_right
                direction = new Vector3(-dragForce, 0, 0);
        }
        else
        {
            if (yInput > 0)//Top_right
            {
                direction = new Vector3(dragForce, 0, 0);
            }
            else //Down_right
                direction = new Vector3(0, 0, dragForce);
        }
        //Debug.Log(direction);
        //Debug.Log(direction+ target.position);
        return direction + target.position;
    }

    private void StartMovement(Vector2 playerInput)
    {
        if (playerInput != Vector2.zero)
        {
            targetPosition = GetPushDirection(pushableObject);
            movingObject = true;
            playerControllerScript.playerInputActions.characterControls.Disable();
        }
    }
    private void GetFacingObject()
    {
        float bodyHeigth = transform.position.y + controller.height / 2;
        float raycastLenght = controller.radius / 2;

        Vector3 startPoint = new Vector3(transform.position.x, bodyHeigth, transform.position.z) + (transform.forward * controller.radius);
        Vector3 endPoint = startPoint + transform.forward;

        Collider[] arrayHits;
        arrayHits = Physics.OverlapSphere(startPoint, raycastLenght);

        for (int i = 0; i < arrayHits.Length; i++)
        {
            if (arrayHits[i].gameObject.layer == pushLayer)
            {
                Debug.Log(arrayHits[i].transform.name);
                pushableObject = arrayHits[i].transform;
                break;
            }
        }
    }
}
