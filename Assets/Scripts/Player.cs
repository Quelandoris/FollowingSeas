using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody), typeof(TrackWind))]
public class Player : MonoBehaviour {
    Rigidbody myRB;
    public float throttleSpeed = 7;
    public float rotateSpeed = 1.0f;
    public static float grappleStrength = 8;
    

    public LayerMask waterLayer;
    public LayerMask playerLayer;
    public LayerMask currentLayer;
    public LayerMask grappleLayer;
    public LayerMask harpoonLayer;
    public LayerMask defaultLayer;
    LayerMask solidLayers;

    public Transform grappleGun;
    public GameObject hook;
    public GameObject rope;
    public GameObject mast;
    public GameObject flag;
    public static bool launched;
    public bool attached;
    public HarpoonLauncher harpoonLauncher;

    TrackWind windScript;
    bool sailEnabled = false;

    // Use this for initialization
    void Start () {
        windScript = GetComponent<TrackWind>();
        solidLayers = ~(waterLayer | playerLayer | currentLayer);
        myRB = GetComponent<Rigidbody>();
	}

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        RaycastHit mouseHit;
        Vector3 lookTarget;
        if(Physics.Raycast(new Ray(Camera.main.transform.position, Camera.main.transform.forward), out mouseHit, 100000, solidLayers))
        {
            lookTarget = mouseHit.point;
        }
        else
        {
            lookTarget = 10000 * Camera.main.transform.forward + Camera.main.transform.position;
        }
        grappleGun.LookAt(lookTarget);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            harpoonLauncher.isShooting = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            harpoonLauncher.isShooting = false;
        }
        
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (launched)
            {
                hook.GetComponent<Grapple>().Retract();
                attached = false;

            }
            else
            {
             
                    Fire();
                
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            sailEnabled = !sailEnabled;
            mast.SetActive(sailEnabled);
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

        if (sailEnabled)
        {
            Vector3 playerForward = TrackWind.MakeHorizontal(transform.forward);
            float angle = Vector3.Angle(windScript.GetWind(), playerForward);
            float power = AngleToSailPower(angle);
            myRB.AddForce(windScript.GetWind().magnitude * power * transform.forward);

            
            if(windScript.GetWind().magnitude > 0)
            {
                //Animate Flag
                flag.transform.LookAt(flag.transform.position + windScript.GetWind());
                //Animate Mast
                int side = Vector3.Cross(windScript.GetWind(), playerForward).y > 0 ? 1 : -1;
                Debug.Log(angle);
                mast.transform.localRotation = Quaternion.Euler(0, side * (180 - angle) / 2, 0);
            }
            else
            {
                //Animate Flag
                flag.transform.LookAt(transform.position - 2 * transform.forward);
                //Animate Mast
                mast.transform.localRotation = Quaternion.identity;
            }

            
        }
        
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
        
       // hook.GetComponent<Grapple>().fired = (false);
        launched = true;
    }

   /* public void Retract()
    {
        hook.transform.parent = grappleGun;
        hook.transform.localPosition = new Vector3(0, 0, 1.5f);
        hook.transform.localScale = new Vector3(1, 1, 1);
        hook.transform.localRotation = Quaternion.identity;
        hook.GetComponent<Grapple>().Detach();
        hook.GetComponent<Rigidbody>().isKinematic = true;
        hook.GetComponent<Grapple>().fired = true;
        launched = false;
        attached = false;
    }*/

    float AngleToSailPower(float angle)
    {
        //Calculate thrust drop-off for a headwind
        float upwind;
        upwind = -0.03905f * (angle - 135);
        upwind = Mathf.Clamp(upwind, -0.5857f, 0);
        //Calculate the sail thrust based on the angle
        float compliment = 180 - angle;
        float power = -0.0000428f * compliment * compliment + 0.00838f * compliment + 0.5857f;
        return power + upwind;
    }
}
