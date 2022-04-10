using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class interactions : MonoBehaviour
{
    CharacterController controller;
    playerController playerControllerScript;

    bool pushActive;
    bool movingObject;
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerControllerScript = GetComponent<playerController>();
        //controller.detectCollisions = true;

        playerControllerScript.playerInputActions.characterControls.push.started += PushObject;
        playerControllerScript.playerInputActions.characterControls.push.performed += PushObject;
        playerControllerScript.playerInputActions.characterControls.push.canceled += PushObject;

    }
    void Update()
    {
        if (movingObject)
        {

        }
    }
    private void PushObject(InputAction.CallbackContext ctx)
    {
        pushActive = ctx.ReadValueAsButton();
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        GameObject objectHit = hit.transform.gameObject;
        if (objectHit.layer == LayerMask.NameToLayer("push") && pushActive)
        {
            /*
            Rigidbody rigidbody = hit.collider.attachedRigidbody;

            Vector3 directionPush = hit.gameObject.transform.position - transform.position;
            directionPush.y = 0;
            directionPush.Normalize();

            rigidbody.AddForceAtPosition(directionPush * 5f, transform.position,ForceMode.Impulse);
            */
            
            Vector3 direction = new Vector3(playerControllerScript.movementFinal.x,0, playerControllerScript.movementFinal.z);
            objectHit.transform.Translate(direction * Time.deltaTime,Space.World);

        }
    }
    private void MoveObject(GameObject target)
    {

    }
}
