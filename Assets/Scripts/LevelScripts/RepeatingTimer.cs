using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RepeatingTimer : MonoBehaviour {
    public Toggleable objectToAffect;
    public Actions actionType;
    public float duration;
    float progress = 0;
	
	// Update is called once per frame
	void Update () {
        progress += Time.deltaTime;
        if(progress >= duration)
        {
            progress = progress - duration;
            switch (actionType)
            {
                case Actions.TOGGLE:
                    objectToAffect.Toggle();
                    break;
                case Actions.PRIMARY:
                    objectToAffect.Primary();
                    break;
                case Actions.SECONDARY:
                    objectToAffect.Secondary();
                    break;
            }
        }
	}
}
