using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CamControl : NetworkBehaviour
{

    public Transform aim;

    public GameObject cam;

    public GameObject cameraGO;

    // Use this for initialization
    public override void OnStartAuthority()
    {
        if (hasAuthority)
        {
            cameraGO = Instantiate(cam, transform.position, Quaternion.identity);
            cameraGO.GetComponent<CamFollowAI>().parent = transform;
            cameraGO.GetComponent<CamFollowAI>().aim = aim;
            aim.gameObject.GetComponent<followMouse>().cam = cameraGO.GetComponent<Camera>();
            if (gameObject.name != "LoadingPlayer")
            {
                cameraGO.GetComponent<SpawnBackgrounds>().inLoadScene = false;
            }
        }
    }
}
