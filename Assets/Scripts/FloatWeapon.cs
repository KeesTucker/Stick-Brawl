using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatWeapon : MonoBehaviour {

    public Rigidbody rb;

    private int layerMask = 1 << 12;

    private RaycastHit hit;

    // Update is called once per frame
    void FixedUpdate () {
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, 5f, layerMask))
        {
            rb.AddForce(Vector3.up * Time.deltaTime * 800f);
        }
        else if (Physics.Raycast(transform.position, -Vector3.up, out hit, 6f, layerMask))
        {
            rb.AddForce(Vector3.up * Time.deltaTime * 400f);
        }
        rb.AddTorque(Vector3.up * Time.deltaTime * 200f);
    }
}
