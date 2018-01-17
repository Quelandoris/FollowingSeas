using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLevel : MonoBehaviour, IToggleable {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Positive()
    {
        //Raise water
    }

    public void Negative()
    {
        //lower water
    }

    public void Toggle()
    {
        //raise or lower water depending on current state
    }
}
