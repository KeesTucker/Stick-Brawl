using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.LiteNetLib4Mirror;
using System.Net;
using System.Net.Sockets;
using UnityEngine.UI;

public class HostServer : MonoBehaviour
{
    public TMPro.TMP_Text buttonText;
    public TMPro.TMP_Text portText;

    LiteNetLib4MirrorTransport transport;

    public TMPro.TMP_Text localIP;
    public TMPro.TMP_Text publicIP;

    IEnumerator Start()
    {
        SyncData.serverName = "Unnamed Server!";
        SyncData.health = 100;

        transport = NetworkManager.singleton.gameObject.GetComponent<LiteNetLib4MirrorTransport>();
        transport.port = 2345;

        foreach (IPAddress ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                localIP.text = ip.ToString();
                break;
            }
        }

        yield return new WaitForSeconds(0.5f);

        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            string externalip = new WebClient().DownloadString("http://icanhazip.com");
            publicIP.text = externalip;
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

    private void ShowError(string msg)
    {
        GameObject confirm;
        if (GameObject.Find("NoDestroyCanvas/Message"))
        {
            confirm = GameObject.Find("NoDestroyCanvas/Message");
            confirm.transform.GetChild(1).GetChild(0).GetComponent<TMPro.TMP_Text>().text = "Message!";
            confirm.transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<TMPro.TMP_Text>().text = msg;
            confirm.GetComponent<Animator>().SetTrigger("Entry");
            foreach (Image image in confirm.GetComponentsInChildren<Image>())
            {
                image.enabled = true;
            }
            foreach (TMPro.TextMeshProUGUI text in confirm.GetComponentsInChildren<TMPro.TextMeshProUGUI>())
            {
                text.enabled = true;
            }
            foreach (TMPro.TMP_Text text in confirm.GetComponentsInChildren<TMPro.TextMeshProUGUI>())
            {
                text.enabled = true;
            }
            if (confirm.GetComponent<Image>())
            {
                confirm.GetComponent<Image>().enabled = true;
            }
            if (confirm.GetComponent<TMPro.TextMeshProUGUI>())
            {
                confirm.GetComponent<TMPro.TextMeshProUGUI>().enabled = true;
            }
            if (confirm.GetComponent<TMPro.TMP_Text>())
            {
                confirm.GetComponent<TMPro.TMP_Text>().enabled = true;
            }
        }
    }

    public void StartServer()
    {
        if (NetworkServer.active)
        {
            NetworkManager.singleton.StopHost();
            ShowError("Stopped hosting, you can now host a new server.");
        }
        else if (NetworkClient.isConnected)
        {
            NetworkManager.singleton.StopClient();
            ShowError("Disconnected from server, you can now host your own!");
        }
        else
        {
            CheckPorts();
            NetworkManager.singleton.StartHost();
            if (GameObject.Find("Canvas/Host").GetComponent<Animator>())
            {
                GameObject go = GameObject.Find("Canvas/Host");
                go.GetComponent<Animator>().SetTrigger("Exit");
                foreach (Image image in go.GetComponentsInChildren<Image>())
                {
                    image.enabled = false;
                }
                foreach (TMPro.TextMeshProUGUI text in go.GetComponentsInChildren<TMPro.TextMeshProUGUI>())
                {
                    text.enabled = false;
                }
                foreach (TMPro.TMP_Text text in go.GetComponentsInChildren<TMPro.TextMeshProUGUI>())
                {
                    text.enabled = false;
                }
                if (go.GetComponent<Image>())
                {
                    go.GetComponent<Image>().enabled = false;
                }
                if (go.GetComponent<TMPro.TextMeshProUGUI>())
                {
                    go.GetComponent<TMPro.TextMeshProUGUI>().enabled = false;
                }
                if (go.GetComponent<TMPro.TMP_Text>())
                {
                    go.GetComponent<TMPro.TMP_Text>().enabled = false;
                }
                go = GameObject.Find("Canvas/Main");
                go.GetComponent<Animator>().SetTrigger("Entry");
                foreach (Image image in go.GetComponentsInChildren<Image>())
                {
                    image.enabled = true;
                }
                foreach (TMPro.TextMeshProUGUI text in go.GetComponentsInChildren<TMPro.TextMeshProUGUI>())
                {
                    text.enabled = true;
                }
                foreach (TMPro.TMP_Text text in go.GetComponentsInChildren<TMPro.TextMeshProUGUI>())
                {
                    text.enabled = true;
                }
                if (go.GetComponent<Image>())
                {
                    go.GetComponent<Image>().enabled = true;
                }
                if (go.GetComponent<TMPro.TextMeshProUGUI>())
                {
                    go.GetComponent<TMPro.TextMeshProUGUI>().enabled = true;
                }
                if (go.GetComponent<TMPro.TMP_Text>())
                {
                    go.GetComponent<TMPro.TMP_Text>().enabled = true;
                }
                go = GameObject.Find("Canvas/JoinMinor");
                go.GetComponent<Animator>().SetTrigger("Exit");
                foreach (Image image in go.GetComponentsInChildren<Image>())
                {
                    image.enabled = false;
                }
                foreach (TMPro.TextMeshProUGUI text in go.GetComponentsInChildren<TMPro.TextMeshProUGUI>())
                {
                    text.enabled = false;
                }
                foreach (TMPro.TMP_Text text in go.GetComponentsInChildren<TMPro.TextMeshProUGUI>())
                {
                    text.enabled = false;
                }
                if (go.GetComponent<Image>())
                {
                    go.GetComponent<Image>().enabled = false;
                }
                if (go.GetComponent<TMPro.TextMeshProUGUI>())
                {
                    go.GetComponent<TMPro.TextMeshProUGUI>().enabled = false;
                }
                if (go.GetComponent<TMPro.TMP_Text>())
                {
                    go.GetComponent<TMPro.TMP_Text>().enabled = false;
                }
                go = GameObject.Find("Canvas/Minor");
                go.GetComponent<Animator>().SetTrigger("Entry");
                foreach (Image image in go.GetComponentsInChildren<Image>())
                {
                    image.enabled = true;
                }
                foreach (TMPro.TextMeshProUGUI text in go.GetComponentsInChildren<TMPro.TextMeshProUGUI>())
                {
                    text.enabled = true;
                }
                foreach (TMPro.TMP_Text text in go.GetComponentsInChildren<TMPro.TextMeshProUGUI>())
                {
                    text.enabled = true;
                }
                if (go.GetComponent<Image>())
                {
                    go.GetComponent<Image>().enabled = true;
                }
                if (go.GetComponent<TMPro.TextMeshProUGUI>())
                {
                    go.GetComponent<TMPro.TextMeshProUGUI>().enabled = true;
                }
                if (go.GetComponent<TMPro.TMP_Text>())
                {
                    go.GetComponent<TMPro.TMP_Text>().enabled = true;
                }
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
            ShowError("Not a valid port!");
        }
    }

    public void EditHealth(string health)
    {
        if (!int.TryParse(health, out SyncData.health))
        {
            ShowError("Not a number!");
        }
    }

    public void EditName(string name)
    {
        SyncData.serverName = name;
    }
}
