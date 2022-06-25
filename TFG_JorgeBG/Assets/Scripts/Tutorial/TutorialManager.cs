using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public GameObject cell;
    public GameObject movingObject;


    playerController playerControllerScript;

    public bool objectOnPlace = false;

    private void OnEnable()
    {
        playerControllerScript = FindObjectOfType<playerController>();
        EventManager.CompleteLevel += StartNextLevel;
    }

    private void OnDisable()
    {
        EventManager.CompleteLevel -= StartNextLevel;
    }
    void Update()
    {
        if(movingObject.transform.parent == cell.transform && !objectOnPlace)
        {
            objectOnPlace = true;
            movingObject.layer = 0;
            SetUpKey();
        }
    }
    private void SetUpKey()
    {
        GameObject keyPrefab = Resources.Load<GameObject>("Key");
        GameObject key = Instantiate(keyPrefab, playerControllerScript.hand);
    }

    void StartNextLevel()
    {

    }
}
