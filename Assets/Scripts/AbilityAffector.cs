using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityAffector : MonoBehaviour {

    public int abilityIndex;
    public float startForce;
    public Rigidbody rb;
    public bool onServer;
    public GameObject localRelay;
    public bool collided;

    IEnumerator Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        for (int i = 0; i < 200; i++)
        {
            rb.AddForce(startForce * Time.deltaTime * transform.up);
        }
        yield return new WaitForSeconds(2f);
        if (collided)
        {
            Destroy(gameObject.GetComponent<SpriteRenderer>());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.layer == 24)
        {
            if (abilityIndex == 0)
            {
                localRelay.GetComponent<PlayerMovementAI>().maxSpeed = 10;
                onServer = true;
                transform.parent = transform.parent.parent;
                collided = true;
                yield return new WaitForSeconds(10f);
                localRelay.GetComponent<PlayerMovementAI>().maxSpeed = 45;
                Destroy(gameObject);
            }
        }
    }
}
