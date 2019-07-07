using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.LiteNetLib4Mirror;

public class ReconnectFromLobby : MonoBehaviour
{
    public Transform campaignLevels;
    public Transform multiplayerLevels;
    public Transform playPanel;

    private bool safety = false;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.Save();
        if (SyncData.reconnect)
        {
            Debug.Log("Waiting for host");
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
            if (SyncData.retryLevel)
            {
                Debug.Log("retry level");
                campaignLevels.GetChild(20 - SyncData.reconnectLevel).GetComponent<StartCampaignLevel>().StartLevel();
            }
            else if (SyncData.nextLevel)
            {
                Debug.Log("next level");
                if (SyncData.isCampaignLevel)
                {
                    //Need to make sure we dont go past last level!
                    Debug.Log(20 - SyncData.reconnectLevel - 1);
                    campaignLevels.GetChild(20 - SyncData.reconnectLevel - 1).GetComponent<StartCampaignLevel>().StartLevel();
                }
                else
                {
                    multiplayerLevels.GetChild(1).GetComponent<MultiplayerStart>().StartLevel();
                }
            }
            
        }
        SyncData.reconnectServer = false;
    }

    IEnumerator WaitForClientsBackup()
    {
        yield return new WaitForSeconds(5f);
        if (!safety)
        {
            if (SyncData.retryLevel)
            {
                Debug.Log("retry level");
                campaignLevels.GetChild(20 - SyncData.reconnectLevel).GetComponent<StartCampaignLevel>().StartLevel();
            }
            else if (SyncData.nextLevel)
            {
                Debug.Log("next level");
                if (SyncData.isCampaignLevel)
                {
                    //Need to make sure we dont go past last level!
                    Debug.Log(20 - SyncData.reconnectLevel - 1);
                    campaignLevels.GetChild(20 - SyncData.reconnectLevel - 1).GetComponent<StartCampaignLevel>().StartLevel();
                }
                else
                {
                    multiplayerLevels.GetChild(1).GetComponent<MultiplayerStart>().StartLevel();
                }
            }
        }
        safety = false;
        SyncData.reconnectServer = false;
    }

    IEnumerator WaitForHost()
    {
        yield return new WaitForSeconds(2f);
        SyncData.reconnect = false;
        NetworkManager.singleton.networkAddress = SyncData.ipAddress;
        //NetworkManager.singleton.maxConnections = 2;
        LiteNetLib4MirrorTransport.Singleton.clientAddress = SyncData.ipAddress;
        LiteNetLib4MirrorTransport.Singleton.port = (ushort)SyncData.port;
        //LiteNetLib4MirrorTransport.Singleton.maxConnections = 2;
        NetworkManager.singleton.StartClient();
    }
}
