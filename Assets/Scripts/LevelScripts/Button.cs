using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Actions { TOGGLE, POSITIVE, NEGATIVE}

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
            case Actions.POSITIVE:
                objectToAffect.Positive();
                break;
            case Actions.NEGATIVE:
                objectToAffect.Negative();
                break;
        }
    }
}
