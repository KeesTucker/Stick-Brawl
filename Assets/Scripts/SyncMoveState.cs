using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SyncMoveState : NetworkBehaviour {

    [SyncVar]
    public int moveState;

    [SyncVar]
    public bool armState;

    [SyncVar]
    public bool grappleState;

    public Animator anim;

    int walkingR = Animator.StringToHash("WalkingR");
    int walkingL = Animator.StringToHash("WalkingL");

    groundForce _groundForce;

    AimShoot aimShoot;

    [SerializeField]
    public GameObject[] playerParts;

    [SerializeField]
    public GameObject[] playerArms;

    [SerializeField]
    public GameObject[] playerArmsGrapple;

    public ID childRag;

    void Start()
    {
        playerParts = GameObject.FindGameObjectsWithTag("Floppy");
        playerArms = GameObject.FindGameObjectsWithTag("Arms");
        playerArmsGrapple = GameObject.FindGameObjectsWithTag("GrappleArms");
        if (isLocalPlayer)
        {
            GameObject.Find("Local/Physics Animator").GetComponent<PlayerMovement>().syncMoveState = this;
            GameObject.Find("Local/Ragdoll").GetComponent<groundForce>().syncMoveState = this;
            aimShoot = GameObject.Find("Local/Ragdoll").GetComponent<AimShoot>();
            _groundForce = GameObject.Find("Local/Ragdoll").GetComponent<groundForce>();
            aimShoot.syncMoveState = this;
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
            foreach (GameObject playerArmGrapple in playerArmsGrapple)
            {
                if (playerArmGrapple != null)
                {
                    if (playerArmGrapple.activeSelf && playerArmGrapple.activeInHierarchy)
                    {
                        if (playerArmGrapple.GetComponent<ID>().IDPlayer == childRag.IDPlayer)
                        {
                            playerArmGrapple.GetComponent<HingeJoint>().useSpring = true;
                        }
                    }
                }
            }
        }
        else
        {
            foreach (GameObject playerArmGrapple in playerArmsGrapple)
            {
                if (playerArmGrapple != null)
                {
                    if (playerArmGrapple.activeSelf && playerArmGrapple.activeInHierarchy)
                    {
                        if (playerArmGrapple.GetComponent<ID>().IDPlayer == childRag.IDPlayer)
                        {
                            playerArmGrapple.GetComponent<HingeJoint>().useSpring = false;
                        }
                    }
                }
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
                if (playerArm != null)
                {
                    if (playerArm.activeSelf && playerArm.activeInHierarchy)
                    {
                        if (playerArm.GetComponent<ID>().IDPlayer == childRag.IDPlayer)
                        {
                            playerArm.GetComponent<HingeJoint>().useSpring = true;
                        }
                    }
                }
            }
        }
        else
        {
            foreach (GameObject playerArm in playerArms)
            {
                if (playerArm != null)
                {
                    if (playerArm.activeSelf && playerArm.activeInHierarchy)
                    {
                        if (playerArm.GetComponent<ID>().IDPlayer == childRag.IDPlayer)
                        {
                            playerArm.GetComponent<HingeJoint>().useSpring = false;
                        }
                    }
                }
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
        if (!isLocalPlayer)
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
                    if (playerPart != null)
                    {
                        if (playerPart.activeSelf && playerPart.activeInHierarchy)
                        {
                            if (playerPart.GetComponent<ID>().IDPlayer == childRag.IDPlayer)
                            {
                                playerPart.GetComponent<HingeJoint>().useSpring = false;
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (GameObject playerPart in playerParts)
                {
                    if (playerPart != null)
                    {
                        if (playerPart.activeSelf && playerPart.activeInHierarchy)
                        {
                            if (playerPart.GetComponent<ID>().IDPlayer == childRag.IDPlayer)
                            {
                                playerPart.GetComponent<HingeJoint>().useSpring = true;
                            }
                        }
                    }
                }
            }
        }
    }
}
