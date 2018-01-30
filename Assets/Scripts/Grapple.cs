using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour {
    public float thrust = 800;
    public float ropeMax = 100f;
    public Rigidbody player;
    Transform hook;
    public Transform rope;
    public Collider hookCollider;
    bool active;
    public bool fired;
    Rigidbody attachedRB;
    public Transform grappleGun;
    Rigidbody rb;
    private Transform anchor;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        fired = true;
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
        float ropeLength = Vector3.Distance(gameObject.transform.position, player.transform.position);//ropeLength
        if(ropeLength >= ropeMax)
        {
           
            Detach();
            rb.isKinematic = true;
            fired = true;
            transform.parent = grappleGun;
            transform.localPosition = new Vector3(0, 0, 1.5f);
            transform.localScale = new Vector3(1, 1, 1);
            //transform.localRotation = Quaternion.identity;
            transform.localPosition = new Vector3(0, 0, 1.5f);
            transform.localRotation = Quaternion.identity;
            Player.launched=(false);
        }

        if (active)
        {
            
            Shoot();
        }
        if (attachedRB != null)
        {
            attachedRB.AddForceAtPosition(Player.grappleStrength * (rope.position - transform.position).normalized, transform.position);
        }
        
    }

    void OnCollisionEnter(Collision collision)
    {
        fired = true;
        if (active)
        {
           
            anchor = new GameObject("Grapple_Anchor").transform;
            anchor.transform.position = this.transform.position;
            anchor.transform.rotation = this.transform.rotation;
            anchor.transform.parent = collision.transform;
            //this.anchor = anchor.transform;
            if (!collision.gameObject.CompareTag("Grapple"))
            {
                player.GetComponent<Player>().Retract();
                return;
            }
            Deactivate();
            player.gameObject.GetComponent<Player>().attached = true;


            Transform otherRoot = collision.transform;
            while (otherRoot.parent != null)
            {
                otherRoot = otherRoot.parent;
            }
            attachedRB = otherRoot.GetComponent<Rigidbody>();
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
        attachedRB = null;
        active = false;
    }

    public void Activate()
    {
        active = true;
        hookCollider.enabled = true;

    }
    void Shoot() {
        if (fired)
        {
            rb.isKinematic = false;
            rb.AddForceAtPosition(-1 * transform.up, transform.position, ForceMode.Impulse);
            rb.AddForce(transform.forward * thrust);
            fired = false;
        }
            
            
     }

    public void Deactivate()
    {
        active = false;
        hookCollider.enabled = false;
        rb.isKinematic = true;
    }
}
