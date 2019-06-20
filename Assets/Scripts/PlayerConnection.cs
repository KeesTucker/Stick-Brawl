using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerConnection : NetworkBehaviour {

	// Use this for initialization
	void Start () {
        if (isLocalPlayer)
        {
            if (GetComponent<PlayerManagement>().isLobbyPlayer)
            {
                gameObject.name = "LocalConnectionG";
            }
            else
            {
                gameObject.name = "LocalConnection";
            }
        }	
	}

}
