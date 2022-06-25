using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateButton : MonoBehaviour
{

    bool isPressed = false;
    public Transform redButton;

    Vector3 originalPosition;

    private void OnTriggerEnter(Collider other)
    {

        if (!isPressed && other.tag=="Player")
        {
            isPressed = true;
            originalPosition = redButton.localPosition;
            StartCoroutine(PressButton());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isPressed && other.tag == "Player")
        {
            isPressed = false;
            StartCoroutine(UnpressButton());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }


    IEnumerator PressButton()
    {
        Vector3 newPosi = new Vector3(redButton.localPosition.x, (redButton.localPosition.y - (redButton.localPosition.y-0.01f)), redButton.localPosition.z);

        EventManager.OnPressButton();

        float rotationTime = 0;
        while (rotationTime <= 1f)
        {
            rotationTime += Time.deltaTime;
            redButton.transform.localPosition = Vector3.Lerp(redButton.transform.localPosition, newPosi, rotationTime);
            yield return null;
        }
    }
    IEnumerator UnpressButton()
    {
        float rotationTime = 0;
        while (rotationTime <= 1f)
        {
            rotationTime += Time.deltaTime;
            redButton.transform.localPosition = Vector3.Lerp(redButton.transform.localPosition, originalPosition, rotationTime);
            yield return null;
        }
    }
}
