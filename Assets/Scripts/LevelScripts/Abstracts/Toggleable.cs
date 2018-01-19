using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Toggleable : MonoBehaviour {
    protected bool primary;

    public abstract void Toggle();

    public abstract void Primary();

    public abstract void Secondary();
}
