using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingControl : MonoBehaviour
{
    public bool a;
    public bool aLast;
    public bool s;
    public bool sLast;
    public bool d;
    public bool dLast;
    public bool space;
    public bool shift;
    public bool shiftLast;
    public bool lClick;
    public bool lClickLast;
    public bool rClick;
    public bool rClickLast;

    public AimShootAI aimShoot;
    public PlayerMovementAI playerMovement;
    public ShootAI shoot;

    void Start()
    {
        shift = true;

        if (Random.Range(0, 2) == 2)
        {
            a = true;
        }
        else
        {
            d = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        int action = Random.Range(0, 50);
        if (action == 30 || action == 20)
        {
            if (transform.position.x > 0)
            {
                a = true;
                d = false;
            }
            else
            {
                d = true;
                a = false;
            }
        }
        else if (action == 10)
        {
            space = true;
        }

        if (a != aLast)
        {
            aLast = a;
            SetKey("a", a);
        }
        if (d != dLast)
        {
            dLast = d;
            SetKey("d", d);
        }
        if (s != sLast)
        {
            sLast = s;
            SetKey("s", s);
        }
        if (space)
        {
            space = false;
            SetKey("space", true);
        }
        if (shift != shiftLast)
        {
            shiftLast = shift;
            SetKey("shift", shift);
        }
        if (rClick != rClickLast)
        {
            rClickLast = rClick;
            SetKey("rClick", rClick);
        }
        if (lClick != lClickLast)
        {
            lClickLast = lClick;
            SetKey("lClick", lClick);
        }
    }

    public void SetKey(string key, bool state)
    {
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
        if (key == "lClick")
        {
            shoot.lClick = state;
        }
        if (key == "rClick")
        {
            aimShoot.RClick = state;
        }
    }
}
