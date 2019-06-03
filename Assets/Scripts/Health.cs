using UnityEngine;
using Mirror;

public class Health : NetworkBehaviour {

    [SyncVar]
    public float health = 200;

    [SyncVar]
    public bool localComplete = false;

    GameObject[] RotHolders;

    public GameObject Ghost;
    public GameObject currentSpawn;

    // Update is called once per frame
    void Update () {
        if (health <= 0 && isLocalPlayer)
        {
            Destroy(GameObject.Find("Local"));
            Destroy(GameObject.Find("Items"));
            RotHolders = GameObject.FindGameObjectsWithTag("RagAng");
            foreach(GameObject RotHolder in RotHolders)
            {
                if (RotHolder.GetComponent<SyncRotation>().parent == gameObject)
                {
                    CmdDestroyOnServer(RotHolder);
                }
            }
            CmdLocalComplete();
        }
        if (isServer && health <= 0 && localComplete)
        {
            CmdSpawn();
        }
    }

    [Command]
    public void CmdLocalComplete()
    {
        localComplete = true;
    }

    [Command]
    public void CmdUpdateHealth(float damage)
    {
        health -= damage;
    }

    [Command]
    public void CmdDestroyOnServer(GameObject gameObject)
    {
        Destroy(gameObject);
    }

    [Command]
    void CmdSpawn()
    {
        currentSpawn = Instantiate(Ghost, transform.position, Quaternion.identity);
        currentSpawn.GetComponent<GhostMovement>().parent = gameObject;
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
        CmdDestroyOnServer(gameObject);
    }
}
