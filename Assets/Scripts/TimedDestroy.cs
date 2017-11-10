using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestroy : MonoBehaviour
{ //Destroys the Attached gameobject after a delay set via the inspector
    public float lifespan;
    void Start()
    {
        Destroy(this.gameObject, lifespan);
    }
}