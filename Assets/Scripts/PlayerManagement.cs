using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerManagement : NetworkBehaviour {

    public GameObject AIPlayer;
    public GameObject bot;
    public GameObject playerSpawned;
    public GameObject playerSpawnedReal;
    public int numPlayers = 5;
    public int currentNum = 1;
    public int totalPlayers;
    private Vector3 pos;
    public PlayerManagement playerManagement;
    [SyncVar]
    public bool server;

	// Use this for initialization
	void Start () {
        numPlayers = SyncData.numPlayers;
        if (!isLocalPlayer && isServer)
        {
            playerManagement = GameObject.Find("LocalConnection").GetComponent<PlayerManagement>();
        }
        if (isServer && isLocalPlayer)
        {
            server = true;
        }
        if (server)
        {
            GameObject.Find("PlayersLeft").transform.GetChild(0).gameObject.GetComponent<PlayersLeft>().playerManagement = this;
        }
        if (isLocalPlayer)
        {
            CmdSpawn(); //Spawn code here
            if (isServer)
            {
                if (SyncData.gameMode == 1)
                {
                    for (int i = 0; i < numPlayers; i++)
                    {
                        CmdBotSpawn();
                    }
                }
                else if (SyncData.gameMode == 2)
                {
                    StartCoroutine(Onslaught());
                }
            }
        }
	}

    [Command]
    public void CmdSpawn()
    {
        if (playerManagement)
        {
            pos = new Vector3((Random.Range(-SyncData.worldSize / 2, SyncData.worldSize / 2) * 250) + 125, 100, 0);
        }
        pos = new Vector3((Random.Range(-SyncData.worldSize / 2, SyncData.worldSize / 2) * 250) + 125, 100, 0);
        playerSpawned = Instantiate(AIPlayer, pos, transform.rotation);
        playerSpawnedReal = playerSpawned;
        playerSpawned.GetComponent<AISetup>().parent = gameObject;
        NetworkServer.SpawnWithClientAuthority(playerSpawned, connectionToClient);
        if (playerManagement)
        {
            playerManagement.currentNum++;
        }
        else
        {
            currentNum++;
        }
    }

    [Command]
    public void CmdBotSpawn()
    {
        if (currentNum % 2 == 0)
        {
            pos = new Vector3((currentNum / 2) * 250, 100, 0);
        }
        else
        {
            pos = new Vector3((int)(-currentNum / 2) * 250, 100, 0);
        }
        playerSpawned = Instantiate(bot, pos, transform.rotation);
        playerSpawned.GetComponent<AISetup>().parent = gameObject;
        NetworkServer.SpawnWithClientAuthority(playerSpawned, connectionToClient);
        currentNum++;
    }

    IEnumerator Onslaught()
    {
        while (!playerSpawnedReal.GetComponent<SpawnRocketAI>().ready)
        {
            yield return null;
        }
        while (playerSpawnedReal.GetComponent<HealthAI>().health >= 0)
        {
            currentNum = 0;
            for (int i = 0; i < numPlayers / 4; i++)
            {
                if (totalPlayers < numPlayers + 5)
                {
                    CmdBotSpawn();
                }
            }
            yield return new WaitForSeconds(15f);
        }
    }
}
