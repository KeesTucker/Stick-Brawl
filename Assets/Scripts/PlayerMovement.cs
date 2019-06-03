using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
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
    public bool groundHitL;
    public bool groundHitR;
    public bool jumpable = true;
    public bool boostable = true;
    private bool jetTimed = false;

    public bool jetOn;

    public int getDown = 0;

    public int jetCounter = 0;

    public groundForce groundforce;

    public bool unCrouch;

    public Rigidbody Ragdoll;

    public float walkForce = 40000f;
    public float jumpForce = 40000f;

    public bool grappleSince = false;

    Animator anim;
    int walkingR = Animator.StringToHash("WalkingR");
    int walkingL = Animator.StringToHash("WalkingL");
    private RaycastHit hit;

    Shoot shoot;

    public bool hasJetPack = false;

    public float maxSpeed = 45;

    public bool fire;

    bool keysTouched;

    public int state;

    public SyncMoveState syncMoveState;

    void Start()
    {
        Ragdoll = GameObject.Find("Local/Ragdoll").GetComponent<Rigidbody>();

        groundforce = GameObject.Find("Ragdoll").GetComponent<groundForce>();
        anim = GetComponent<Animator>();

        jetMaterialL = jetFlashL.GetComponent<Renderer>().material;
        jetMaterialL.SetFloat("Vector1_B173D9FB", 0);
        jetMaterialR = jetFlashR.GetComponent<Renderer>().material;
        jetMaterialR.SetFloat("Vector1_B173D9FB", 0);

        shoot = transform.parent.Find("Ragdoll/ULRA/LLRA/Rotation Gun Manager/Weapon").GetComponent<Shoot>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey("a"))
        {
            aDepressed = true;
        }
        else
        {
            aDepressed = false;
        }

        if (Input.GetKey("d"))
        {
            dDepressed = true;
        }
        else
        {
            dDepressed = false;
        }

        if (Input.GetKeyDown("s"))
        {
            sDepressed = true;
        }
        else if(Input.GetKeyUp("s"))
        {
            sDepressed = false;
            getDown = 0;
        }

        if (Input.GetKeyDown("space"))
        {
            spaceDepressed = true;
            spaceJetDepressed = true;
        }
        if (Input.GetKeyUp("space"))
        {
            spaceDepressed = false;
            spaceJetDepressed = false;
        }

        if (Input.GetKey("left shift"))
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
            if (shiftDepressed == true)
            {
                lFoot.AddForce(-walkForce * Time.deltaTime * 0.85f, walkForce * Time.deltaTime * 0.05f, 0);
                rFoot.AddForce(-walkForce * Time.deltaTime * 0.85f, walkForce * Time.deltaTime * 0.05f, 0);
                body.AddForce(-walkForce * Time.deltaTime * 1f, 0, 0);
                anim.SetTrigger(walkingL);
                state = 1;
                syncMoveState.CmdSetState(state);
                anim.ResetTrigger("stand");
                anim.ResetTrigger("WalkingR");
            }

            else
            {
                lFoot.AddForce(-walkForce * Time.deltaTime * 0.45f, walkForce * Time.deltaTime * 0.05f, 0);
                rFoot.AddForce(-walkForce * Time.deltaTime * 0.45f, walkForce * Time.deltaTime * 0.05f, 0);
                body.AddForce(-walkForce * Time.deltaTime * 1f, 0, 0);
                anim.SetTrigger(walkingL);
                state = 1;
                syncMoveState.CmdSetState(state);
                anim.ResetTrigger("stand");
                anim.ResetTrigger("WalkingR");
            }
        }
        else if (aDepressed == true)
        {
            body.AddForce(-walkForce * Time.deltaTime * 1f, 0, 0);
            anim.SetTrigger(walkingL);
            state = 1;
            syncMoveState.CmdSetState(state);
            anim.ResetTrigger("stand");
            anim.ResetTrigger("WalkingR");
        }
        else if (dDepressed == true && (groundHitL == true || groundHitR == true))
        {
            if (shiftDepressed == true)
            {
                lFoot.AddForce(walkForce * Time.deltaTime * 0.85f, walkForce * Time.deltaTime * 0.05f, 0);
                rFoot.AddForce(walkForce * Time.deltaTime * 0.85f, walkForce * Time.deltaTime * 0.05f, 0);
                body.AddForce(walkForce * Time.deltaTime * 1f, 0, 0);
                anim.SetTrigger(walkingR);
                state = 0;
                syncMoveState.CmdSetState(state);
                anim.ResetTrigger("stand");
                anim.ResetTrigger("WalkingL");
            }

            else
            {
                lFoot.AddForce(walkForce * Time.deltaTime * 0.45f, walkForce * Time.deltaTime * 0.05f, 0);
                rFoot.AddForce(walkForce * Time.deltaTime * 0.45f, walkForce * Time.deltaTime * 0.05f, 0);
                body.AddForce(walkForce * Time.deltaTime * 1f, 0, 0);
                anim.SetTrigger(walkingR);
                state = 0;
                syncMoveState.CmdSetState(state);
                anim.ResetTrigger("stand");
                anim.ResetTrigger("WalkingL");
            }
        }
        else if (dDepressed == true)
        {
            body.AddForce(walkForce * Time.deltaTime * 1f, 0, 0);
            anim.SetTrigger(walkingR);
            state = 0;
            syncMoveState.CmdSetState(state);
            anim.ResetTrigger("stand");
            anim.ResetTrigger("WalkingL");
        }
        else
        {
            anim.ResetTrigger("WalkingR");
            anim.ResetTrigger("WalkingL");
            anim.SetTrigger("stand");
            state = 2;
            syncMoveState.CmdSetState(state);

        }

        if (groundforce.touchingGround)
        {
            boostable = true;
        }

        if (groundforce.touchingObject && jumpable)
        {
            if (spaceDepressed)
            {
                jumpable = false;
                for (int i = 0; i < 15; i++)
                {
                    body.AddForce(0, (jumpForce * 0.5f - (i * 20)) * Time.deltaTime, 0);
                    
                    lFoot.AddForce(-lFoot.transform.right * 0.1f * (jumpForce - (i * 20)) * Time.deltaTime);
                    rFoot.AddForce(rFoot.transform.right * 0.1f * (jumpForce - (i * 20)) * Time.deltaTime);

                    spaceDepressed = false;
                    groundHitL = false;
                    groundHitR = false;
                }
                StartCoroutine("jumpTimer");
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
        else if (spaceJetDepressed && boostable && spaceDepressed && hasJetPack)
        {
            jetpack.AddForce(jetpack.transform.up * jumpForce * Time.deltaTime * 0.3f);
            jetTimer();
            if (!jetTimed)
            {
                jetTimed = true;
            }
            jetOn = true;
            jetMaterialL.SetFloat("Vector1_B173D9FB", 1);
            jetMaterialR.SetFloat("Vector1_B173D9FB", 1);
        }
        else
        {
            jetOn = false;
            jetMaterialL.SetFloat("Vector1_B173D9FB", 0);
            jetMaterialR.SetFloat("Vector1_B173D9FB", 0);
        }
    }

    IEnumerator jumpTimer()
    {
        yield return new WaitForSeconds(0.001f);
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

    public void jetTimer()
    {
        Debug.Log(jetCounter);
        jetCounter++;
        if (jetCounter > 100)
        {
            jetCounter = 0;
            boostable = false;
            jetTimed = false;
            jetMaterialL.SetFloat("Vector1_B173D9FB", 0);
            jetMaterialR.SetFloat("Vector1_B173D9FB", 0);
        }
    }

}
