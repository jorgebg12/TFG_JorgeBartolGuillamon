using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class TutorialManager : MonoBehaviour
{
    public GameObject cell;
    public GameObject movingObject;


    playerController playerControllerScript;
    PushObject pushObject;

    public bool objectOnPlace = false;

    public Text tutorialText;
    CanvasGroup transitionCanvasGroup;
    public GameObject imageObject;

    bool movementPressed = false;
    bool jumpPressed = false;
    bool pushPressed = false;

    private void Awake()
    {
        playerControllerScript = FindObjectOfType<playerController>();
        pushObject = FindObjectOfType<PushObject>();
        transitionCanvasGroup = FindObjectOfType<CanvasGroup>();
    }
    private void OnEnable()
    {
        playerControllerScript.playerInputActions.characterControls.Disable();
        playerControllerScript.playerInputActions.characterControls.Movement.Enable();
        playerControllerScript.playerInputActions.characterControls.PauseMenu.Enable();

        playerControllerScript.playerInputActions.characterControls.Movement.started += DetectMovement;
        playerControllerScript.playerInputActions.characterControls.jump.started += DetectJump;
    }
    private void OnDisable()
    {
        playerControllerScript.playerInputActions.characterControls.Disable();
    }
    private void DetectMovement(InputAction.CallbackContext obj)
    {
        if (!movementPressed)
        {
            movementPressed = true;
            playerControllerScript.playerInputActions.characterControls.jump.Enable();
            playerControllerScript.playerInputActions.characterControls.Run.Enable();
        }
    }
    private void DetectJump(InputAction.CallbackContext obj)
    {
        if (!jumpPressed)
        {
            jumpPressed = true;
            playerControllerScript.playerInputActions.characterControls.push.Enable();
            pushObject.enabled = true;

        }
    }
    void Update()
    {
        if(movingObject.transform.parent == cell.transform && !objectOnPlace)
        {
            objectOnPlace = true;
            movingObject.layer = 0;
            playerControllerScript.playerInputActions.characterControls.UseObject.Enable();
            pushPressed = true;
            SetUpKey();
        }

        tutorialText.color = new Color(tutorialText.color.r, tutorialText.color.g, tutorialText.color.b, 1-transitionCanvasGroup.alpha);


        SetText();
    }
    public void SetText()
    {
        if (!movementPressed)
        {
            tutorialText.text = "Use W , A , S and D to move the character";
        }
        else if (!jumpPressed)
        {
            tutorialText.text = "Press Space to jump and Shift to run";
        }
        else if (!pushPressed)
        {
            tutorialText.text = "Use V and move the character to move objects";
        }
        else
        {
            tutorialText.text = "Press E to use the object on the door";
        }
    }
    private void SetUpKey()
    {
        imageObject.SetActive(true);
        GameObject keyPrefab = Resources.Load<GameObject>("Key");
        GameObject key = Instantiate(keyPrefab, playerControllerScript.hand);
    }




}
