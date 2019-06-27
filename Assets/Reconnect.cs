using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.LiteNetLib4Mirror;

public class Reconnect : MonoBehaviour
{
    [Command]
    public void CmdBackToHome()
    {
        SyncData.reconnectServer = true;
        SyncData.numOfClients = NetworkServer.connections.Count;
        RpcBackToHome();
        StartCoroutine(WaitToKill());
    }

    IEnumerator WaitToKill()
    {
        yield return new WaitForSeconds(0.5f);
        NetworkManager.singleton.StopClient();
        NetworkManager.singleton.StopHost();
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
