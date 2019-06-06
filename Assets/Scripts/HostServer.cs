using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.LiteNetLib4Mirror;
using System.Net;
using System.Net.Sockets;

public class HostServer : MonoBehaviour
{
    public TMPro.TMP_Text buttonText;
    public TMPro.TMP_Text portText;

    LiteNetLib4MirrorTransport transport;

    public TMPro.TMP_Text localIP;
    public TMPro.TMP_Text publicIP;

    void Start()
    {
        SyncData.serverName = "Unnamed Server!";
        SyncData.health = 200;

        transport = NetworkManager.singleton.gameObject.GetComponent<LiteNetLib4MirrorTransport>();
        transport.port = 2345;

        string externalip = new WebClient().DownloadString("http://icanhazip.com");
        publicIP.text = externalip;
        foreach (IPAddress ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                localIP.text = ip.ToString();
                break;
            }
        }
    }

    void Update()
    {
        if (NetworkServer.active)
        {
            buttonText.text = "Stop Host";
            portText.text = "Server Port: " + transport.port.ToString();
        }
        else if (NetworkClient.isConnected)
        {
            buttonText.text = "Disconnect";
            portText.text = "";
        }
        else
        {
            buttonText.text = "Host";
            portText.text = "";
        }
    }

    public void StartServer()
    {
        if (NetworkServer.active)
        {
            NetworkManager.singleton.StopHost();
            //Message
        }
        else if (NetworkClient.isConnected)
        {
            NetworkManager.singleton.StopClient();
            //Message
        }
        else
        {
            CheckPorts();
            NetworkManager.singleton.StartHost();
            if (GameObject.Find("Canvas/Host").GetComponent<Animator>())
            {
                GameObject.Find("Canvas/Host").GetComponent<Animator>().SetTrigger("Exit");
                GameObject.Find("Canvas/Main").GetComponent<Animator>().SetTrigger("Entry");
            }
        }
    }

    void CheckPorts()
    {
        foreach (ServerUIObject UIObject in SyncData.servers)
        {
            if (UIObject.port == transport.port)
            {
                transport.port = (ushort)Random.Range(2345, 2365);
                CheckPorts();
            }
        }
    }

    public void EditPort(string port)
    {
        if (!ushort.TryParse(port, out NetworkManager.singleton.gameObject.GetComponent<LiteNetLib4MirrorTransport>().port))
        {
            //Error
        }
    }

    public void EditHealth(string health)
    {
        if (!int.TryParse(health, out SyncData.health))
        {
            //Error
        }
    }

    public void EditName(string name)
    {
        SyncData.serverName = name;
    }
}
