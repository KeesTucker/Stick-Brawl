using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AISetup : NetworkBehaviour
{

    public Collider[] colliders;

    [SyncVar]
    public GameObject parent;

    public bool local = false;

    public NetworkManager manager;

    public HealthAI health;

    public PlayerManagement playerManagement;

    public bool dead = false;

    public GameObject nameTag;

    public bool stop;

    public SpawnRocketAI spawnRocket;

    // Use this for initialization
    void Start()
    {
        spawnRocket = GetComponent<SpawnRocketAI>();
        manager = GameObject.Find("_NetworkManager").GetComponent<NetworkManager>();
        for (int i = 0; i < colliders.Length; i++)
        {
            for (int v = 0; v < colliders.Length; v++)
            {
                Physics.IgnoreCollision(colliders[i], colliders[v]);
            }
        }
        if (isServer)
        {
            playerManagement = GameObject.Find("LocalConnection").GetComponent<PlayerManagement>();
        }
        else
        {
            playerManagement = GameObject.Find("PlayerConnect(Clone)").GetComponent<PlayerManagement>();
        }
        playerManagement.totalPlayers++;
    }

    public override void OnStartAuthority()
    {
        if (hasAuthority)
        {
            manager = GameObject.Find("_NetworkManager").GetComponent<NetworkManager>();
            if (SyncData.gameMode == 1)
            {
                if (GameObject.Find("Player(Clone)").GetComponent<SpawnRocketAI>().ready)
                {
                    SyncData.failed = true;
                    manager.StopClient();
                }
                else
                {
                    SyncData.failed = false;
                }
            }
            
            local = true;
            if (GetComponent<PlayerControl>())
            {
                gameObject.name = "LocalPlayer";
                foreach (ChunkLoad chunk in GameObject.Find("Terrain").GetComponentsInChildren<ChunkLoad>())
                {
                    chunk.local = transform;
                }
                GameObject.Find("Inventory").GetComponent<UpdateUI>().refrenceKeeper = GetComponent<RefrenceKeeperAI>();

                CmdSpawnName();
            }
        }
    }

    [Command]
    public void CmdSpawnName()
    {
        GameObject nameTagObject = Instantiate(nameTag, transform.position, Quaternion.identity);
        NetworkServer.SpawnWithClientAuthority(nameTagObject, parent);
        nameTagObject.GetComponent<SyncName>().CmdUpdateParent(gameObject);
    }

    void Update()
    {
        if (playerManagement)
        {
            if (playerManagement.totalPlayers <= 1 && !health.deaded && gameObject.name != "Player(Clone)" && hasAuthority && SyncData.gameMode == 1 && spawnRocket.ready)
            {
                GetComponent<RefrenceKeeperAI>().updateUI.won.SetActive(true);
                if (PlayerPrefs.HasKey("wins") && !stop)
                {
                    PlayerPrefs.SetInt("wins", PlayerPrefs.GetInt("wins") + 1);
                    stop = true;
                }
                else if (!stop)
                {
                    PlayerPrefs.SetInt("wins", 1);
                    stop = true;
                }
                
                if (Input.GetKey("f") && GetComponent<PlayerControl>() && hasAuthority)
                {
                    if (!isServer)
                    {
                        manager.StopClient();
                    }
                    else
                    {
                        manager.StopClient();
                        manager.StopServer();
                    }
                }
            }
        }
        
        if (health.health <= 0 && spawnRocket.ready)
        {
            if (!dead)
            {
                playerManagement.totalPlayers--;
                dead = true;
                if (isServer && GetComponent<PlayerControl>() && !GetComponent<RefrenceKeeperAI>().updateUI.won.activeInHierarchy)
                {
                    GetComponent<RefrenceKeeperAI>().updateUI.deadMessageServer.SetActive(true);
                }
            }
            
            if (Input.GetKey("f") && GetComponent<PlayerControl>() && hasAuthority)
            {
                if (!isServer)
                {
                    manager.StopClient();
                }
                else
                {
                    manager.StopClient();
                    manager.StopServer();
                }
            }
        }
    }
}
