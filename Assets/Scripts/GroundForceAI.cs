using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundForceAI : MonoBehaviour
{
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
    public bool hitLowerBody = false;
    public bool hitHead = false;
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
    public bool touchingWall = true;

    public float appliedForce;
    public float counter = 0;
    public float counterO = 0;

    public float dist = 12f;
    public int layerMask = 1 << 12;
    public RaycastHit hitL;
    public RaycastHit hitR;

    public RaycastHit hitC;
    public RaycastHit hitb;

    [SerializeField]
    public GameObject[] playerParts;

    public List<HingeJoint> playerPartsR;

    public bool dead = false;

    //public grappleActivatorAI grappleActivatorScript;

    public GameObject grapple;

    public bool grappled = false;

    public SyncMoveStateAI syncMoveState;

    public PlayerControl playerControl;
    public BaseControl baseControl;
    public PlayerMovementAI playerMovement;

    IEnumerator Start()
    {
        foreach (GameObject playerPart in playerParts)
        {
            playerPartsR.Add(playerPart.GetComponent<HingeJoint>());
        }
        if (GetComponent<PlayerControl>())
        {
            playerControl = GetComponent<PlayerControl>();
        }
        else if (GetComponent<BaseControl>())
        {
            baseControl = GetComponent<BaseControl>();
        }
        yield return new WaitForSeconds(0.3f);
        //grappleActivatorScript = grapple.GetComponent<grappleActivatorAI>(); //Grapple Needs to be a seperate type for ai
        hitLowerBody = false;
        hitHead = false;
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

    void Update()
    {
        if (playerControl)
        {
            if (!playerControl.rClick && grappled)
            {
                grappled = false;
            }
        }
        else if (baseControl && grappled)
        {
            if (!baseControl.rClick)
            {
                grappled = false;
            }
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
        if (body)
        {
            if (Physics.Raycast(body.transform.position, Vector3.down, out hitC, Mathf.Infinity, layerMask) && !dead)
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
                        body.AddForce(Vector3.down * Time.deltaTime * 5000);
                    }
                    else
                    {
                        head.AddForce(Vector3.up * Time.deltaTime * 10000);
                    }
                }
                else
                {
                    hasHit = false;
                    touchingGround = false;
                }
            }
        }

        if (!touchingGround && !dead)
        {
            //syncMoveState.CmdSetState(3);
            foreach (HingeJoint playerPart in playerPartsR)
            {
                playerPart.useSpring = false;
            }
        }
        else if (!dead)
        {
            foreach (HingeJoint playerPart in playerPartsR)
            {
                playerPart.useSpring = true;
            }
        }

        if (dead)
        {
            foreach (HingeJoint playerPart in playerPartsR)
            {
                if (playerPart)
                {
                    Destroy(playerPart);
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

        if (hitULRA == false && hitBody == false && hitLLRA == false && hitULLA == false && hitLLLA == false && hitULRL == false && hitLLRL == false && hitULLL == false && hitLLLL == false && !hitLowerBody && !hitHead)
        {
            touchingWall = false;
        }
        else
        {
            touchingWall = true;
        }
    }
}
