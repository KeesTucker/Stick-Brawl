using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerControl : NetworkBehaviour
{
    public bool e;
    public bool a;
    public bool aLast;
    public bool s;
    public bool sLast;
    public bool d;
    public bool dLast;
    public bool space;
    public bool r;
    public bool shift;
    public bool shiftLast;
    public bool lClick;
    public bool lClickLast;
    public bool rClick;
    public bool rClickLast;
    public bool one;
    public bool two;
    public bool three;
    public bool four;
    public bool i;

    public float scroll = 1;
    public bool oneDone = false;

    public bool spaceDone;

    public ItemCheck itemCheck;
    public AimShootAI aimShoot;
    public PlayerMovementAI playerMovement;
    public ShootAI shoot;
    public RefrenceKeeperAI refrenceKeeper;
    public SpawnRocketAI spawnRocket;

    void Start()
    {
        shift = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (refrenceKeeper.weaponInventory.Count < 1)
        {
            e = true;
        }
        if (hasAuthority)
        {
            if (gameObject.GetComponent<SpawnRocketAI>().spaceDepressed)
            {
                if (Input.GetKeyDown(SyncData.f))
                {
                    e = true;
                }
                if (Input.GetKeyDown(SyncData.a))
                {
                    a = true;
                }
                if (Input.GetKeyUp(SyncData.a))
                {
                    a = false;
                }
                if (Input.GetKeyDown(SyncData.s))
                {
                    s = true;
                }
                if (Input.GetKeyUp(SyncData.s))
                {
                    s = false;
                }
                if (Input.GetKeyDown(SyncData.d))
                {
                    d = true;
                }
                if (Input.GetKeyUp(SyncData.d))
                {
                    d = false;
                }
                if (Input.GetKeyDown(SyncData.r))
                {
                    r = true;
                }
                if (Input.GetKeyDown("1"))
                {
                    one = true;
                    scroll = 1;
                }
                if (Input.GetKeyDown("2"))
                {
                    two = true;
                    scroll = 2;
                }
                if (Input.GetKeyDown("3"))
                {
                    three = true;
                    scroll = 3;
                }
                if (Input.GetKeyDown("4"))
                {
                    four = true;
                    scroll = 4;
                }

                if (Input.mouseScrollDelta.y != 0)
                {
                    scroll -= Input.mouseScrollDelta.y * 1f;
                    scroll = Mathf.Clamp(scroll, 1, 4);
                    if ((int)scroll == 1 && !oneDone)
                    {
                        one = true;
                        oneDone = true;
                    }
                    if ((int)scroll == 2)
                    {
                        two = true;
                        oneDone = false;
                    }
                    if ((int)scroll == 3)
                    {
                        three = true;
                        oneDone = false;
                    }
                    if ((int)scroll == 4 && !oneDone)
                    {
                        four = true;
                        oneDone = true;
                    }
                }

                if (Input.GetMouseButtonDown(0))
                {
                    //lClick = true;
                }
                if (Input.GetMouseButtonUp(0))
                {
                    //lClick = false;
                }
                if (Input.GetMouseButtonDown(1))
                {
                    //rClick = true;
                }
                if (Input.GetMouseButtonUp(1))
                {
                    //rClick = false;
                }
                if (Input.GetKeyDown(SyncData.i))
                {
                    i = true;
                }
                if (Input.GetKeyUp(SyncData.i))
                {
                    i = false;
                }
            }
            
            if (Input.GetKeyDown(SyncData.space))
            {
                if (spawnRocket.ready)
                {
                    space = true;
                }
            }
            /*if (Input.GetKeyDown("left shift") || Input.GetKeyDown("right shift"))
            {
                shift = true;
            }
            if (Input.GetKeyUp("left shift") || Input.GetKeyUp("right shift"))
            {
                shift = false; //Just so always running
            }*/

            if (e)
            {
                e = false;
                CmdSetKey("e", true);
            }
            if (a != aLast)
            {
                aLast = a;
                CmdSetKey("a", a);
            }
            if (d != dLast)
            {
                dLast = d;
                CmdSetKey("d", d);
            }
            if (s != sLast)
            {
                sLast = s;
                CmdSetKey("s", s);
            }
            if (space)
            {
                space = false;
                if (!spaceDone)
                {
                    CmdSetKey("spaceFirst", true);
                    spaceDone = true;
                }
                CmdSetKey("space", true);
            }
            if (shift != shiftLast)
            {
                shiftLast = shift;
                CmdSetKey("shift", shift);
            }
            if (r)
            {
                r = false;
                CmdSetKey("r", true);
            }
            if (rClick != rClickLast)
            {
                rClickLast = rClick;
                CmdSetKey("rClick", rClick);
            }
            if (lClick != lClickLast)
            {
                lClickLast = lClick;
                CmdSetKey("lClick", lClick);
            }
            if (one)
            {
                one = false;
                CmdSetKey("one", true);
            }
            if (two)
            {
                two = false;
                CmdSetKey("two", true);
            }
            if (three)
            {
                three = false;
                CmdSetKey("three", true);
            }
            if (four)
            {
                four = false;
                CmdSetKey("four", true);
            }
            if (i)
            {
                refrenceKeeper.updateUI.OpenClose();
                i = false;
            }
        }
    }

    [Command]
    public void CmdSetKey(string key, bool state)
    {
        RpcSetKey(key, state);
        if (key == "e")
        {
            itemCheck.e = state;
        }
        if (key == "a")
        {
            playerMovement.a = state;
        }
        if (key == "d")
        {
            playerMovement.d = state;
        }
        if (key == "s")
        {
            playerMovement.s = state;
        }
        if (key == "space")
        {
            playerMovement.space = state;
        }
        if (key == "shift")
        {
            playerMovement.shift = state;
        }
        if (key == "r")
        {
            shoot.r = state;
        }
        if (key == "lClick")
        {
            shoot.lClick = state;
        }
        if (key == "rClick")
        {
            aimShoot.RClick = state;
        }
        if (key == "one")
        {
            refrenceKeeper.one = state;
        }
        if (key == "two")
        {
            refrenceKeeper.two = state;
        }
        if (key == "three")
        {
            refrenceKeeper.three = state;
        }
        if (key == "four")
        {
            refrenceKeeper.four = state;
        }
        if (key == "spaceFirst")
        {
            spawnRocket.AISpace = state;
        }
    }

    [ClientRpc]
    public void RpcSetKey(string key, bool state)
    {
        if (key == "e")
        {
            itemCheck.e = state;
        }
        if (key == "a")
        {
            playerMovement.a = state;
        }
        if (key == "d")
        {
            playerMovement.d = state;
        }
        if (key == "s")
        {
            playerMovement.s = state;
        }
        if (key == "space")
        {
            playerMovement.space = state;
        }
        if (key == "r")
        {
            shoot.r = state;
        }
        if (key == "lClick")
        {
            shoot.lClick = state;
        }
        if (key == "rClick")
        {
            aimShoot.RClick = state;
        }
        if (key == "one")
        {
            refrenceKeeper.one = state;
        }
        if (key == "two")
        {
            refrenceKeeper.two = state;
        }
        if (key == "three")
        {
            refrenceKeeper.three = state;
        }
        if (key == "four")
        {
            refrenceKeeper.four = state;
        }
        if (key == "spaceFirst")
        {
            spawnRocket.AISpace = state;
        }
    }
}
