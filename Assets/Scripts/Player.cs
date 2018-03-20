using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Rigidbody), typeof(TrackWind))]
public class Player : MonoBehaviour {
    Rigidbody myRB;
    public float throttleSpeed = 7;
    public float rotateSpeed = 1.2f;
    public float grappleStrength = 10;


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
    public Animator Anim;
    public float foldingSpeed;

    private Vector3 vel;
    TrackWind windScript;
    bool sailEnabled = false;
    public bool grounded;
    public bool attachedToRB = false;
    float scrollSpeed = -75;
    bool reeling = true;
    // Use this for initialization
    void Start() {
        Anim = GetComponent<Animator>();
        windScript = GetComponent<TrackWind>();
        solidLayers = ~(waterLayer | playerLayer | currentLayer);
        myRB = GetComponentInParent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update() {
        vel = myRB.velocity;
        if (Input.GetKeyDown(KeyCode.Escape) && !Application.isEditor)
        {
            SceneManager.LoadScene(0);
        }
        RaycastHit mouseHit;
        Vector3 lookTarget;
        if (Physics.Raycast(new Ray(transform.Find("Camera follow").position, Camera.main.transform.forward), out mouseHit, 100000, solidLayers))
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
            // mast.SetActive(sailEnabled);
            if (sailEnabled)
            {
                GetComponent<WindPush>().enabled = true;
                Anim.SetBool("FullMast", true);
                Anim.Play("FullMast");
                foldingSpeed = 1;
            }
            else
            {
                GetComponent<WindPush>().enabled = false;
                Anim.SetBool("FullMast", false);
                Anim.Play("FullMast");
                foldingSpeed = -1;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            reeling = !reeling;
        }



        rope.transform.LookAt(hook.transform.position);
        rope.transform.localScale = new Vector3(1, 1, Vector3.Distance(hook.transform.position, rope.transform.position));
    }

    private void FixedUpdate()
    {
        if (!grounded) {
            myRB.AddForce(Input.GetAxis("Vertical") * throttleSpeed * transform.forward, ForceMode.Acceleration);
            TurnShip();
            myRB.AddTorque(Input.GetAxis("Horizontal") * -1 * transform.forward);
            //myRB.AddTorque(Input.GetAxis("Horizontal") * -0.1f * transform.forward, ForceMode.Impulse);
            float thrust = Mathf.Max(0, Input.GetAxis("Vertical"));
            myRB.AddTorque(-0.5f * thrust * transform.right);
        }
        if (windScript.GetWind().magnitude > 0)
        {
            //Animate Flag
            flag.transform.LookAt(flag.transform.position + windScript.GetWind());
        }
        else
        {
            //Animate Flag
            flag.transform.LookAt(transform.position - 2 * transform.forward);
        }
        if (sailEnabled)
        {
            Vector3 playerForward = TrackWind.MakeHorizontal(transform.forward);
            float angle = Vector3.Angle(windScript.GetWind(), playerForward);
            float power = AngleToSailPower(angle);
            myRB.AddForce(windScript.GetWind().magnitude * power * transform.forward);


            if (windScript.GetWind().magnitude > 0)
            {
                //Animate Mast
                int side = Vector3.Cross(windScript.GetWind(), playerForward).y > 0 ? 1 : -1;
                mast.transform.localRotation = Quaternion.Euler(0, side * (180 - angle) / 2, 0);
            }
            else
            {
                //Animate Mast
                mast.transform.localRotation = Quaternion.identity;
            }

        }
        if (Input.GetKey(KeyCode.Q))
        {
            grappleStrength = grappleStrength + .2f;
            grappleStrength = Mathf.Clamp(grappleStrength, 2f, 10f);
        
        }
        if (Input.GetKey(KeyCode.E))
        {
            grappleStrength = grappleStrength - .2f;
            grappleStrength = Mathf.Clamp(grappleStrength, 2f, 10f);
        }
        if (Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.Q))
        {
            grappleStrength = 10;
        }


            Vector3 direction = hook.transform.position - transform.position;
      //  Mathf.Clamp(grappleStrength, -10, 10);
                if (attached)//what happens if you are attached to a stationary
                {
                if (hook.GetComponent<Grapple>().ropeDistanceMax < hook.GetComponent<Grapple>().ropeLength)//prevents player from extending past distnace
                {
                    myRB.AddForce(new Vector3(direction.x, 0, direction.z).normalized * (hook.GetComponent<Grapple>().ropeLength - hook.GetComponent<Grapple>().ropeDistanceMax) * 75);
                }
                if (reeling)
                   {
                      
                      myRB.AddForce(grappleStrength * new Vector3(direction.x, 0, direction.z).normalized);
                   }
                }
                if (attachedToRB)
                {
            if (reeling)
            {
                //when you are connected to a moving object
                // myRB.AddForce(grappleStrength * (hook.transform.position - transform.position).normalized);
                myRB.AddForce((Input.GetAxis("Mouse ScrollWheel") * scrollSpeed) * (hook.transform.position - transform.position).normalized);
               
            }
            }
            if (!launched)
            {
                // hook.transform.localPosition = new Vector3(0, -8f, 0);
                hook.transform.localRotation = Quaternion.identity;
            }
        }
        void OnCollisionEnter(Collision collision)
        {
            // hook.transform.localPosition = new Vector3(0, -8f, 0);
            hook.transform.localRotation = Quaternion.identity;
        }

        ApplyFriction();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
        if (collision.gameObject.GetComponent<Rigidbody>())
        {
            myRB.velocity = new Vector3(vel.x, 0, vel.z);
        }
            
       
    }
        void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                grounded = true;
            }
            
       
        }
           void OnCollisionExit(Collision collision)
          {
              if (collision.gameObject.CompareTag("Ground"))
             {
                 grounded = false;
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
            //Calculate the sail thrust based on the angle
            float compliment = 180f - angle;
            float fraction = compliment / 180f;
            float power = 8f * (fraction * fraction * fraction) - 18f * (fraction * fraction) + 12f * fraction - 1.5f;
            //Debug.Log(power);
            return Mathf.Max(0, power);
        }

        void TurnShip()
        {
            Vector3 velocity = myRB.velocity;
            Vector3 onlyForward = Vector3.Project(velocity, transform.forward);
            Vector3 otherVel = velocity - onlyForward;
            transform.Rotate(Input.GetAxis("Horizontal") * rotateSpeed * Vector3.up, Space.World);
            float sign = (onlyForward.normalized + transform.forward).magnitude > 1 ? 1 : -1;
            myRB.velocity = onlyForward.magnitude * sign * transform.forward + otherVel;
        }
    }

