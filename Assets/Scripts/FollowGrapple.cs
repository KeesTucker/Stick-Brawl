using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowGrapple : MonoBehaviour {
    public GameObject parent;
	// Use this for initialization
	void Start () {
        gameObject.GetComponent<MeshRenderer>().material.color = parent.GetComponent<renderGrapple>().color;
	}
	
	void FixedUpdate () {
        if (!parent)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position = parent.transform.position;
        }
	}
}
