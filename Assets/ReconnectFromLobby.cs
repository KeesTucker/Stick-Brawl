using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.LiteNetLib4Mirror;

public class ReconnectFromLobby : MonoBehaviour
{
    public Transform campaignLevels;
    public Transform playPanel;

    private bool safety = false;

    // Start is called before the first frame update
    void Start()
    {
        if (SyncData.reconnect)
        {
            Debug.Log("Waiting for host");
            SyncData.reconnect = false;
            StartCoroutine(WaitForHost());
        }
        else if (SyncData.reconnectServer)
        {
            StartCoroutine(WaitForClients());
            StartCoroutine(WaitForClientsBackup());
        }
    }

    IEnumerator WaitForClients()
    {
        if (NetworkServer.active)
        {
            NetworkManager.singleton.StopHost();
        }
        else if (NetworkClient.isConnected)
        {
            NetworkManager.singleton.StopClient();
        }
        Debug.Log("Starting Host1");
        yield return new WaitForSeconds(0.2f);
        Debug.Log("Starting Host2");
        NetworkManager.singleton.StartHost();
        yield return new WaitForSeconds(0.5f);
        while (NetworkServer.connections.Count < SyncData.numOfClients)
        {
            yield return null;
        }
        if (!safety)
        {
            Debug.Log("CorrectNumOfConnections");
            safety = true;
            campaignLevels.GetChild(20 - SyncData.reconnectLevel).GetComponent<StartCampaignLevel>().StartLevel();
        }
        SyncData.reconnectServer = false;
    }

    IEnumerator WaitForClientsBackup()
    {
        yield return new WaitForSeconds(10f);
        if (!safety)
        {
            Debug.Log(NetworkServer.connections.Count.ToString() + " vs. " + SyncData.numOfClients.ToString());
            campaignLevels.GetChild(20 - SyncData.reconnectLevel).GetComponent<StartCampaignLevel>().StartLevel();
        }
        safety = false;
    }

    IEnumerator WaitForHost()
    {
        yield return new WaitForSeconds(0.5f);
        NetworkManager.singleton.networkAddress = SyncData.ipAddress;
        //NetworkManager.singleton.maxConnections = 2;
        LiteNetLib4MirrorTransport.Singleton.clientAddress = SyncData.ipAddress;
        LiteNetLib4MirrorTransport.Singleton.port = (ushort)SyncData.port;
        //LiteNetLib4MirrorTransport.Singleton.maxConnections = 2;
        NetworkManager.singleton.StartClient();
    }
}
