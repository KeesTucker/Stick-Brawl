using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror.LiteNetLib4Mirror;
using System.Net;

public class NetworkDiscoveryManager : LiteNetLib4MirrorDiscovery
{
    protected override bool ProcessDiscoveryRequest(IPEndPoint ipEndPoint, string text, out string username)
    {
        username = SyncData.serverName;
        return true;
    }
}