using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPlaceHolder : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
