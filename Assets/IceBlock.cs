using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBlock : MonoBehaviour {
    public float targetScale = 0.1f;
    public float shrinkSpeed = 0.1f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

         
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(targetScale, targetScale, targetScale), Time.deltaTime* shrinkSpeed); //transforms local scale to target scale by the shrink speed
        Destroy(gameObject, 10f);//destroys the iceblock after x seconds
}
}
