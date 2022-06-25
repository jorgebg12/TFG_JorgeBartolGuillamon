using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Key : MonoBehaviour
{
    playerController playerControllerScript;
    DoorDetection doorDetection;
    public bool equiped = false;
    void Awake()
    {
        playerControllerScript = FindObjectOfType<playerController>();
        doorDetection = FindObjectOfType<DoorDetection>();
    }

    private void OnEnable()
    {
        playerControllerScript.playerInputActions.characterControls.UseObject.started += UseKey;
    }

    private void OnDisable()
    {
        playerControllerScript.playerInputActions.characterControls.UseObject.started -= UseKey;

    }
    private void UseKey(InputAction.CallbackContext context)
    {
        if (doorDetection.inFront)
        {
            Debug.Log("Complete level");
            EventManager.OnCompleteLevel();
        }
    }
}
