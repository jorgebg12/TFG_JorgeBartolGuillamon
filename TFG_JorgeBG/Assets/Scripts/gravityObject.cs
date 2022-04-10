using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravityObject : MonoBehaviour
{

    Rigidbody rigidbody;
    public Transform point;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            rigidbody.isKinematic = false;

        }
    }

}
