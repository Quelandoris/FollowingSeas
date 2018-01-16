using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour {
    Rigidbody myRB;
    public float throttleSpeed = 7;
    public float rotateSpeed = 0.5f;
    public static float grappleStrength = 8;
    

    public LayerMask waterLayer;
    public LayerMask playerLayer;
    public LayerMask currentLayer;
    public LayerMask grappleLayer;
    public LayerMask harpoonLayer;
    public LayerMask defaultLayer;

    public Transform grappleGun;
    public GameObject hook;
    public GameObject rope;
    bool launched;
    public bool attached;
    bool hasGrappleTarget;
    public HarpoonLauncher harpoonLauncher;

    // Use this for initialization
    void Start () {
        myRB = GetComponent<Rigidbody>();
        //grappleLayer = ~(waterLayer | playerLayer | currentLayer);  //Makes everything but the water, player, or current layer the grapple layer
	}

    // Update is called once per frame
    void Update() {
        RaycastHit mouseHit;
        if (Physics.Raycast(new Ray(Camera.main.transform.position, Camera.main.transform.forward), out mouseHit, 100000, ~(playerLayer))) { 
            grappleGun.LookAt(mouseHit.point);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            harpoonLauncher.isShooting = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            harpoonLauncher.isShooting = false;
        }
        hasGrappleTarget = Physics.Raycast(new Ray(Camera.main.transform.position, Camera.main.transform.forward), out mouseHit, 100000, grappleLayer);
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (launched)
            {
                Retract();
                launched = false;
                attached = false;
            }
            else if(hasGrappleTarget)
            {
                Fire();
                launched = true;
            }
        }




        rope.transform.LookAt(hook.transform.position);
        rope.transform.localScale = new Vector3(1, 1, Vector3.Distance(hook.transform.position, rope.transform.position));
	}

    private void FixedUpdate()
    {
        myRB.AddForce(Input.GetAxis("Vertical") * throttleSpeed * transform.forward, ForceMode.Acceleration);
        transform.Rotate(Input.GetAxis("Horizontal") * rotateSpeed * Vector3.up, Space.World);
        myRB.AddTorque(Input.GetAxis("Horizontal") * -1 * transform.forward);
        //myRB.AddTorque(Input.GetAxis("Horizontal") * -0.1f * transform.forward, ForceMode.Impulse);
        float thrust = Mathf.Max(0, Input.GetAxis("Vertical"));
        myRB.AddTorque(-0.5f * thrust * transform.right);
        
        if (attached)
        {
            myRB.AddForce(grappleStrength * (hook.transform.position - transform.position).normalized);
        }
        if (!launched)
        {
            hook.transform.localPosition = new Vector3(0, 0, 1.5f);
            hook.transform.localRotation = Quaternion.identity;
        }
    }

    void Fire()
    {
        hook.transform.parent = null;
        hook.GetComponent<Grapple>().Activate();
    }

    void Retract()
    {
        hook.transform.parent = grappleGun;
        hook.transform.localPosition = new Vector3(0, 0, 1.5f);
        hook.transform.localScale = new Vector3(1, 1, 1);
        hook.transform.localRotation = Quaternion.identity;
        hook.GetComponent<Grapple>().Detach();
    }
}
