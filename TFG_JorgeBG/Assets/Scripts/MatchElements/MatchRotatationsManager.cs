using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MatchRotatationsManager : MonoBehaviour
{
    playerController playerControlls;

    public MatchElement[] puzzlePieces;

    private void OnEnable()
    {
        playerControlls.playerInputActions.characterControls.UseObject.started += OnUseObject;
    }

    private void OnDisable()
    {
        playerControlls.playerInputActions.characterControls.UseObject.started -= OnUseObject;
    }
    private void Awake()
    {
        playerControlls = FindObjectOfType<playerController>();
    }
    private void OnUseObject(InputAction.CallbackContext obj)
    {
        puzzlePieces[0].RotateObject();
    }
}
