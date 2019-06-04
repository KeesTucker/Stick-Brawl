using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using Mirror.LiteNetLib4Mirror;
using Mirror;

public class JoinServerList : MonoBehaviour
{
    public IPEndPoint endpoint;

    public string IPAddress = "localhost";
    public int ServerPort = 2345;

    public void JoinServer()
    {
        string ip = endpoint.Address.ToString();

        NetworkManager.singleton.networkAddress = ip;
        NetworkManager.singleton.maxConnections = 2;
        LiteNetLib4MirrorTransport.Singleton.clientAddress = ip;
        LiteNetLib4MirrorTransport.Singleton.port = (ushort)endpoint.Port;
        LiteNetLib4MirrorTransport.Singleton.maxConnections = 2;
        NetworkManager.singleton.StartClient();
    }

    public void EditIP(string ip)
    {
        IPAddress = ip;
    }

    public void EditPort(string port)
    {
        ServerPort = int.Parse(port);
    }

    public void JoinServerManual()
    {
        JoinServerManualCall(IPAddress, (ushort)ServerPort);
    }

    private void JoinServerManualCall(string ip, ushort port)
    {
        NetworkManager.singleton.networkAddress = ip;
        NetworkManager.singleton.maxConnections = 2;
        LiteNetLib4MirrorTransport.Singleton.clientAddress = ip;
        LiteNetLib4MirrorTransport.Singleton.port = port;
        LiteNetLib4MirrorTransport.Singleton.maxConnections = 2;
        NetworkManager.singleton.StartClient();
    }
}
