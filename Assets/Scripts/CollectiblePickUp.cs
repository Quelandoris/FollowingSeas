using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblePickUp : MonoBehaviour {
    Object lockObj = new Object();
    bool claimed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            lock (lockObj)
            {
                if (!claimed)
                {
                    ScoreManager.score++;
                    claimed = true;
                }
            }
            Destroy(this.gameObject);
        }
    }
}
