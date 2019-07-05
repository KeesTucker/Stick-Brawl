using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionJoystick : MonoBehaviour
{
    public VariableJoystick joystick;

    PlayerControl playerControl;

    Transform aim;

    Transform player;

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
        aim = GameObject.Find("LocalPlayer/AIAim").transform;
        player = playerControl.transform;
    }

    public void Pickup()
    {
        playerControl.e = true;
    }

    void Update()
    {
        if (playerControl && aim)
        {
            if (Vector2.Distance(joystick.Direction, Vector2.zero) > 0.1f)
            {
                aim.position = joystick.Direction * 75f + new Vector2(player.position.x, player.position.y);
            }

            if (Vector2.Distance(joystick.Direction, Vector2.zero) > 0.99f)
            {
                playerControl.rClick = true;
                playerControl.lClick = false;
            }
            else if (Vector2.Distance(joystick.Direction, Vector2.zero) > 0.2f)
            {
                playerControl.rClick = false;
                playerControl.lClick = true;
            }
            else
            {
                playerControl.lClick = false;
                playerControl.rClick = false;
            }
        }
    }
}
