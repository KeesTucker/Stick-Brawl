using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SpawnRotHolder : NetworkBehaviour {
    [SerializeField]
    GameObject RotHolder;
    public GameObject currentSpawn;

    public static uint parentID;
    // Use this for initialization
    void Start() {
        if (isServer)
        {
            parentID = this.netId;
            CmdSpawn();
        }
    }
    [Command]
    void CmdSpawn()
    {
        currentSpawn = Instantiate(RotHolder, new Vector3(0, 0, 0), transform.rotation);
        currentSpawn.GetComponent<SyncRotation>().parent = gameObject;
        currentSpawn.GetComponent<ReadRotation>().parent = gameObject;
        NetworkServer.Spawn(currentSpawn);
    }
    [Command]
    public void CmdAssignAuthority(NetworkIdentity identity)
    {
        NetworkConnection currentOwner = identity.clientAuthorityOwner;
        if (currentOwner == connectionToClient)
        {
            return;
        }
        else
        {
            if (currentOwner != null)
            {
                identity.RemoveClientAuthority(currentOwner);
            }
            identity.AssignClientAuthority(connectionToClient);
        }
    }
}
