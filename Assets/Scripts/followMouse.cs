using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followMouse : MonoBehaviour {
    public Camera cam;

	// Update is called once per frame
	void FixedUpdate () {
        if (cam)
        {
            transform.position = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, 0);
        }
        else if (transform.parent) {
            if (Camera.main && transform.parent.GetComponent<AISetup>().hasAuthority)
            {
                cam = Camera.main;
            }
        }
    }
}
