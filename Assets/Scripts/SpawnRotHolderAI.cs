using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SpawnRotHolderAI : NetworkBehaviour
{
    [SerializeField]
    GameObject RotHolder;
    public GameObject currentSpawn;

    public static uint parentID;
    // Use this for initialization
    public override void OnStartAuthority()
    {
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
        currentSpawn.GetComponent<SyncRotationAI>().parent = gameObject;
        currentSpawn.GetComponent<ReadRotationAI>().parent = gameObject;
        NetworkServer.Spawn(currentSpawn);
    }
    [Command]
    public void CmdAssignAuthority(NetworkIdentity identity)
    {
        NetworkConnection currentOwner = identity.clientAuthorityOwner;
        if (currentOwner != null)
        {
            identity.RemoveClientAuthority(currentOwner);
        }
        identity.AssignClientAuthority(GameObject.Find("LocalRelay").GetComponent<PlayerSetup>().connectionToClient);
    }
}
