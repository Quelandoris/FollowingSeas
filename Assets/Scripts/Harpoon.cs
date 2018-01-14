using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpoon : MonoBehaviour
{
    Rigidbody rb;
    public float thrust = 500;
    public float timer = 20f;
    //private bool haveJoint = false;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * thrust);
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(gameObject);
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Harpoon"))
        {
            Destroy(gameObject);
        }
        if (other.CompareTag("Grapple")|| other.CompareTag("Wall")||other.CompareTag("Crate"))
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            rb.detectCollisions = false;
            transform.parent = other.transform;
            
        }


    }
   /* public void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.GetComponent<Rigidbody>() != null && !haveJoint)
        {
            FixedJoint joint = gameObject.AddComponent<FixedJoint>();
            joint.connectedBody = other.rigidbody;
            joint.breakForce = 2000f;
            joint.breakTorque = 2000f;
            haveJoint = true;
            rb.useGravity = true;
        }
        if (other.gameObject.tag == "Harpoon")
        {
            Debug.Log("hit obj");
        }
    }*/
}
