using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLevel : Toggleable {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void Positive()
    {
        //Raise water
    }

    public override void Negative()
    {
        //lower water
    }

    public override void Toggle()
    {
        //raise or lower water depending on current state
    }
}
