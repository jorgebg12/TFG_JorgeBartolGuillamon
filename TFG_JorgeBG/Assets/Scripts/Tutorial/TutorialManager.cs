using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject cell;
    public GameObject movingObject;


    playerController playerControllerScript;

    public bool objectOnPlace = false;

    private void Start()
    {
        playerControllerScript = FindObjectOfType<playerController>();
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

}
