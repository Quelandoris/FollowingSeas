using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindEffect : MonoBehaviour {
    Transform trigger;
    Vector3 wind;

    public void EnterWind(Transform trigger, Vector3 wind)
    {
        this.trigger = trigger;
        this.wind = wind;
    }

    public void LeaveWind(Transform trigger, Vector3 wind)
    {
        if(this.trigger == trigger)
        {
            this.trigger = null;
            this.wind = Vector3.zero;
        }
    }

    public Vector3 GetWind()
    {
        return wind;
    }

}
