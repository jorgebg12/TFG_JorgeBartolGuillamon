using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{/*
    Animator animator;

    int isWalkingHas;
    int isRunningHas;

    PlayerInput input;

    Vector2 currentMovement;
    bool movementPressed;
    bool runPressed;

    Vector3 newPosition;

    void Awake()
    {
        input = new PlayerInput();
        input.characterControls.Movement.performed += ctx => {
            currentMovement = ctx.ReadValue<Vector2>();
            movementPressed = currentMovement.x != 0 || currentMovement.y != 0;
        };
        input.characterControls.Run.performed += ctx => runPressed = ctx.ReadValueAsButton();
    }
    void Start()
    {
        animator = GetComponent<Animator>();

        isWalkingHas = Animator.StringToHash("isWalking");
        isRunningHas = Animator.StringToHash("isRunning");
    }
    void Update()
    {
        movement();
        rotation();
    }

    private void OnMovement(InputValue value)
    {
        Vector2 inputMovimiento = value.Get<Vector2>();
        newPosition = new Vector3(inputMovimiento.x, 0, inputMovimiento.y);
    }

    void movement()
    {
        bool isRunning = animator.GetBool(isRunningHas);
        bool isWalking= animator.GetBool(isWalkingHas);

        if(movementPressed && !isWalking)
        {
            animator.SetBool(isWalkingHas, true);
        }
        if (!movementPressed && isWalking)
        {
            animator.SetBool(isWalkingHas, false);
        }
        if ((movementPressed && runPressed) && !isRunning)
        {
            animator.SetBool(isRunningHas, true);
        }
        if ((!movementPressed || !runPressed) && isRunning)
        {
            animator.SetBool(isRunningHas, false);
        }
    }
    void rotation()
    {
        Vector3 currentPosition = transform.position;
        Vector3 newPos = new Vector3(currentMovement.x, 0, currentMovement.y);
        Vector3 positionToLook = currentPosition + newPos;
        transform.LookAt(positionToLook);
    }

    void OnEnable()
    {
        input.characterControls.Enable();
    }
    void OnDisable()
    {
        input.characterControls.Disable();
    }*/
}
