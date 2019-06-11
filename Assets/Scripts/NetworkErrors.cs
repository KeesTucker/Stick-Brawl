using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using System.Net.Sockets;

public class NetworkErrors : MonoBehaviour
{
    public GameObject message;
    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        Transport.activeTransport.OnClientError.AddListener(Error);
        Transport.activeTransport.OnServerError.AddListener(Error);
    }

    private void Error(NetworkConnection arg1, ErrorMessage arg2)
    {
        ShowError(arg1.ToString() + " " + arg2.ToString());
    }

    public void Error(string arg0)
    {
        ShowError(arg0);
    }

    public void Error(SocketError arg0)
    {
        ShowError(arg0.ToString());
    }

    public void Error(int arg0, SocketError arg1)
    {
        ShowError(arg1.ToString() + " Code: " + arg0.ToString());
    }

    private void Error(Exception arg0)
    {
        ShowError(arg0.Message);
    }

    private void Error(int arg0, Exception arg1)
    {
        ShowError(arg1.Message + " Code: " + arg0.ToString());
    }

    private void ShowError(string msg)
    {
        GameObject confirm;
        if (GameObject.Find("NoDestroyCanvas/Message"))
        {
            confirm = GameObject.Find("NoDestroyCanvas/Message");
            confirm.transform.GetChild(1).GetChild(0).GetComponent<TMPro.TMP_Text>().text = "ERROR";
            confirm.transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<TMPro.TMP_Text>().text = "Network Error! INFO: " + msg;
            confirm.GetComponent<Animator>().SetTrigger("Entry");
        }
    }
}

