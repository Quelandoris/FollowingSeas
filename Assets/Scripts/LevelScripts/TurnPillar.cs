using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPillar : MonoBehaviour {
    Rigidbody myRB;
    bool solved;

	// Use this for initialization
	void Start () {
        myRB = GetComponentInParent<Rigidbody>();
	}

    void FixedUpdate()
    {
        float angle = (transform.localRotation.y + 360) % 90;
        if(angle <= 45)
        {
            myRB.AddTorque(Vector3.down * 3);
        }
        else
        {
            myRB.AddTorque(Vector3.up * 3);
        }

        if(Vector3.Angle(transform.parent.forward, transform.forward) <= 15)
        {
            solved = true;
        }
        else
        {
            solved = false;
        }
    }

    public bool GetSolved()
    {
        return solved;
    }
}
