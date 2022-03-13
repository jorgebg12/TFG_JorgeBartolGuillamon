using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    Animator animator;
    PlayerInputActions playerInputActions;
    CharacterController characterController;

    public float speed = 2f;
    public float turnSpeed = 10f;
    public float jumpForce;

    int isWalkingHas;
    int isRunningHas;

    bool movementPressed;
    bool runPressed;

    public float smoothInputSpeed = .02f;

    Vector2 movementInput;
    Vector3 movementConverted;

    void Awake()
    {
        animator = GetComponent<Animator>();
        playerInputActions = new PlayerInputActions();
        characterController = GetComponent<CharacterController>();

        playerInputActions.characterControls.Movement.started += OnMove;
        playerInputActions.characterControls.Movement.performed += OnMove;
        playerInputActions.characterControls.Movement.canceled += OnMove;

        playerInputActions.characterControls.Run.performed += Run;
        playerInputActions.characterControls.Run.canceled += stopRun;

        playerInputActions.characterControls.jump.performed += initJump;
    }
    private void OnMove(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();
        movementConverted.x = movementInput.x;
        movementConverted.z = movementInput.y;
        movementPressed = movementInput.x != 0 || movementInput.y != 0;
    }
    private void initJump(InputAction.CallbackContext context)
    {
    }

    private void stopRun(InputAction.CallbackContext context)
    {
        runPressed = false;
        speed /= 2f;
    }

    public void Run(InputAction.CallbackContext context)
    {
        runPressed = true;
        speed *= 2f;
    }

    void Start()
    {
        isWalkingHas = Animator.StringToHash("startWalk");
        isRunningHas = Animator.StringToHash("startRun");
    }

    void Update()
    {
        movement();
        rotation();
    }
    void movement()
    {
        bool isRunning = animator.GetBool(isRunningHas);
        bool isWalking = animator.GetBool(isWalkingHas);

        //
        characterController.Move(movementConverted * Time.deltaTime * speed);
        //
        if (characterController.isGrounded)
        {
            float groundedGravity = -.05f;
            movementConverted.y = groundedGravity;
        }
        else
        {
            float gravity = -9.8f;
            movementConverted.y += gravity;
        }

        //Detect animation to play
        if (movementPressed && !isWalking)
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
        Vector3 positionToLook = new Vector3(movementConverted.x,0,movementConverted.z);
        Quaternion currentRotation = transform.rotation;

        if (movementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLook);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }

    void OnEnable()
    {
        playerInputActions.characterControls.Enable();
    }
    void OnDisable()
    {
        playerInputActions.characterControls.Disable();
    }
}
