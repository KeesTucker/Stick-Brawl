using UnityEngine;
using System.Collections;
using Mirror;

public class PlayerSetupAI : NetworkBehaviour
{
    public Collider[] colliders;

    Camera sceneCamera;

    [SyncVar]
    public GameObject parent;

    public GameObject ragdollPlaceholder;

    void Start()
    {
        if (parent)
        {
            
        }
        for (int i = 0; i < colliders.Length; i++)
        {
            for (int v = 0; v < colliders.Length; v++)
            {
                Physics.IgnoreCollision(colliders[i], colliders[v]);
            }
        }
    }

    public override void OnStartAuthority()
    {
        if (!hasAuthority)
        {
            ragdollPlaceholder.SetActive(true);
        }
        else
        {
            ragdollPlaceholder.gameObject.SetActive(false);
            /*sceneCamera = Camera.main;
            gameObject.name = "Local";
            if (sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }
            charCam.gameObject.SetActive(true);*/
        }
    }

    /*void Update()
    {
        if (isLocalPlayer)
        {
            CmdSyncJet(GameObject.Find("Local").transform.Find("Physics Animator").GetComponent<PlayerMovement>().jetOn);
        }
    }

    [Command]
    void CmdSyncJet(bool jetState)
    {
        jetOn = jetState;
    }
    */
    /*void OnDisable()
    {
        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }
    }*/

}
