using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DeleteColliders : NetworkBehaviour {

    public GameObject local;

	// Use this for initialization
	IEnumerator Start () {
        yield return new WaitForSeconds(5f);
        if (isServer)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
	}
}
