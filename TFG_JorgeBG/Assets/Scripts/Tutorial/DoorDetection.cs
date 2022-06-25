using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDetection : MonoBehaviour
{
    playerController playerControllerScript;
    public bool inFront = false;

    void Start()
    {
        playerControllerScript = FindObjectOfType<playerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            inFront = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inFront = false;
        }
    }
}
