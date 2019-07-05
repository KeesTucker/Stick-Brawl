using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementJoyStick : MonoBehaviour
{
    public VariableJoystick joystick;

    PlayerControl playerControl;

    private IEnumerator Start()
    {
        while (playerControl == null)
        {
            if (GameObject.Find("LocalPlayer"))
            {
                playerControl = GameObject.Find("LocalPlayer").GetComponent<PlayerControl>();
            }
            yield return null;
        }
    }

    void Update()
    {
        if (playerControl)
        {
            if (joystick.Direction.x == 0)
            {
                playerControl.d = false;
                playerControl.a = false;
            }
            else if (joystick.Direction.x > 0)
            {
                playerControl.d = true;
                playerControl.a = false;
            }
            else
            {
                playerControl.d = false;
                playerControl.a = true;
            }

            if (joystick.Direction.y > 0.6f)
            {
                playerControl.space = true;
                playerControl.s = false;
            }
            else if (joystick.Direction.y < -0.7f)
            {
                playerControl.space = false;
                playerControl.s = true;
            }
            else
            {
                playerControl.space = false;
                playerControl.s = false;
            }
        }
    }
}
