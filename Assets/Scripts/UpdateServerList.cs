using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror.LiteNetLib4Mirror;

public class UpdateServerList : MonoBehaviour
{
    public Transform UIParent;

    public GameObject listObject;

    public Animator joinPanel;
    public Animator hostPanel;
    public Animator campaignPanel;

    public NetworkDiscoveryHUD hud;

    // Update is called once per frame
    void Update()
    {
        if (hud == null)
        {
            hud = NetworkDiscoveryManager.Singleton.gameObject.GetComponent<NetworkDiscoveryHUD>();
        }
        if (joinPanel == null)
        {
            joinPanel = GameObject.Find("Canvas/Join").GetComponent<Animator>();
        }
        if (UIParent == null)
        {
            joinPanel.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(0);
        }
        if (joinPanel.GetCurrentAnimatorStateInfo(0).IsName("IdlePanel") || hostPanel.GetCurrentAnimatorStateInfo(0).IsName("IdlePanel") || campaignPanel.GetCurrentAnimatorStateInfo(0).IsName("IdlePanel"))
        {
            if (hud._noDiscovering)
            {
                hud.StartSearch();
            }
        }
        else
        {
            if (!hud._noDiscovering)
            {
                hud.EndSearch();
            }
        }
    }

    public void Refresh()
    {
        if (joinPanel.GetCurrentAnimatorStateInfo(0).IsName("IdlePanel"))
        {
            if (SyncData.servers.Count != 0)
            {
                //oldServers = SyncData.servers;

                for (int i = 0; i < UIParent.childCount; i++)
                {
                    Destroy(UIParent.GetChild(i).gameObject);
                }

                foreach (ServerUIObject UIObject in SyncData.servers)
                {
                    Debug.Log("New server listing!");
                    GameObject instantiatedListObject = Instantiate(listObject, UIParent);
                    instantiatedListObject.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = UIObject.name;
                    instantiatedListObject.GetComponent<JoinServerList>().endpoint = UIObject.endpoint;
                }
            }
        }
    }
}
