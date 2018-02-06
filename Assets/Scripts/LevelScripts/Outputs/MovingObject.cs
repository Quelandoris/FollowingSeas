﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : Toggleable {
    public Vector3 primaryPosition;
    public Vector3 secondaryPosition;
    float remainingTime = 0;
    public float duration;

    private void Start()
    {
        if(duration == 0)
        {
            this.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        remainingTime = Mathf.Max(remainingTime - Time.fixedDeltaTime, 0);

        if (primary)
        {
            Vector3 targetPosition = Vector3.Lerp(primaryPosition, secondaryPosition, remainingTime / duration);
            Debug.Log(targetPosition);
            transform.localPosition = targetPosition;
        }
        else
        {
            transform.localPosition = Vector3.Lerp(secondaryPosition, primaryPosition, remainingTime / duration);
        }
        
    }

    public override void Primary()
    {
        if (!primary)
        {
            primary = true;
            remainingTime = duration - remainingTime;
        }
    }

    public override void Secondary()
    {
        if (primary)
        {
            primary = false;
            remainingTime = duration - remainingTime;
        }
    }

    public override void Toggle()
    {
        BasicToggle();
    }
}
