using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionFollow : MonoBehaviour {

    public Transform player;

    public Rigidbody rb;

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(new Vector3(Mathf.Clamp(transform.position.x - player.position.x, -5, 5) * 1, Mathf.Clamp(transform.position.y - player.position.y, -5, 5) * 1, 0));
    }
}
