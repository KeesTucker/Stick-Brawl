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

    [SyncVar]
    public bool serverLocal;

    public List<HealthAI> healths = new List<HealthAI>();

    private int count;
    private bool ready = false;

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
            //ERROR maybe? not sure if that will get the correct refrence.
            playerManagement = GameObject.Find("PlayerConnect(Clone)").GetComponent<PlayerManagement>();
        }
        playerManagement.totalPlayers++;
    }

    IEnumerator checkPlayers()
    {
        yield return new WaitForSeconds(1f);
        foreach (HealthAI health in FindObjectsOfType<HealthAI>())
        {
            if (health.gameObject.name == "Player(Clone)")
            {
                healths.Add(health);
            }
        }
        ready = true;
    }

    public override void OnStartAuthority()
    {
        if (hasAuthority)
        {
            manager = GameObject.Find("_NetworkManager").GetComponent<NetworkManager>();
            /*if (SyncData.gameMode == 1)
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
            }*/
            SyncData.failed = false;

            local = true;
            if (GetComponent<PlayerControl>())
            {
                gameObject.name = "LocalPlayer";
                foreach (ChunkLoad chunk in GameObject.Find("Terrain").GetComponentsInChildren<ChunkLoad>())
                {
                    chunk.local = transform;
                }
                GameObject.Find("Inventory").GetComponent<UpdateUI>().refrenceKeeper = GetComponent<RefrenceKeeperAI>();

                StartCoroutine(WaitForNameSpawn());
            }

            StartCoroutine(checkPlayers());
        }
    }

    IEnumerator WaitForNameSpawn()
    {
        yield return new WaitForSeconds(3f);
        CmdSpawnName();
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
        if (playerManagement && ready)
        {
            if (Input.GetKey("f") && GetComponent<PlayerControl>() && hasAuthority && stop)
            {
                if (SyncData.isCampaign)
                {
                    SyncData.openCampaignScreen = true;
                }
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
            count++;
            if (count > 30)
            {
                count = 0;
                if (SyncData.isCampaign)
                {
                    bool allDead = true;
                    foreach (HealthAI health in healths)
                    {
                        if (!health.deaded)
                        {
                            allDead = false;
                        }
                    }
                    if (allDead && !health.deaded && gameObject.name != "Player(Clone)" && hasAuthority && spawnRocket.ready)
                    {
                        Debug.Log((float)health.health / (float)health.health * 100f);
                        if (!PlayerPrefs.HasKey(SyncData.chunkID.ToString() + "level"))
                        {
                            PlayerPrefs.SetFloat(SyncData.chunkID.ToString() + "level", (float)health.health / (float)SyncData.health * 100f);
                        }
                        else if (PlayerPrefs.GetFloat(SyncData.chunkID.ToString() + "level") < (float)health.health / (float)SyncData.health * 100f)
                        {
                            PlayerPrefs.SetFloat(SyncData.chunkID.ToString() + "level", (float)health.health / (float)SyncData.health * 100f);
                        }
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
                    }
                }
                else
                {
                    if (playerManagement.totalPlayers <= 1 && !health.deaded && gameObject.name != "Player(Clone)" && hasAuthority && spawnRocket.ready)
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
