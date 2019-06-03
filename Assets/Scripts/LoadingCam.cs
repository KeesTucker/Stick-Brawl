using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingCam : MonoBehaviour {

    public Transform aim;

    public GameObject cam;

    public GameObject cameraGO;

    void Start()
    {
        if (gameObject.name == "LoadingPlayer")
        {
            cameraGO = Instantiate(cam, transform.position, Quaternion.identity);
            cameraGO.GetComponent<CamFollowAI>().parent = transform;
            cameraGO.GetComponent<CamFollowAI>().aim = aim;
            aim.gameObject.GetComponent<followMouse>().cam = cameraGO.GetComponent<Camera>();
            cameraGO.transform.GetChild(0).gameObject.GetComponent<ColorBack>().isLoading = true;
        }
    }
}
