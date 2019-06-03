using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindCam : MonoBehaviour {

    public Canvas canvas;

    public Camera cam;
	
	// Update is called once per frame
	void Update () {
        if (canvas.worldCamera == null)
        {
            if (GameObject.Find("Main Camera(Clone)"))
            {
                cam = GameObject.Find("Main Camera(Clone)").GetComponent<Camera>();
                canvas.worldCamera = cam;
            }
        }
	}
}
