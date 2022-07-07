using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MatchRotatationsManager : MonoBehaviour
{
    playerController playerControlls;
    bool isRotating = false;

    public MatchElement[] puzzlePieces;

    private void OnEnable()
    {
        EventManager.PressButton += OnUseObject;
    }
    private void OnDisable()
    {
        EventManager.PressButton -= OnUseObject;
    }
    private void Awake()
    {
        playerControlls = FindObjectOfType<playerController>();
    }
    private void OnUseObject()
    {
        if (!isRotating)
        {
            isRotating = true;
            StartCoroutine(StartRotations());
        }
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
        isRotating = false;

    }

    void CheckCompleted()
    {
        foreach (MatchElement element in puzzlePieces)
        {
            bool isCorrect = element.CheckRotation();
            Debug.Log(element.name + " " + isCorrect);

            if (!isCorrect)
            {
                return;
            }
        }
        Debug.Log("Completed");
        EventManager.OnCompleteLevel();


    }

}
