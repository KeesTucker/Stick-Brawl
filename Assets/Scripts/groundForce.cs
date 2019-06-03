using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundForce : MonoBehaviour {
    public Rigidbody rbL;
    public Rigidbody rbR;
    public Rigidbody head;
    public Transform rbLt;
    public Transform rbRt;
    public Rigidbody body;
    public bool hit;
    public bool hitBody = true;
    public bool hitULRA = true;
    public bool hitLLRA = true;
    public bool hitULLA = true;
    public bool hitLLLA = true;
    public bool hitULRL = true;
    public bool hitLLRL = true;
    public bool hitULLL = true;
    public bool hitLLLL = true;
    public bool hitBodyObject = false;
    public bool hitULRAObject = false;
    public bool hitLLRAObject = false;
    public bool hitULLAObject = false;
    public bool hitLLLAObject = false;
    public bool hitULRLObject = false;
    public bool hitLLRLObject = false;
    public bool hitULLLObject = false;
    public bool hitLLLLObject = false;
    public bool hasHit = true;
    public bool touchingGround = true;
    public bool touchingObject = true;

    public float appliedForce;
    public float counter = 0;
    public float counterO = 0;

    public float dist = 12f;
    public int layerMask = 1 << 12;
    public RaycastHit hitL;
    public RaycastHit hitR;

    public RaycastHit hitC;

    [SerializeField]
    public GameObject[] playerParts;

    public grappleActivator grappleActivatorScript;

    public GameObject grapple;

    public bool grappled = false;

    public SyncMoveState syncMoveState;

    void Start()
    {
        playerParts = GameObject.FindGameObjectsWithTag("Floppy");
        grappleActivatorScript = grapple.GetComponent<grappleActivator>();
    }

    void OnCollisionEnter(Collision collsionInfo)
    {
        if (collsionInfo.collider.gameObject.layer == 12 || collsionInfo.collider.gameObject.layer == 11)
        {
            hitBody = true;
        }
        if (collsionInfo.collider.gameObject.layer == 16)
        {
            hitBodyObject = true;
        }
    }
    void OnCollisionExit(Collision collsionInfo)
    {
        if (collsionInfo.collider.gameObject.layer == 12 || collsionInfo.collider.gameObject.layer == 11)
        {
            hitBody = false;
        }
        if (collsionInfo.collider.gameObject.layer == 16)
        {
            hitBodyObject = false;
        }
    }

    void FixedUpdate()
    {
        /*if (hitULRA == false && hitBody == false && hitLLRA == false && hitULLA == false && hitLLLA == false && hitULRL == false && hitLLRL == false && hitULLL == false && hitLLLL == false && Physics.Raycast(body.transform.position, Vector3.down, out hitR, dist, layerMask) == false)
        {
            hasHit = false;
            touchingGround = false;
        }
        else if(!grappleActivatorScript.Collided)
        {
            
        }*/

        if (Physics.Raycast(body.transform.position, Vector3.down, out hitC, Mathf.Infinity, layerMask))
        {
            if (hitC.distance < 10 && !grappled)
            {
                touchingGround = true;
                touchingObject = true;
                if (hasHit == false)
                {
                    appliedForce = counter * 500;
                    if (counter > 99)
                    {
                        hasHit = true;
                        counter = 0;
                    }
                    counter = counter + 2.0f;
                }
                body.AddForce(0, 2 * -appliedForce * Time.deltaTime, 0);
                head.AddForce(0, 2 * appliedForce * Time.deltaTime, 0);

                if (hitC.distance > 5.5)
                {
                    body.GetComponent<Rigidbody>().AddForce(Vector3.down * Time.deltaTime * 5000);
                }
                else
                {
                    head.GetComponent<Rigidbody>().AddForce(Vector3.up * Time.deltaTime * 10000);
                }
            }
            else
            {
                hasHit = false;
                touchingGround = false;
            }
        }

        if (!touchingGround)
        {
            syncMoveState.CmdSetState(3);
            foreach(GameObject playerPart in playerParts)
            {
                if (playerPart.GetComponent<ID>().IDPlayer == gameObject.GetComponent<ID>().IDPlayer)
                {
                    playerPart.GetComponent<HingeJoint>().useSpring = false;
                }
            }
        }
        else
        {
            foreach (GameObject playerPart in playerParts)
            {
                if (playerPart.GetComponent<ID>().IDPlayer == gameObject.GetComponent<ID>().IDPlayer)
                {
                    playerPart.GetComponent<HingeJoint>().useSpring = true;
                }
            }
        }

        if (hitULRAObject == false && hitBodyObject == false && hitLLRAObject == false && hitULLAObject == false && hitLLLAObject == false && hitULRLObject == false && hitLLRLObject == false && hitULLLObject == false && hitLLLLObject == false && Physics.Raycast(rbRt.position, Vector3.down, out hitR, dist, layerMask) == false && Physics.Raycast(rbLt.position, Vector3.down, out hitL, dist, layerMask) == false)
        {
            hasHit = false;
            touchingObject = false;
        }
        else
        {
            touchingObject = true;
            if (hasHit == false)
            {
                appliedForce = counterO * 750;
                if (counterO > 99)
                {
                    hasHit = true;
                    counterO = 0;
                }
                counterO = counterO + 2.0f;
            }
        }
    }
}
