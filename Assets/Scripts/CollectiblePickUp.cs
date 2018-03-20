using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblePickUp : MonoBehaviour {


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager.score++;
            Destroy(this.gameObject);
        }
    }
}
