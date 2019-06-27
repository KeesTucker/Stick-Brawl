using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.LiteNetLib4Mirror;

public class ReconnectFromLobby : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (SyncData.reconnect)
        {
            SyncData.reconnect = false;
            StartCoroutine(WaitForHost());
        }
        else if (SyncData.reconnectServer)
        {
            NetworkManager.singleton.StartHost();
            //wait till same connections then restart
        }
    }

    IEnumerator WaitForHost()
    {
        SyncData.servers.Clear();
        while (SyncData.servers.Count == 0)
        {
            yield return null;
        }
        NetworkManager.singleton.networkAddress = SyncData.ipAddress;
        //NetworkManager.singleton.maxConnections = 2;
        LiteNetLib4MirrorTransport.Singleton.clientAddress = SyncData.ipAddress;
        LiteNetLib4MirrorTransport.Singleton.port = (ushort)SyncData.port;
        //LiteNetLib4MirrorTransport.Singleton.maxConnections = 2;
        NetworkManager.singleton.StartClient();
    }
}
