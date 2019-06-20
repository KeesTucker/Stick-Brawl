﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.LiteNetLib4Mirror;
using UnityEngine.SceneManagement;

public class StartCampaignLevel : MonoBehaviour
{
    LiteNetLib4MirrorTransport transport;

    public int id;

    public int gameMode;

    public int numBots;

    public int chunkID;

    public int health = 100;

    // Start is called before the first frame update
    void Start()
    {
        transport = NetworkManager.singleton.gameObject.GetComponent<LiteNetLib4MirrorTransport>();
    }

    void CheckPorts()
    {
        foreach (ServerUIObject UIObject in SyncData.servers)
        {
            if (UIObject.port == transport.port)
            {
                transport.port = (ushort)Random.Range(2345, 2365);
                CheckPorts();
            }
        }
    }

    public void StartLevel()
    {
        SyncData.health = health;
        SyncData.numPlayers = numBots;
        SyncData.gameMode = gameMode;
        SyncData.chunkID = chunkID;

        if (!NetworkServer.active)
        {
            CheckPorts();
            SyncData.serverName = SyncData.name + "s Campaign Server!";
            if (NetworkClient.isConnected)
            {
                NetworkManager.singleton.StopClient();
                NetworkManager.singleton.StartHost();
            }
            else
            {
                NetworkManager.singleton.StartHost();
            }
        }

        StartCoroutine(WaitForReady());
    }

    IEnumerator WaitForReady()
    {
        yield return new WaitForSeconds(1f);
        SyncData.isCampaign = true;
        SceneManager.LoadScene("Main");
        GameObject.Find("LocalConnection").GetComponent<NetworkLobbyPlayer>().CmdChangeReadyState(true);
    }
}