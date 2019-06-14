using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror.LiteNetLib4Mirror;
using System.Net;

public class GenerateServerList : NetworkDiscoveryHUD
{
    public UpdateServerList updateServerList;

    public override void OnClientDiscoveryResponse(IPEndPoint endpoint, string text)
    {
        Debug.Log("Found Server: " + text + " " + endpoint.Port.ToString() + " " + endpoint.Address.ToString());
        ServerUIObject server = new ServerUIObject(text, endpoint.Port, endpoint);
        SyncData.servers.Add(server);
    }

    public override void ResetList()
    {
        if (updateServerList == null)
        {
            updateServerList = FindObjectOfType<UpdateServerList>();
        }
        if (updateServerList != null)
        {
            updateServerList.Refresh();
        }
        
        SyncData.servers.Clear();
    }
}
