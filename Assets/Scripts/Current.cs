using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Current : MonoBehaviour {
    List<Rigidbody> affectedObjects;
    public GameObject arrow;
    public float force;
    public Vector3 direction;

    void Start()
    {
        if(arrow != null)
        {
            direction = arrow.transform.forward;
            Destroy(arrow);
        }
        GetComponent<MeshRenderer>().enabled = false;
    }

    void FixedUpdate()
    {
        if (affectedObjects == null)
        {
            affectedObjects = new List<Rigidbody>();
        }
        if(affectedObjects.Count > 0)
        {
            foreach (Rigidbody rb in affectedObjects)
            {
                rb.AddForce(direction.normalized * force);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (affectedObjects == null)
        {
            affectedObjects = new List<Rigidbody>();
        }
        Rigidbody otherBody = other.GetComponent<Rigidbody>();
        if (otherBody != null)
        {
            affectedObjects.Add(otherBody);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody otherBody = other.GetComponent<Rigidbody>();
        if (otherBody != null)
        {
            if (affectedObjects.Contains(otherBody))
            {
                affectedObjects.Remove(otherBody);
            }
        }
    }
}
