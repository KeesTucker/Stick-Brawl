using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.LiteNetLib4Mirror;

public class Reconnect : NetworkBehaviour
{
    [Command]
    public void CmdLevelUpdate()
    {
        SyncData.reconnectServer = true;
        SyncData.numOfClients = NetworkServer.connections.Count;
        RpcLevelUpdate(SyncData.backToHome);
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
    public void RpcLevelUpdate(bool home)
    {
        if (!isServer)
        {
            SyncData.backToHome = home;
            SyncData.reconnect = true;
            SyncData.ipAddress = LiteNetLib4MirrorTransport.Singleton.clientAddress;
            SyncData.port = LiteNetLib4MirrorTransport.Singleton.port;
            NetworkManager.singleton.StopClient();
        }
    }
}
