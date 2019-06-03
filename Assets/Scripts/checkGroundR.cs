using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkGroundR : MonoBehaviour {

    public bool jumpNow;
    public RaycastHit hitR;
    public int layerMask = 1 << 12;
    public PlayerMovement playerMovement;

    void OnCollisionEnter(Collision collsionInfo)
    {
        if (collsionInfo.collider.gameObject.layer == 12 || collsionInfo.collider.gameObject.layer == 11)
        {
            playerMovement.groundHitR = true;
        }
    }

    void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out hitR, 5, layerMask) == false)
        {
            playerMovement.groundHitR = false;
        }
    }
}
