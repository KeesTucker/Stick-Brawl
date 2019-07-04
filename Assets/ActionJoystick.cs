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

    void Update()
    {
        aim.position = joystick.Direction * 100f + new Vector2(player.position.x, player.position.y);

        if (Vector2.Distance(joystick.Direction, Vector2.zero) > 0.98f)
        {
            playerControl.rClick = true;
            playerControl.lClick = false;
        }
        else if (Vector2.Distance(joystick.Direction, Vector2.zero) > 0.4f)
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
