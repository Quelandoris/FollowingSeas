using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrackWind))]
public class WindPush : MonoBehaviour {
    TrackWind windScript;
    Rigidbody myRB;

	void Start () {
        windScript = GetComponent<TrackWind>();
        myRB = GetComponentInParent<Rigidbody>();
	}
	
    private void FixedUpdate()
    {
        myRB.AddForce(windScript.GetWind());
    }
}
