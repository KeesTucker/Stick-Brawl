using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostSwitch : MonoBehaviour {

    public StartGame startGame;
    public Rigidbody rb;
	// Update is called once per frame
	void Update () {
        if (rb.angularVelocity.z != 0)
        {
            if (transform.localRotation.eulerAngles.z > 335)
            {
                startGame.isHost = false;
            }
            else if (transform.localRotation.eulerAngles.z < 295)
            {
                startGame.isHost = true;
            }
        }
	}
}
