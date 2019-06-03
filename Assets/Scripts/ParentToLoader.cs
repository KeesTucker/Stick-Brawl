using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ParentToLoader : NetworkBehaviour {
    public GameObject loaderP;
	// Use this for initialization
	void Start () {
        GameObject loader = Instantiate(loaderP, new Vector3(0, 0, 0), Quaternion.identity);
        loader.transform.parent = transform.parent;
        transform.parent = loader.transform;
    }
}
