using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : Toggleable {
    public Vector3 negativePosition;
    public Vector3 positivePosition;
    float remainingTime = 0;
    public float duration;


    private void FixedUpdate()
    {
        remainingTime -= Time.fixedDeltaTime;
        if(remainingTime < 0)
        {
            remainingTime = 0;
        }

        if (positive)
        {
            transform.localPosition = Vector3.Lerp(positivePosition, negativePosition, remainingTime/duration);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(negativePosition, positivePosition, remainingTime / duration);
        }
        
    }

    public override void Positive()
    {
        if (!positive)
        {
            positive = true;
            remainingTime = duration - remainingTime;
        }
    }

    public override void Negative()
    {
        if (positive)
        {
            positive = false;
            remainingTime = duration - remainingTime;
        }
    }

    public override void Toggle()
    {
        if (positive)
        {
            Negative();
        }
        else
        {
            Positive();
        }
    }
}
