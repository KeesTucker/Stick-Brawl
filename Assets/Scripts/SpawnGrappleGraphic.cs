using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGrappleGraphic : MonoBehaviour {

    public GameObject grappleGraphic;

	// Use this for initialization
	void Start () {
        GameObject grapple = Instantiate(grappleGraphic, transform.position, transform.rotation);
        grapple.GetComponent<FollowGrapple>().parent = gameObject;
	}
}
