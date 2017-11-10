using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour {

    public Rigidbody player;
    public Collider hookCollider;
    bool active;
    Rigidbody attachedRB;

    void FixedUpdate()
    {
        if (active)
        {
            transform.Translate(Vector3.forward);
        }
        if(attachedRB != null)
        {
            attachedRB.AddForceAtPosition(Player.grappleStrength * (player.transform.position - transform.position).normalized, transform.position);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (active)
        {
            Deactivate();
            player.gameObject.GetComponent<Player>().attached = true;
            transform.parent = collision.gameObject.transform;

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
