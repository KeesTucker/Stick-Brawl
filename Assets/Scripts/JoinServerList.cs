﻿using System.Collections;
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

    public TMPro.TMP_Text mainButtonText;

    public TMPro.TMP_Text listButtonText;

    void Update()
    {
        if (mainButtonText)
        {
            if (NetworkServer.active)
            {
                mainButtonText.text = "Stop Host";
            }
            else if (NetworkClient.isConnected)
            {
                mainButtonText.text = "Disconnect";
            }
            else
            {
                mainButtonText.text = "Join";
            }
        }
        if (listButtonText)
        {
            if (NetworkServer.active)
            {
                listButtonText.text = "Stop Hosting to Join";
            }
            else if (NetworkClient.isConnected)
            {
                listButtonText.text = "Disconnect to Join";
            }
        }
    }

    public void JoinServer()
    {
        if (NetworkServer.active)
        {
            NetworkManager.singleton.StopHost();
            ShowError("Stopped hosting, you can now join another server.");
        }
        else if (NetworkClient.isConnected)
        {
            NetworkManager.singleton.StopClient();
            ShowError("Disconnected from server, you can now join another!");
        }
        else
        {
            string ip = endpoint.Address.ToString();

            NetworkManager.singleton.networkAddress = ip;
            NetworkManager.singleton.maxConnections = 2;
            LiteNetLib4MirrorTransport.Singleton.clientAddress = ip;
            LiteNetLib4MirrorTransport.Singleton.port = (ushort)endpoint.Port;
            LiteNetLib4MirrorTransport.Singleton.maxConnections = 2;
            NetworkManager.singleton.StartClient();
            if (GameObject.Find("Canvas/Join").GetComponent<Animator>())
            {
                GameObject.Find("Canvas/Join").GetComponent<Animator>().SetTrigger("Exit");
                GameObject.Find("Canvas/Main").GetComponent<Animator>().SetTrigger("Entry");
                GameObject.Find("Canvas/JoinMinor").GetComponent<Animator>().SetTrigger("Exit");
                GameObject.Find("Canvas/Minor").GetComponent<Animator>().SetTrigger("Entry");
            }
        }
    }

    public void EditIP(string ip)
    {
        IPAddress = ip;
    }

    public void EditPort(string port)
    {
        if (!int.TryParse(port, out ServerPort))
        {
            ShowError("Not a valid port number");
        }
    }

    public void JoinServerManual()
    {
        if (NetworkServer.active)
        {
            NetworkManager.singleton.StopHost();
            ShowError("Stopped hosting, you can now join another server.");
        }
        else if (NetworkClient.isConnected)
        {
            NetworkManager.singleton.StopClient();
            ShowError("Disconnected from server, you can now join another!");
        }
        else
        {
            JoinServerManualCall(IPAddress, (ushort)ServerPort);
        }
    }

    private void ShowError(string msg)
    {
        GameObject confirm;
        if (GameObject.Find("NoDestroyCanvas/Message"))
        {
            confirm = GameObject.Find("NoDestroyCanvas/Message");
            confirm.transform.GetChild(1).GetChild(0).GetComponent<TMPro.TMP_Text>().text = "Message!";
            confirm.transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<TMPro.TMP_Text>().text = msg;
            confirm.GetComponent<Animator>().SetTrigger("Entry");
        }
    }

    private void JoinServerManualCall(string ip, ushort port)
    {
        NetworkManager.singleton.networkAddress = ip;
        NetworkManager.singleton.maxConnections = 2;
        LiteNetLib4MirrorTransport.Singleton.clientAddress = ip;
        LiteNetLib4MirrorTransport.Singleton.port = port;
        //LiteNetLib4MirrorTransport.Singleton.maxConnections = 2;
        NetworkManager.singleton.StartClient();
        if (GameObject.Find("Canvas/Join").GetComponent<Animator>())
        {
            GameObject.Find("Canvas/Join").GetComponent<Animator>().SetTrigger("Exit");
            GameObject.Find("Canvas/Main").GetComponent<Animator>().SetTrigger("Entry");
            GameObject.Find("Canvas/JoinMinor").GetComponent<Animator>().SetTrigger("Exit");
            GameObject.Find("Canvas/Minor").GetComponent<Animator>().SetTrigger("Entry");
        }
    }
}
