using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.LiteNetLib4Mirror;

public class Reconnect : NetworkBehaviour
{
    [Command]
    public void CmdBackToHome()
    {
        SyncData.reconnectServer = true;
        SyncData.numOfClients = NetworkServer.connections.Count;
        SyncData.reconnectLevel = SyncData.chunkID;
        RpcBackToHome();
        StartCoroutine(WaitToKill());
    }

    IEnumerator WaitToKill()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("WORKING");
        if (NetworkServer.active)
        {
            NetworkManager.singleton.StopHost();
        }
        else if (NetworkClient.isConnected)
        {
            NetworkManager.singleton.StopClient();
        }
    }

    [ClientRpc]
    public void RpcBackToHome()
    {
        if (!GetComponent<AISetup>().isServer)
        {
            SyncData.reconnect = true;
            SyncData.ipAddress = LiteNetLib4MirrorTransport.Singleton.clientAddress;
            SyncData.port = LiteNetLib4MirrorTransport.Singleton.port;
            NetworkManager.singleton.StopClient();
        }
    }
}
