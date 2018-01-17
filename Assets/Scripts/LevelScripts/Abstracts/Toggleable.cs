using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Toggleable : MonoBehaviour {
    protected bool positive;

    public abstract void Toggle();

    public abstract void Positive();

    public abstract void Negative();
}
