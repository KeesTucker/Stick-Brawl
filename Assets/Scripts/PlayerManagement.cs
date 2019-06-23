using System.Collections;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

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
    [SyncVar]
    public int skinID;

    public GameObject lobbyPlayer;

    public bool createdPlayer = false;

    GameObject lobbyPlayerInstantiated;

    public bool isCampaign;

    public bool isLobby;

    // Use this for initialization
    IEnumerator Start()
    {
        if (isLobby && SceneManager.GetActiveScene().name == "Main")
        {
            Destroy(gameObject);
        }

        //DontDestroyOnLoad(gameObject);
        if (!isLobby && SceneManager.GetActiveScene().name == "Main")
        {
            StartGameScene();
        }
        else if(SceneManager.GetActiveScene().name != "Main")
        {
            if (isLocalPlayer)
            {
                if (!isServer)
                {
                    GetComponent<NetworkLobbyPlayer>().CmdChangeReadyState(true);
                }
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
                SyncData.skinID = PlayerPrefs.GetInt(ShopItemType.Skin.ToString() + "selected");
                CmdSetColorAndName(SyncData.name, SyncData.color, PlayerPrefs.GetInt(ShopItemType.Skin.ToString() + "selected"));
            }
            else
            {
                yield return new WaitForSeconds(2f);
                if (!createdPlayer)
                {
                    lobbyPlayerInstantiated = Instantiate(lobbyPlayer, new Vector3(Random.Range(-25f, 25f), 0, -1.4f), Quaternion.identity);
                    lobbyPlayerInstantiated.GetComponent<ColourSetterLoad>().SetColor(playerColor);
                    lobbyPlayerInstantiated.GetComponent<SkinApply>().UpdateSkin(skinID);
                    StartCoroutine(SetColorAndNameSecond(lobbyPlayerInstantiated));
                    createdPlayer = true;
                }
            }
        }
    }

    [Command]
    public void CmdSetColorAndName(string playerNameS, Color playerColorS, int skinIDS)
    {
        playerName = playerNameS;
        playerColor = playerColorS;
        skinID = skinIDS;
        /*if (!isLocalPlayer)
        {
            GameObject lobbyPlayerInstantiated = Instantiate(lobbyPlayer, new Vector3(Random.Range(-25f, 25f), 0, -1.4f), Quaternion.identity, transform);
            lobbyPlayerInstantiated.GetComponent<ColourSetterLoad>().SetColor(playerColor);
            StartCoroutine(SetColorAndNameSecond(lobbyPlayerInstantiated));
        }*/
        RpcSetColorAndName(playerColor, skinID);
    }

    [ClientRpc]
    public void RpcSetColorAndName(Color playerColorSS, int skinIDS)
    {
        if (!isLocalPlayer)
        {
            lobbyPlayerInstantiated = Instantiate(lobbyPlayer, new Vector3(Random.Range(-25f, 25f), 0, -1.4f), Quaternion.identity);
            lobbyPlayerInstantiated.GetComponent<ColourSetterLoad>().SetColor(playerColorSS);
            lobbyPlayerInstantiated.GetComponent<SkinApply>().UpdateSkin(skinIDS);
            StartCoroutine(SetColorAndNameSecond(lobbyPlayerInstantiated));
            createdPlayer = true;
        }
    }

    [Command]
    public void CmdUpdateColorAndName(string playerNameS, Color playerColorS, int skinIDS)
    {
        playerName = playerNameS;
        playerColor = playerColorS;
        skinID = skinIDS;
        foreach (SyncName syncName in FindObjectsOfType<SyncName>())
        {
            syncName.UpdateColor();
        }
        /*if (!isLocalPlayer)
        {
            GameObject lobbyPlayerInstantiated = Instantiate(lobbyPlayer, new Vector3(Random.Range(-25f, 25f), 0, -1.4f), Quaternion.identity, transform);
            lobbyPlayerInstantiated.GetComponent<ColourSetterLoad>().SetColor(playerColor);
            StartCoroutine(SetColorAndNameSecond(lobbyPlayerInstantiated));
        }*/
        RpcUpdateColorAndName(playerColor, skinID);
    }

    [Command]
    public void CmdUpdateColorAndNameN(string playerNameS)
    {
        playerName = playerNameS;
        RpcUpdateColorAndNameN(playerNameS);
    }

    [ClientRpc]
    public void RpcUpdateColorAndNameN(string playerNameS)
    {
        if (!isLocalPlayer)
        {
            StartCoroutine(SetColorAndNameSecond(lobbyPlayerInstantiated, playerNameS));
        }
    }

    [ClientRpc]
    public void RpcUpdateColorAndName(Color playerColorSS, int skinIDS)
    {
        if (!isLocalPlayer)
        {
            lobbyPlayerInstantiated.GetComponent<ColourSetterLoad>().SetColor(playerColorSS);
            lobbyPlayerInstantiated.GetComponent<SkinApply>().UpdateSkin(skinIDS);
            foreach (SyncName syncName in FindObjectsOfType<SyncName>())
            {
                syncName.UpdateColor();
            }
            StartCoroutine(SetColorAndNameSecond(lobbyPlayerInstantiated));
        }
    }

    public IEnumerator SetColorAndNameSecond(GameObject lobbyPlayerInstantiatedS)
    {
        yield return new WaitForEndOfFrame();
        lobbyPlayerInstantiatedS.GetComponent<SetupLoading>().nameTag.GetComponent<SyncName>().instantiatedLobbyPlayer = true;
        lobbyPlayerInstantiatedS.GetComponent<SetupLoading>().nameTag.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = playerName;
        foreach (SyncName syncName in FindObjectsOfType<SyncName>())
        {
            syncName.UpdateColor();
        }
    }

    public IEnumerator SetColorAndNameSecond(GameObject lobbyPlayerInstantiatedS, string playerNameS)
    {
        yield return new WaitForEndOfFrame();
        lobbyPlayerInstantiatedS.GetComponent<SetupLoading>().nameTag.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = playerName;
    }

    private void StartGameScene()
    {
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
                if (!SyncData.isCampaign)
                {
                    SyncData.gameMode = ChooseGamemode();
                }
                
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

        StartCoroutine(GameModeUpdate());
    }

    IEnumerator GameModeUpdate()
    {
        yield return new WaitForSeconds(4f);
        CmdServerGamemodeUpdate(SyncData.gameMode, SyncData.isCampaign, SyncData.health, SyncData.chunkID);
    }

    [Command]
    void CmdServerGamemodeUpdate(int gameMode, bool isCampaign, int health, int chunkID)
    {
        RpcClientGamemodeUpdate(gameMode, isCampaign, health, chunkID);
    }

    [ClientRpc]
    void RpcClientGamemodeUpdate(int gameMode, bool isCampaign, int health, int chunkID)
    {
        Debug.Log("Got Gamemode: " + gameMode.ToString() + isCampaign.ToString());
        SyncData.gameMode = gameMode;
        SyncData.isCampaign = isCampaign;
        SyncData.health = health;
        SyncData.chunkID = chunkID;
    }

    public int ChooseGamemode()
    {
        bool hasActivated = false;
        for (int i = 0; i < SyncData.gameModes.Length; i++)
        {
            if (SyncData.gameModes[i] == true)
            {
                hasActivated = true;
            }   
        }

        int gameMode = Random.Range(0, SyncData.gameModes.Length);

        if (!hasActivated)
        {
            return gameMode;
        }
        else
        {
            gameMode = Random.Range(0, SyncData.gameModes.Length);
            while (SyncData.gameModes[gameMode] == false)
            {
                gameMode = Random.Range(0, SyncData.gameModes.Length);
            }
            return gameMode;
        }
    }

    [Command]
    public void CmdSpawn()
    {
        if (playerManagement)
        {
            pos = new Vector3(Random.Range(-100, 100), 0, 0);
        }
        pos = new Vector3(Random.Range(-100, 100), 0, 0);
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
        pos = new Vector3(Random.Range(-100, 100), 0, 0);
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
