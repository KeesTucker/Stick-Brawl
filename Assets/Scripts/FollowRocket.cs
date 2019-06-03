using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRocket : MonoBehaviour {

    public Rigidbody rb;
    public Transform parent;

    public float force = 10;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (parent)
        {
            if (parent.transform.position.x > transform.position.x)
            {
                rb.AddForce(new Vector3(force * Time.deltaTime * (parent.transform.position.x - transform.position.x), 0, 0));
            }
            else if (parent.transform.position.x < transform.position.x)
            {
                rb.AddForce(new Vector3(-force * Time.deltaTime * (transform.position.x - parent.transform.position.x), 0, 0));
            }

            if (parent.transform.position.y > transform.position.y)
            {
                rb.AddForce(new Vector3(0, force * Time.deltaTime * (parent.transform.position.y - transform.position.y), 0));
            }
            else if (parent.transform.position.y < transform.position.y)
            {
                rb.AddForce(new Vector3(0, -force * Time.deltaTime * (transform.position.y - parent.transform.position.y), 0));
            }
        }
    }
}
