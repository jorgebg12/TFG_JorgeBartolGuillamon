using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravityObject : MonoBehaviour
{
    BoxCollider boxCollider;
    Rigidbody rigidbody;
    float colliderHeigth;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        rigidbody = GetComponent<Rigidbody>();
        colliderHeigth = boxCollider.size.y;
    }

    void Update()
    {
        //DetectGround();
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.layer == 6)
        {
            //rigidbody.isKinematic = true;

        }
    }

    private void DetectGround()
    {
        Vector3 downPoint = new Vector3(transform.position.x, transform.position.y - colliderHeigth/2, transform.position.z);

        Debug.DrawRay(downPoint, Vector3.down,Color.red);
        //!Physics.Raycast(downPoint, Vector3.down, 0.01f, 6)

        bool test = Physics.BoxCast(boxCollider.center, boxCollider.size, Vector3.down, Quaternion.identity, 1f, 6);
        Debug.Log(test);
        if (!Physics.BoxCast(boxCollider.center,boxCollider.size,Vector3.down,Quaternion.identity,0.1f,6))
        {
            transform.position -= new Vector3(0, 1, 0) * Time.deltaTime;
        }
        else
            Debug.Log("Toca");
    }

}
