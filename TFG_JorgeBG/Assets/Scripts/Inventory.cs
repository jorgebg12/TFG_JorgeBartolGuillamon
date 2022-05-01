using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Inventory : MonoBehaviour
{
    playerController playerControllerScript;

    GameObject equipedItem;
    Transform handPosition;

    public UnityEngine.UI.Image firstObject_UI;
    public UnityEngine.UI.Image secondObject_UI;


    private void Awake()
    {
        playerControllerScript = GetComponent<playerController>();

        handPosition = GameObject.Find("mixamorig:RightHand").transform;

        firstObject_UI = GameObject.Find("Slot_1").GetComponent<UnityEngine.UI.Image>();
        secondObject_UI = GameObject.Find("Slot_2").GetComponent<UnityEngine.UI.Image>();

        playerControllerScript.playerInputActions.characterControls.Objects.started += EquipObject;

        playerControllerScript.playerInputActions.characterControls.Objects.canceled += QuitObject;

    }
    private void EquipObject(InputAction.CallbackContext ctx)
    {
        if (ctx.control.name == "1")
        {
            firstObject_UI.color = Color.white;
            secondObject_UI.color = Color.black;

            Destroy(equipedItem);
            equipedItem = GameObject.Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cylinder), handPosition, this);
        }
        else
        {
            firstObject_UI.color = Color.black;
            secondObject_UI.color = Color.white;
        }
    }

    private void QuitObject(InputAction.CallbackContext ctx)
    {

    }


    // Update is called once per frame
    void Update()
    {

    }
}
