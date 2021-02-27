using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointCreator : MonoBehaviour {

    Vector2 radius;
    Vector2 velocity;
    Rigidbody2D otherBody;
    JointDeleter deleterPlayer;
    Joint2D hinge;
    public GameObject JointSource;
    
    public bool jointDeleted = true;

	void Start () {
		
	}
	
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;
        deleterPlayer = other.gameObject.GetComponent<JointDeleter>();
        otherBody = other.transform.gameObject.GetComponent<Rigidbody2D>();
        radius = other.transform.position - transform.position;
        velocity = otherBody.velocity;
        if (IsTangent(radius, velocity) && !jointDeleted && deleterPlayer.currentAttractor == null) 
        {
            CreateJointTo(otherBody, deleterPlayer);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;
        jointDeleted = false;
    }

    bool IsTangent(Vector2 v1, Vector2 v2)
    {
        if (Vector2.Angle(v1, v2) - 90f < 0.1f)
            return true;
        return false;
    }

    void CreateJointTo(Rigidbody2D rigidbody, JointDeleter jointDeleter)
    {
        hinge = gameObject.GetComponent<Joint2D>();
        hinge.connectedBody = rigidbody;
        jointDeleter.currentAttractor = transform.gameObject;
    }
}
