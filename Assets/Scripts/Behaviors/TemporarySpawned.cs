using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporarySpawned : MonoBehaviour {

    public TemporaryObjectSpawner spawner;
    public float lifespan;
    float timeAlive = 0;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        timeAlive += Time.deltaTime;
        if(timeAlive >= lifespan)
        {
            spawner.Secondary();
            Destroy(this.gameObject);
        }
	}

}
