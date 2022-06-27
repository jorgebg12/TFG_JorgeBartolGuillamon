using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Flashlight : MonoBehaviour
{

    playerController playerControlls;

    Light lightCone;
    CapsuleCollider capsuleCollider;

    public GameObject lightConeMesh;

    Vector3 startPoint;
    Vector3 endPoint;
    float radius;
    Vector3 direction;

    private void OnEnable()
    {

        playerControlls.playerInputActions.characterControls.UseObject.performed += OnStartUse;
        playerControlls.playerInputActions.characterControls.UseObject.performed += OnUsingObject;
        playerControlls.playerInputActions.characterControls.UseObject.canceled += OnCancelUse;
    }

    private void OnDisable()
    {
        playerControlls.playerInputActions.characterControls.UseObject.performed -= OnStartUse;
        playerControlls.playerInputActions.characterControls.UseObject.performed -= OnUsingObject;
        playerControlls.playerInputActions.characterControls.UseObject.canceled -= OnCancelUse;
    }
    private void Awake()
    {
        lightCone = GetComponentInChildren<Light>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        playerControlls = FindObjectOfType<playerController>();
    }
    private void OnStartUse(InputAction.CallbackContext obj)
    {
        lightCone.enabled = true;
        capsuleCollider.enabled = true;
        //lightConeMesh.SetActive(true);
        //CastLightRaycast();
    }
    private void OnUsingObject(InputAction.CallbackContext obj)
    {
    }
    private void OnCancelUse(InputAction.CallbackContext obj)
    {
        lightCone.enabled = false;
        capsuleCollider.enabled = false;
        //lightConeMesh.SetActive(false);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "ChangeMode")
        {
            other.GetComponentInParent<MatchElement>().ChangeRotationMode();
        }
    }

}
