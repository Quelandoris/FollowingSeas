using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpoon : MonoBehaviour
{
    Rigidbody rb;
    public float thrust = 500;
    //private bool haveJoint = false;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * thrust);
    }

    public void OnTriggerEnter(Collider other)
    {

        HarpoonHit(other.gameObject);

    }
   public void OnCollisionEnter(Collision other)
    {

        /* if (other.gameObject.GetComponent<Rigidbody>() != null && !haveJoint)
         {
             FixedJoint joint = gameObject.AddComponent<FixedJoint>();
             joint.connectedBody = other.rigidbody;
             joint.breakForce = 2000f;
             joint.breakTorque = 2000f;
             haveJoint = true;
             rb.useGravity = true;
         }*/
        HarpoonHit(other.gameObject);
    }

    void HarpoonHit(GameObject hit)
    {
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.detectCollisions = false;
        transform.parent = hit.transform;

        if (hit.CompareTag("Harpoon"))
        {
            try
            {
                hit.GetComponentInParent<IHarpoonable>().OnHarpoon();
            }
            catch
            {
                Debug.Log(hit);
                Debug.Log("Object does not have a harpoonable script but is tagged as harpoonable");
            }
        }
    }
}
