using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SyncMoveStateAI : NetworkBehaviour
{

    [SyncVar]
    public int moveState;

    [SyncVar]
    public bool armState;

    [SyncVar]
    public bool grappleState;

    public Animator anim;

    int walkingR = Animator.StringToHash("WalkingR");
    int walkingL = Animator.StringToHash("WalkingL");

    public GroundForceAI groundForce;

    public AimShootAI aimShoot;

    public PlayerMovementAI playerMovement;

    public GameObject parent;

    [SerializeField]
    public GameObject[] playerParts;

    [SerializeField]
    public GameObject[] playerArms;

    [SerializeField]
    public GameObject[] playerArmGrapples;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.3f);
        if (hasAuthority)
        {
            parent = GetComponent<PlayerSetupAI>().parent;
            playerMovement = parent.transform.Find("Physics AnimatorAI").GetComponent<PlayerMovementAI>();
            groundForce = parent.transform.Find("RagdollAI").GetComponent<GroundForceAI>();
            aimShoot = parent.transform.Find("RagdollAI").GetComponent<AimShootAI>();
        }
    }

    [Command]
    public void CmdSetArmGrappleState(bool state)
    {
        grappleState = state;
        RpcSetArmGrapple();
    }

    [ClientRpc]
    public void RpcSetArmGrapple()
    {
        if (grappleState)
        {
            foreach (GameObject playerArmGrapple in playerArmGrapples)
            {
                playerArmGrapple.GetComponent<HingeJoint>().useSpring = true;
            }
        }
        else
        {
            foreach (GameObject playerArmGrapple in playerArmGrapples)
            {
                playerArmGrapple.GetComponent<HingeJoint>().useSpring = false;
            }
        }
    }

    [Command]
    public void CmdSetArmState(bool state)
    {
        armState = state;
        RpcSetArm();
    }

    [ClientRpc]
    public void RpcSetArm()
    {
        if (armState)
        {
            foreach (GameObject playerArm in playerArms)
            {
                playerArm.GetComponent<HingeJoint>().useSpring = true;
            }
        }
        else
        {
            foreach (GameObject playerArm in playerArms)
            {
                playerArm.GetComponent<HingeJoint>().useSpring = false;
            }
        }
    }

    [Command]
    public void CmdSetState(int state)
    {
        moveState = state;
        RpcSetAnimation();
    }

    [ClientRpc]
    public void RpcSetAnimation()
    {
        if (!hasAuthority)
        {
            if (moveState == 0)
            {
                anim.SetTrigger(walkingR);
                anim.ResetTrigger("stand");
                anim.ResetTrigger("WalkingL");
            }
            else if (moveState == 1)
            {
                anim.SetTrigger(walkingL);
                anim.ResetTrigger("stand");
                anim.ResetTrigger("WalkingR");
            }
            else if (moveState == 2)
            {
                anim.SetTrigger("stand");
                anim.ResetTrigger("WalkingL");
                anim.ResetTrigger("WalkingR");
            }

            if (moveState == 3)
            {
                foreach (GameObject playerPart in playerParts)
                {
                    playerPart.GetComponent<HingeJoint>().useSpring = false;
                }
            }
            else
            {
                foreach (GameObject playerPart in playerParts)
                {
                    playerPart.GetComponent<HingeJoint>().useSpring = true;
                }
            }
        }
    }
}
