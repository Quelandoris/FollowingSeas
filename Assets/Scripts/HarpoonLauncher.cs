using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonLauncher : MonoBehaviour {

    public bool isShooting;
    public Harpoon harpoon;
    public float timeBetweenShots = 0.5f;
    private float shotCounter;
    public Transform firePoint;
     AudioSource source;
    public AudioClip clip;

    public void Start()
    {
        source.GetComponent<AudioSource>();
    }

    public void Update ()
    {
        shotCounter -= Time.deltaTime;
        if (isShooting)
        {
            if(shotCounter <= 0)
            {
                shotCounter = timeBetweenShots;
                Instantiate(harpoon, firePoint.position, (firePoint.rotation));
                source.PlayOneShot(clip,1f);
                gameObject.GetComponentInParent<Rigidbody>().AddForceAtPosition(-1 * transform.up, transform.position, ForceMode.Impulse);//this is our recoil
            }
            
        }
	}
}
