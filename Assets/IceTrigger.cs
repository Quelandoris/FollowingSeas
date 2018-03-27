using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTrigger : MonoBehaviour {
    public Toggleable objectToAffect;
    public Actions actionType;

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.GetComponent("IceBlock") as IceBlock) != null)
        {
            objectToAffect.Secondary();
            

        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if ((other.gameObject.GetComponent("IceBlock") as IceBlock) != null)
        {
            objectToAffect.Primary();


        }

    }
}
