using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Actions { TOGGLE, POSITIVE, NEGATIVE}

public class Button : MonoBehaviour, IHarpoonable {

    public IToggleable objectToAffect;
    public Actions actionType;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnHarpoon()
    {
        //activate button
    }
}
