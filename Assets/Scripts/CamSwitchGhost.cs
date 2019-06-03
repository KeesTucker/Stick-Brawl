using Mirror;
using System.Collections;
using UnityEngine;

public class CamSwitchGhost : NetworkBehaviour {

    public Camera cam;
    public bool done = false;
	// Use this for initialization
	void Update () {
        if (hasAuthority && !done)
        {
            GameObject.Find("MouseFollower").GetComponent<followMouse>().cam = cam;
            done = true;
        }
	}
}
