using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowAI : MonoBehaviour {

    public Transform parent;

    public Transform pos;

    public Transform aim;

    public Vector3 offset;

    public Vector3 target;

    public Rigidbody rb;

    public float force;

    public Transform rocket;
	
	// Update is called once per frame
    IEnumerator Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, offset.z);
        yield return new WaitForSeconds(0.1f);
        if (parent.GetComponent<PlayerControl>())
        {
            while (!gameObject.GetComponent<SpawnRocketAI>())
            {
                yield return null;
            }
            while (!gameObject.GetComponent<SpawnRocketAI>().rocketGO)
            {
                yield return null;
            }
            rocket = gameObject.GetComponent<SpawnRocketAI>().rocketGO.transform;
        }
    }

    /*void Update()
    {
        transform.position = parent.position + offset;
    }*/

	void FixedUpdate () {
        if (rocket && !pos)
        {
            pos = rocket;
        }
        else
        {
            pos = null;
        }
        if (parent && !pos)
        {
            pos = parent;
        }
        if (pos)
        {
            target = pos.position;
        }
        rb.AddForce(new Vector3(force * Time.deltaTime * (target.x - transform.position.x), force * Time.deltaTime * (target.y - transform.position.y), 0));
    }
}
