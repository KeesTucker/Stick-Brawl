using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.LiteNetLib4Mirror;

public class HostServer : MonoBehaviour
{
    public NetworkManager manager;
    public LiteNetLib4MirrorTransport transport;

    void Start()
    {
        SyncData.serverName = "Unnamed Server!";
        SyncData.numPlayers = 0;
        transport.port = 2345;
    }

    public void StartServer()
    {
        NetworkManager.singleton.StartHost();
    }

    public void EditPort(string port)
    {
        transport.port = ushort.Parse(port);
    }

    public void EditBots(string bots)
    {
        SyncData.numPlayers = int.Parse(bots);
    }

    public void EditName(string name)
    {
        SyncData.serverName = name;
    }
}
