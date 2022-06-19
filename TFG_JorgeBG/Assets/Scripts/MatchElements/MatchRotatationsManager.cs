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
        //playerControlls.playerInputActions.characterControls.UseObject.started += OnUseObject;
    }

    private void OnDisable()
    {
        //playerControlls.playerInputActions.characterControls.UseObject.started -= OnUseObject;
    }
    private void Awake()
    {
        playerControlls = FindObjectOfType<playerController>();
    }
    private void OnUseObject(InputAction.CallbackContext obj)
    {
        StartCoroutine(StartRotations());
    }
    IEnumerator StartRotations()
    {
        foreach(MatchElement element in puzzlePieces)
        {
            element.RotateObject();
            element.isRotating = true;
            while (element.isRotating)
                yield return null;
        }

        CheckCompleted();


    }

    void CheckCompleted()
    {
        foreach (MatchElement element in puzzlePieces)
        {
            Debug.Log(element.name + " " + element.CheckRotation());
            if (!element.CheckRotation())
            {
                return;
            }
        }
        StartCoroutine(LoadNewLevel());


    }

    IEnumerator LoadNewLevel()
    {
        yield return null;
    }
}
