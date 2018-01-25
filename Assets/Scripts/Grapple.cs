using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour {

    public Rigidbody player;
    Transform hook;
    public Transform rope;
    public Collider hookCollider;
    bool active;
    Rigidbody attachedRB;
    private Transform anchor;

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
        if (anchor != null)
        {
            float ropeLength = Vector3.Distance(gameObject.transform.position, player.transform.position);
            Debug.Log(ropeLength);
        }
        
        if (active)
        {
            transform.Translate(Vector3.forward);
        }
        if(attachedRB != null)
        {
            attachedRB.AddForceAtPosition(Player.grappleStrength * (rope.position - transform.position).normalized, transform.position);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (active)
        {
            anchor = new GameObject("Grapple_Anchor").transform;
            anchor.transform.position = this.transform.position;
            anchor.transform.rotation = this.transform.rotation;
            anchor.transform.parent = collision.transform;
            this.anchor = anchor.transform;
            if (collision.gameObject.CompareTag("Harpoon"))
            {
                player.GetComponent<Player>().Retract();
                return;
            }
            Deactivate();
            player.gameObject.GetComponent<Player>().attached = true;
            //transform.parent = collision.gameObject.transform;

            Transform otherRoot = collision.transform;
            while(otherRoot.parent != null)
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

    public void Deactivate()
    {
        active = false;
        hookCollider.enabled = false;
    }
}
