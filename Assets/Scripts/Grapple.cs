﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour {
    public float thrust = 2500;
    public float ropeMax = 100f;
    public float ropeLength = 0f;
    float scrollSpeed = 500;
    public Rigidbody player;
    public float retractSpeed = 60;
    Transform hook;
    public Transform rope;
    public Collider hookCollider;
    bool active;
    public bool fireable;
    bool retracting = false;
    Rigidbody attachedRB;
    public Transform grappleGun;
    Rigidbody rb;
    private Transform anchor;
    public float ropeDistanceMax;
    public float ropeConnectionMax;
    AudioSource source;
    public AudioClip GrappleShootclip;
    public AudioClip GrappleReelingclip;
    


    private void Start()
    {
        source = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        fireable = true;

    }
    private void Update()
    {
        if (this.anchor != null)
        {
            this.transform.position = anchor.transform.position;
            this.transform.rotation = anchor.transform.rotation;
        }
    }
    void FixedUpdate()
    {
       ropeLength = Vector3.Distance(gameObject.transform.position, player.transform.position);//ropeLength
        
        if (ropeLength >= ropeMax)
        {
            retracting = true;
            
        }
        
        if(retracting)
        {
            Retract();
        }
        if (active)
        {
            
            Shoot();
        }
        Vector3 direction = rope.position - transform.position;
        if (attachedRB != null)
        {
            
            if (ropeDistanceMax < ropeLength)
            {
                attachedRB.AddForceAtPosition(new Vector3(direction.x, 0, direction.z).normalized * (ropeLength-ropeDistanceMax)*(attachedRB.mass*400), transform.position);
                
            }
            if(ropeDistanceMax < ropeLength - 1)
            {
                Retract();
            }
            // += Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
            if (Input.GetKey(KeyCode.Q))
            {
                attachedRB.AddForceAtPosition(40 * new Vector3(direction.x, 0, direction.z).normalized, transform.position);
                ropeDistanceMax = Vector3.Distance(gameObject.transform.position, player.transform.position);
            }
            else if(Input.GetKey(KeyCode.E))
            {
               // attachedRB.AddForceAtPosition(-40 * new Vector3(direction.x, 0, direction.z).normalized, transform.position);
                
                ropeDistanceMax = Vector3.Distance(gameObject.transform.position, player.transform.position);
            }
            else
            {
                try
                {
                    attachedRB.AddForceAtPosition((Input.GetAxis("Mouse ScrollWheel") * -scrollSpeed) * new Vector3(direction.x, 0, direction.z).normalized, transform.position);
                }
                catch
                {

                }
        
            }
            
            
        }
       
       
    }

    void OnCollisionEnter(Collision collision)
    {
        
        fireable = true;
        if (active)
        {
            ropeDistanceMax = Vector3.Distance(gameObject.transform.position, player.transform.position);
            anchor = new GameObject("Grapple_Anchor").transform;
            anchor.transform.position = this.transform.position;
            anchor.transform.rotation = this.transform.rotation;
            anchor.transform.parent = collision.transform;
            ropeDistanceMax = Vector3.Distance(gameObject.transform.position, player.transform.position);
            //this.anchor = anchor.transform;
            if (!collision.gameObject.CompareTag("Grapple"))
            {
                retracting = true;
                Retract();
               // return;
            }
            Deactivate();
            

            
            if (collision.gameObject.GetComponent<Rigidbody>() != null)
            {
                attachedRB = collision.gameObject.GetComponentInParent<Rigidbody>();
                player.gameObject.GetComponent<Player>().attachedToRB = true;
            }
            else
            {
                player.gameObject.GetComponent<Player>().attached = true;
            }
        }


    }

    public void Detach()
    {
        try
        {
            Destroy(anchor.gameObject);
        }
        catch {
        }
        retracting = true;
        //Retract();
        attachedRB = null;
        active = false;
    }

    public void Activate()
    {
        active = true;
        hookCollider.enabled = true;

    }
    void Shoot() {
        if (fireable)
        {
            rb.isKinematic = false;
            rb.AddForceAtPosition(-1 * transform.up, transform.position, ForceMode.Impulse);
            rb.AddForce(transform.forward * thrust);
            fireable = false;
        }
            
            
     }

    public void Deactivate()
    {
        active = false;
        hookCollider.enabled = false;
        rb.isKinematic = true;
    }
    public void Retract()
    {
        Detach();
        if (retracting)
        {
            

            //  rb.useGravity = false ;
            if (Vector3.Distance(transform.position, grappleGun.transform.position) > 1f)
            {
                rb.isKinematic = true;
                rb.isKinematic = false;
                Vector3 directionOfTravel = grappleGun.transform.position - transform.position;

                directionOfTravel.Normalize();


                this.transform.Translate(
                    (directionOfTravel.x * retractSpeed * Time.deltaTime),
                    (directionOfTravel.y * retractSpeed * Time.deltaTime),
                    (directionOfTravel.z * retractSpeed * Time.deltaTime),
                    Space.World);
                // transform.position = Vector3.MoveTowards(transform.position, grappleGun.transform.position, 4000f * Time.deltaTime);
                player.gameObject.GetComponent<Player>().attachedToRB = false;
                player.gameObject.GetComponent<Player>().attached = false;
            }
            else
            {
               // retracting = false;
                rb.isKinematic = true;
               transform.parent = grappleGun;
              // transform.localPosition = new Vector3(0, -8.2f, 0);
               // transform.localScale = new Vector3(1, 1, 1);
                transform.localRotation = Quaternion.identity;
                transform.localPosition = new Vector3(0.003f, -8.165392e-06f, -0.0003082038f);
              //  transform.localRotation = Quaternion.identity;
                Player.launched=(false);
                retracting = false;
                
                
            }
            fireable = true;



        }
    }
}
