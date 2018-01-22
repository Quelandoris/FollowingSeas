using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Button : MonoBehaviour, IHarpoonable {

    public Toggleable objectToAffect;
    public Actions actionType;

    public void OnHarpoon()
    {
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
