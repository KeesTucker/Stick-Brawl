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
    [SyncVar]
    public string playerName;
    [SyncVar]
    public Color playerColor;

    public GameObject lobbyPlayer;

    public bool createdPlayer = false;

	// Use this for initialization
    IEnumerator Start()
    {
        if (isLocalPlayer)
        {
            if (SyncData.color == null || SyncData.color == new Color())
            {
                if (PlayerPrefs.HasKey("r") && PlayerPrefs.HasKey("g") && PlayerPrefs.HasKey("b"))
                {
                    SyncData.color = new Color(PlayerPrefs.GetFloat("r"), PlayerPrefs.GetFloat("g"), PlayerPrefs.GetFloat("b"));
                }
                else
                {
                    Color randomColour = Color.HSVToRGB(Random.Range(0, 1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));
                    SyncData.color = randomColour;
                    PlayerPrefs.SetFloat("r", randomColour.r);
                    PlayerPrefs.SetFloat("g", randomColour.g);
                    PlayerPrefs.SetFloat("b", randomColour.b);
                }
            }
            CmdSetColorAndName(SyncData.name, SyncData.color);
        }
        else
        {
            yield return new WaitForSeconds(2f);
            if (!createdPlayer)
            {
                GameObject lobbyPlayerInstantiated = Instantiate(lobbyPlayer, new Vector3(Random.Range(-25f, 25f), 0, -1.4f), Quaternion.identity, transform);
                lobbyPlayerInstantiated.GetComponent<ColourSetterLoad>().SetColor(playerColor);
                StartCoroutine(SetColorAndNameSecond(lobbyPlayerInstantiated));
                createdPlayer = true;
            }
        }
        
    }

    [Command]
    public void CmdSetColorAndName(string playerNameS, Color playerColorS)
    {
        playerName = playerNameS;
        playerColor = playerColorS;
        /*if (!isLocalPlayer)
        {
            GameObject lobbyPlayerInstantiated = Instantiate(lobbyPlayer, new Vector3(Random.Range(-25f, 25f), 0, -1.4f), Quaternion.identity, transform);
            lobbyPlayerInstantiated.GetComponent<ColourSetterLoad>().SetColor(playerColor);
            StartCoroutine(SetColorAndNameSecond(lobbyPlayerInstantiated));
        }*/
        RpcSetColorAndName();
    }

    [ClientRpc]
    public void RpcSetColorAndName()
    {
        if (!isLocalPlayer)
        {
            GameObject lobbyPlayerInstantiated = Instantiate(lobbyPlayer, new Vector3(Random.Range(-25f, 25f), 0, -1.4f), Quaternion.identity, transform);
            lobbyPlayerInstantiated.GetComponent<ColourSetterLoad>().SetColor(playerColor);
            StartCoroutine(SetColorAndNameSecond(lobbyPlayerInstantiated));
            createdPlayer = true;
        }
    }

    public IEnumerator SetColorAndNameSecond(GameObject lobbyPlayerInstantiated)
    {
        yield return new WaitForEndOfFrame();
        lobbyPlayerInstantiated.GetComponent<SetupLoading>().nameTag.GetComponent<SyncName>().instantiatedLobbyPlayer = true;
        int wins;
        if (PlayerPrefs.HasKey("wins"))
        {
            wins = PlayerPrefs.GetInt("wins");
        }
        else
        {
            wins = 0;
        }
        lobbyPlayerInstantiated.GetComponent<SetupLoading>().nameTag.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = playerName + " *" + wins.ToString() + "*";
    }

    public void StartGame () {
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
