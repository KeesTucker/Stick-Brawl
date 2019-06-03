using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerConnection : NetworkBehaviour {

	// Use this for initialization
	void Start () {
        if (isLocalPlayer)
        {
            gameObject.name = "LocalConnection";
        }	
	}

}
