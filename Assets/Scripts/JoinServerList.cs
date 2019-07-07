using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using Mirror.LiteNetLib4Mirror;
using Mirror;
using UnityEngine.UI;

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
                GameObject go = GameObject.Find("Canvas/Join");
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
            GameObject go = GameObject.Find("Canvas/Join");
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
