using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinTerrain : MonoBehaviour {

    public float amount = 50f;
    public Rigidbody rb;
    public float targetSpeed = 20;

	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float force = amount * Time.deltaTime;
        if (targetSpeed > 0)
        {
            if (rb.angularVelocity.z < targetSpeed)
            {
                rb.AddTorque(transform.forward * force);
            }
        }
        else
        {
            if (rb.angularVelocity.z > targetSpeed)
            {
                rb.AddTorque(transform.forward * -force);
            }
        }
    }
}
