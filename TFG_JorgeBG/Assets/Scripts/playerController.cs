using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-10)]
public class playerController : MonoBehaviour
{
    [HideInInspector]public Animator animator;
    [HideInInspector]public PlayerInputActions playerInputActions;
    [HideInInspector]public CharacterController characterController;
    public Transform m_camera;

    public Transform hand;

    float speed = 2f;
    float turnSpeed = 10f;

    int isWalkingHas;
    int isRunningHas;
    int isJumpingHas;

    bool movementPressed;
    bool runPressed;
    bool jumpPressed;

    float gravity = -9.8f;
    float groundedGravity = -.05f;

    bool isJumping = false;
    bool isJumpPressed = false;
    float maxJumpHeight =3f;
    float maxJumpTime =0.6f;
    float initialJumpVelocity;

    public Vector2 movementInput;
    public Vector3 movementFinal;
    Vector3 movementRunFinal;

    Vector3 cameraForward;
    Vector3 cameraRight;

    void Awake()
    {
        animator = GetComponent<Animator>();
        playerInputActions = new PlayerInputActions();
        characterController = GetComponent<CharacterController>();

        cameraForward = m_camera.forward;
        cameraRight = m_camera.right;
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward = cameraForward.normalized;
        cameraRight = cameraRight.normalized;

        isWalkingHas = Animator.StringToHash("startWalk");
        isRunningHas = Animator.StringToHash("startRun");
        isJumpingHas = Animator.StringToHash("startJump");

        setupJump();
    }
    void setupJump()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }
    private void OnMove(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();

        movementFinal = (movementInput.x * cameraRight + movementInput.y * cameraForward) * speed;
        movementRunFinal = (movementInput.x * cameraRight + movementInput.y * cameraForward) * speed * 2;

        movementPressed = movementInput.x != 0 || movementInput.y != 0;
    }
    
    public void Run(InputAction.CallbackContext context)
    {
        runPressed = context.ReadValueAsButton();
    }
    private void Jump(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
    }
    void Update()
    {
        movement();
        rotation();
        handleGravity();
        handleJump();
    }
    void movement()
    {
        bool isRunning = animator.GetBool(isRunningHas);
        bool isWalking = animator.GetBool(isWalkingHas);

        //Move
        if (runPressed){  
            characterController.Move(movementRunFinal * Time.deltaTime);
        }
        else{
            characterController.Move(movementFinal * Time.deltaTime);
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
        Vector3 positionToLook = new Vector3(movementFinal.x,0,movementFinal.z);
        Quaternion currentRotation = transform.rotation;

        if (movementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLook);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }
    void handleJump()
    {
        if(!isJumping && characterController.isGrounded && isJumpPressed)
        {
            animator.SetBool(isJumpingHas, true);
            isJumping = true;
            movementFinal.y = initialJumpVelocity * 0.5f;
            movementRunFinal.y = initialJumpVelocity * 0.5f;
        }
        else if(!isJumpPressed && isJumping && characterController.isGrounded)
        {
            isJumping = false;
        }
    }
    void handleGravity()
    {
        bool isFalling = movementFinal.y <= 0f || !isJumpPressed;
        float fallMultiplier = 2f;

        //Gravity
        if (characterController.isGrounded)
        {
            animator.SetBool(isJumpingHas, false);
            movementFinal.y = groundedGravity;
            movementRunFinal.y = groundedGravity;
        }else if (isFalling)
        {
            float previousYvelocity = movementFinal.y;
            float newYvelocity = movementFinal.y + (gravity * fallMultiplier * Time.deltaTime);
            float nextYvelocity = Mathf.Max((previousYvelocity + newYvelocity) * 0.5f, -20f);
            movementFinal.y = nextYvelocity;
            movementRunFinal.y = nextYvelocity;
        }
        else
        {
            float previousYvelocity = movementFinal.y;
            float newYvelocity = movementFinal.y + (gravity * Time.deltaTime);
            float nextYvelocity = (previousYvelocity + newYvelocity) * 0.5f;
            movementFinal.y = nextYvelocity;
            movementRunFinal.y = nextYvelocity;
        }
    }

    void OnEnable()
    {
        playerInputActions.characterControls.Enable();
        //callback movement
        playerInputActions.characterControls.Movement.started += OnMove;
        playerInputActions.characterControls.Movement.performed += OnMove;
        playerInputActions.characterControls.Movement.canceled += OnMove;
        //callback run
        playerInputActions.characterControls.Run.performed += Run;
        playerInputActions.characterControls.Run.canceled += Run;
        //calback jump
        playerInputActions.characterControls.jump.started += Jump;
        playerInputActions.characterControls.jump.canceled += Jump;
    }
    void OnDisable()
    {
        playerInputActions.characterControls.Disable();
        //callback movement
        playerInputActions.characterControls.Movement.started -= OnMove;
        playerInputActions.characterControls.Movement.performed -= OnMove;
        playerInputActions.characterControls.Movement.canceled -= OnMove;
        //callback run
        playerInputActions.characterControls.Run.performed -= Run;
        playerInputActions.characterControls.Run.canceled -= Run;
        //calback jump
        playerInputActions.characterControls.jump.started -= Jump;
        playerInputActions.characterControls.jump.canceled -= Jump;
    }
}
