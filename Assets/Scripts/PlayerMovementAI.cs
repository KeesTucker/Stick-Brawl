using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementAI : MonoBehaviour {

    public GameObject jetFlashL;
    public Material jetMaterialL;
    public GameObject jetFlashR;
    public Material jetMaterialR;

    public Rigidbody body;
    public Rigidbody lFoot;
    public Rigidbody rFoot;
    public Rigidbody rLeg;
    public Rigidbody lLeg;
    public Rigidbody jetpack;

    private bool aDepressed = false;
    private bool dDepressed = false;
    private bool shiftDepressed = false;
    private bool spaceDepressed = false;
    private bool spaceJetDepressed = false;
    private bool sDepressed = false;

    //Change these with ai
    public bool a = false;
    public bool d = false;
    public bool shift = false;
    public bool space = false;
    public bool s = false;

    public bool groundHitL;
    public bool groundHitR;
    public bool jumpable = true;
    public bool boostable = true;

    public int getDown = 0;

    public GroundForceAI groundforce;

    public bool unCrouch;

    public Rigidbody Ragdoll;

    public float walkForce = 40000f;
    public float jumpForce = 40000f;

    public bool grappleSince = false;

    public Animator anim;
    int walkingR = Animator.StringToHash("WalkingR");
    int walkingL = Animator.StringToHash("WalkingL");
    private RaycastHit hit;

    public ShootAI shoot;

    public float maxSpeed = 45;

    public bool fire;

    bool keysTouched;

    public int state;

    public SyncMoveStateAI syncMoveState;

    public float downForce;

    public bool inAir;

    public SpawnRocketAI spawnRocket;

    public Rigidbody[] downForceLimbs;

    public int layerMask = 1 << 12;

    public bool jumping = true;

    public GameObject footL;
    public GameObject footR;

    public GameObject jumpParticle;

    public AudioSource audioSource;
    public AudioClip jump;

    public Transform local;

    public bool applying;

    IEnumerator Start()
    {
        while (!GameObject.Find("LocalPlayer") && !GameObject.Find("LoadingPlayer"))
        {
            yield return null;
        }
        if (GameObject.Find("LocalPlayer"))
        {
            local = GameObject.Find("LocalPlayer").transform;
        }
        else if (GameObject.Find("LoadingPlayer"))
        {
            local = GameObject.Find("LoadingPlayer").transform;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (a)
        {
            aDepressed = true;
        }
        else
        {
            aDepressed = false;
        }

        if (d)
        {
            dDepressed = true;
        }
        else
        {
            dDepressed = false;
        }

        if (s)
        {
            sDepressed = true;
        }
        else if (!s)
        {
            sDepressed = false;
            getDown = 0;
        }

        if (space)
        {
            spaceDepressed = true;
            spaceJetDepressed = true;
            space = false;
        }
        /*if (!space)
        {
            spaceDepressed = false;
            spaceJetDepressed = false;
        }*/

        if (shift)
        {
            shiftDepressed = true;
        }
        else
        {
            shiftDepressed = false;
        }

        if (aDepressed || dDepressed || spaceDepressed)
        {
            grappleSince = false;
        }

        if (shoot.fireButtonDown)
        {
            fire = true;
            keysTouched = false;
        }
        else if (aDepressed || dDepressed || spaceDepressed)
        {
            StartCoroutine("fireOffTimer");
            keysTouched = true;
        }

        if (aDepressed && Ragdoll.velocity.x < -maxSpeed)
        {
            body.AddForce(walkForce * Time.deltaTime * 2f, 0, 0);
        }
        if (dDepressed && Ragdoll.velocity.x > maxSpeed)
        {
            body.AddForce(-walkForce * Time.deltaTime * 2f, 0, 0);
        }
        
        if (!dDepressed && !aDepressed && !spaceDepressed && groundforce.touchingGround && !grappleSince && !fire)
        {
            body.AddForce(walkForce * Time.deltaTime * 0.2f * -Ragdoll.velocity.x, 0, 0);
        }

        if (aDepressed == true && (groundHitL == true || groundHitR == true))
        {
            lFoot.AddForce(-walkForce * Time.deltaTime * 0.3f, walkForce * Time.deltaTime * 0.05f, 0);
            rFoot.AddForce(-walkForce * Time.deltaTime * 0.3f, walkForce * Time.deltaTime * 0.05f, 0);
            body.AddForce(-walkForce * Time.deltaTime * 2f, 0, 0);
            anim.SetTrigger(walkingL);
            state = 1;
            //syncMoveState.CmdSetState(state);
            anim.ResetTrigger("stand");
            anim.ResetTrigger("WalkingR");
        }
        else if (aDepressed == true && Physics.Raycast(body.transform.position, -Vector3.right, out hit, 20f, layerMask))
        {
            body.AddForce(-walkForce * Time.deltaTime * 1.5f, 0, 0);
            anim.SetTrigger(walkingL);
            state = 1;
            //syncMoveState.CmdSetState(state);
            anim.ResetTrigger("stand");
            anim.ResetTrigger("WalkingR");
        }
        else if (aDepressed == true)
        {
            body.AddForce(-walkForce * Time.deltaTime * 1f, 0, 0);
            anim.SetTrigger(walkingL);
            state = 1;
            //syncMoveState.CmdSetState(state);
            anim.ResetTrigger("stand");
            anim.ResetTrigger("WalkingR");
        }
        else if (dDepressed == true && (groundHitL == true || groundHitR == true))
        {
            lFoot.AddForce(walkForce * Time.deltaTime * 0.3f, walkForce * Time.deltaTime * 0.05f, 0);
            rFoot.AddForce(walkForce * Time.deltaTime * 0.3f, walkForce * Time.deltaTime * 0.05f, 0);
            body.AddForce(walkForce * Time.deltaTime * 2f, 0, 0);
            anim.SetTrigger(walkingR);
            state = 0;
            //syncMoveState.CmdSetState(state);
            anim.ResetTrigger("stand");
            anim.ResetTrigger("WalkingL");
        }
        else if (aDepressed == true && Physics.Raycast(body.transform.position, Vector3.right, out hit, 20f, layerMask))
        {
            body.AddForce(walkForce * Time.deltaTime * 1.5f, 0, 0);
            anim.SetTrigger(walkingR);
            state = 1;
            //syncMoveState.CmdSetState(state);
            anim.ResetTrigger("stand");
            anim.ResetTrigger("WalkingL");
        }
        else if (dDepressed == true)
        {
            body.AddForce(walkForce * Time.deltaTime * 1f, 0, 0);
            anim.SetTrigger(walkingR);
            state = 0;
            //syncMoveState.CmdSetState(state);
            anim.ResetTrigger("stand");
            anim.ResetTrigger("WalkingL");
        }
        else
        {
            anim.ResetTrigger("WalkingR");
            anim.ResetTrigger("WalkingL");
            anim.SetTrigger("stand");
            state = 2;
            //syncMoveState.CmdSetState(state);

        }

        if (spaceDepressed && aDepressed && Physics.Raycast(body.transform.position, Vector3.right, out hit, 20f, layerMask) && !Physics.Raycast(body.transform.position, -Vector3.right, out hit, 20f, layerMask))
        {
            if (dDepressed)
            {
                for (int i = 0; i < 20; i++)
                {
                    body.AddForce(-walkForce * Time.deltaTime * 0.75f, 0, 0);
                }
            }
            else
            {
                for (int i = 0; i < 20; i++)
                {
                    body.AddForce(-walkForce * Time.deltaTime * 0.5f, 0, 0);
                }
            }
        }
        else if (spaceDepressed && aDepressed && !dDepressed && Physics.Raycast(body.transform.position, Vector3.right, out hit, 20f, layerMask) && !Physics.Raycast(body.transform.position, Vector3.right, out hit, 20f, layerMask))
        {
            body.AddForce(-walkForce * Time.deltaTime * 0.75f, 0, 0);
        }
        else if (spaceDepressed && dDepressed && Physics.Raycast(body.transform.position, -Vector3.right, out hit, 20f, layerMask) && !Physics.Raycast(body.transform.position, Vector3.right, out hit, 20f, layerMask))
        {
            if (aDepressed)
            {
                for (int i = 0; i < 20; i++)
                {
                    body.AddForce(walkForce * Time.deltaTime * 0.75f, 0, 0);
                }
            }
            else
            {
                for (int i = 0; i < 20; i++)
                {
                    body.AddForce(walkForce * Time.deltaTime * 0.5f, 0, 0);
                }
            }
        }
        else if (spaceDepressed && dDepressed && !aDepressed && Physics.Raycast(body.transform.position, -Vector3.right, out hit, 20f, layerMask) && !Physics.Raycast(body.transform.position, Vector3.right, out hit, 20f, layerMask))
        {
            body.AddForce(walkForce * Time.deltaTime * 0.75f, 0, 0);
        }

        if (groundforce.touchingGround)
        {
            boostable = true;
        }

        if (groundforce.touchingObject && jumpable)
        {
            if (spaceDepressed)
            {
                audioSource.PlayOneShot(jump, SyncData.sfx / 50f * 0.2f * (Mathf.Clamp((200 - Vector3.Distance(transform.position, local.position)), 0, 200) / 200));
                jumpable = false;
                for (int i = 0; i < 15; i++)
                {
                    body.AddForce(0, (jumpForce * 0.7f - (i * 20)) * Time.deltaTime, 0);

                    lFoot.AddForce(-lFoot.transform.right * 0.025f * (jumpForce - (i * 20)) * Time.deltaTime);
                    rFoot.AddForce(rFoot.transform.right * 0.025f * (jumpForce - (i * 20)) * Time.deltaTime);

                    spaceDepressed = false;
                    groundHitL = false;
                    groundHitR = false;
                }
                StartCoroutine("jumpTimer");
                GameObject particleL = Instantiate(jumpParticle, footL.transform.position, Quaternion.identity);
                particleL.transform.parent = footL.transform;
                GameObject particleR = Instantiate(jumpParticle, footR.transform.position, Quaternion.identity);
                particleR.transform.parent = footR.transform;
            }
            if (sDepressed)
            {
                body.AddForce(0, -30000 * Time.deltaTime, 0);
                if (getDown < 60)
                {
                    lFoot.AddForce(-lFoot.transform.right * 9000 * Time.deltaTime);
                    rFoot.AddForce(rFoot.transform.right * 9000 * Time.deltaTime);
                    getDown++;
                }
            }
            if ((Physics.Raycast(rLeg.gameObject.transform.position, -Vector3.up, out hit, 5f)) && !spaceDepressed)
            {
                rLeg.AddForce(0, 3000f * Time.deltaTime, 0);

            }
        }
        if (sDepressed)
        {
            if (groundforce.grappled)
            {
                body.AddForce(0, -50000 * Time.deltaTime, 0);
            }
            else
            {
                body.AddForce(0, -30000 * Time.deltaTime, 0);
            }
        }
        if (groundforce.touchingWall && jumpable)
        {
            if (spaceDepressed)
            {
                jumpable = false;
                for (int i = 0; i < 15; i++)
                {
                    if (body.velocity.y < 15)
                    {
                        body.AddForce(0, (jumpForce * 0.75f - (i * 20)) * Time.deltaTime, 0);
                    }
                    else
                    {
                        body.AddForce(0, (jumpForce * 0.35f - (i * 20)) * Time.deltaTime, 0);
                    }

                    //body.velocity = new Vector3(body.velocity.x, body.velocity.y + 20f, 0);

                    lFoot.AddForce(lFoot.transform.up * 0.05f * (jumpForce - (i * 20)) * Time.deltaTime);
                    rFoot.AddForce(rFoot.transform.up * 0.05f * (jumpForce - (i * 20)) * Time.deltaTime);

                    spaceDepressed = false;
                }
                GameObject particleL = Instantiate(jumpParticle, footL.transform.position, Quaternion.identity);
                particleL.transform.parent = footL.transform;
                GameObject particleR = Instantiate(jumpParticle, footR.transform.position, Quaternion.identity);
                particleR.transform.parent = footR.transform;
                StartCoroutine("wallTimer");
            }
        }
        if (groundforce.touchingWall || groundforce.touchingObject)
        {
            jumping = true;
        }
        if (!groundforce.touchingWall && !groundforce.touchingObject) //Add raycast so it doesnt apply imediatley!!!!!!!!!!!
        {
            inAir = true;
            spaceDepressed = false;
        }
        else if (groundforce.touchingWall || groundforce.touchingObject)
        {
            inAir = false;
        }
        if (inAir)
        {
            jumping = false;
            if (body.velocity.y > -25f)
            {
                foreach (Rigidbody rb in downForceLimbs)
                {
                    rb.AddForce(0, -35f * Time.deltaTime * 100f, 0);
                }
            }
        }
    }

    IEnumerator jumpTimer()
    {
        yield return new WaitForSeconds(0.05f);
        jumpable = true;
    }
    IEnumerator wallTimer()
    {
        yield return new WaitForSeconds(0.4f);
        jumpable = true;
    }

    IEnumerator fireOffTimer()
    {
        yield return new WaitForSeconds(0.5f);
        if (keysTouched)
        {
            keysTouched = false;
            fire = true;
        }
        else
        {
            fire = false;
        }
    }
}
