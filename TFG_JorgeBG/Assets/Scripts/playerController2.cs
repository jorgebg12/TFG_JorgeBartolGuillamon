using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController2 : MonoBehaviour
{
    Animator animator;
    PlayerInputActions playerInputActions;
    Rigidbody m_Rigidbody;
    
    Quaternion m_Rotation = Quaternion.identity;

    [SerializeField] private LayerMask groundMask;

    public float groundDistance = 0.2f;
    public Transform groundDetection; 

    public float speed = 2f;
    public float turnSpeed = 20f;
    public float jumpForce;

    int isWalkingHas;
    int isRunningHas;

    bool movementPressed;
    bool runPressed;
    bool onAir = false;

    public float smoothInputSpeed = .02f;

    Vector2 inputVector;
    Vector2 newPosition;
    Vector2 smoothInputVelocity;
    Vector3 newDirection;

    void Awake()
    {
        animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();

        playerInputActions = new PlayerInputActions();

        playerInputActions.characterControls.Enable();

        playerInputActions.characterControls.Run.performed += Run;
        playerInputActions.characterControls.Run.canceled += stop;

        playerInputActions.characterControls.jump.performed += initJump;
    }

    private void initJump(InputAction.CallbackContext context)
    {
        if(touchGround())
            onAir = true;
    }

    private void stop(InputAction.CallbackContext context)
    {
        Debug.Log("canceled");
        runPressed = false;
        speed /= 2f;
    }

    public void Run(InputAction.CallbackContext context)
    {
        Debug.Log("performed");
        runPressed = true;
        speed *= 2f;
    }

    void Start()
    {
        isWalkingHas = Animator.StringToHash("startWalk");
        isRunningHas = Animator.StringToHash("startRun");
    }

    void FixedUpdate()
    {
        movement();
        rotation();
    }
    void movement()
    {
        bool isRunning = animator.GetBool(isRunningHas);
        bool isWalking = animator.GetBool(isWalkingHas);

        //keyboard input
        inputVector = playerInputActions.characterControls.Movement.ReadValue<Vector2>();
        //
        movementPressed = inputVector.x != 0 || inputVector.y != 0;
        //smooth the input
        newPosition = Vector2.SmoothDamp(newPosition, inputVector, ref smoothInputVelocity, smoothInputSpeed);
        //to vector 3
        newDirection = new Vector3(newPosition.x, 0, newPosition.y);
        //Apply
        m_Rigidbody.MovePosition(m_Rigidbody.position + newDirection * Time.deltaTime * speed);

        if (onAir) {
            m_Rigidbody.AddForce(Vector3.up * 2 * 9.8f * jumpForce);
            onAir = false;
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
    private bool touchGround()
    {
        //Check if the player is touching ground
        return Physics.CheckSphere(groundDetection.position, groundDistance, groundMask); ;
    }
    void rotation()
    {
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, newDirection, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);
        m_Rigidbody.MoveRotation(m_Rotation);
    }
}
