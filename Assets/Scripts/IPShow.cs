using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Net;

public class IPShow : MonoBehaviour {

    public TMPro.TMP_Text inputField;

    // Update is called once per frame
    void Start() {
        string externalip = new WebClient().DownloadString("http://icanhazip.com");
        inputField.text = "Your IP: " + externalip;
    }
}
