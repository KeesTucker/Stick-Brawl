using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPoint : MonoBehaviour
{

    public Transform ragdoll;

    public GameObject velocityCreate;
    public GameObject velocity;

    GameObject MouseFollower;
    public Rigidbody rb;
    public float force = 600f;
    public Vector3 velvec;
    // Use this for initialization
    void Start()
    {
        MouseFollower = GameObject.Find("MouseFollower");
        velocity = Instantiate(velocityCreate, transform.position, Quaternion.identity);
        velocity.GetComponent<FollowRocket>().parent = transform;
        rb = velocity.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        transform.position = ragdoll.position;
        if (rb.velocity.y < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, -1 * Mathf.Atan(rb.velocity.x / rb.velocity.y) * Mathf.Rad2Deg + 180);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, -1 * Mathf.Atan(rb.velocity.x / rb.velocity.y) * Mathf.Rad2Deg);
        }
        
    }
}
