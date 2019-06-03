using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGroundLRAI : MonoBehaviour {

	public bool jumpNow;
    public RaycastHit hitL;
    public int layerMask = 1 << 12;
    public GameObject animator;
    void OnCollisionEnter(Collision collsionInfo)
    {
        if (collsionInfo.collider.gameObject.layer == 12 || collsionInfo.collider.gameObject.layer == 11)
        {
            animator.GetComponent<PlayerMovementAI>().groundHitL = true;
        }
    }

    void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out hitL, 5, layerMask) == false)
        {
            animator.GetComponent<PlayerMovementAI>().groundHitL = false;
        }
    }
}
