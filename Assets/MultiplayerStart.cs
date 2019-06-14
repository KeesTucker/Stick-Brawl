using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.LiteNetLib4Mirror;

public class MultiplayerStart : MonoBehaviour
{
    LiteNetLib4MirrorTransport transport;

    // Start is called before the first frame update
    void Start()
    {
        transport = NetworkManager.singleton.gameObject.GetComponent<LiteNetLib4MirrorTransport>();
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

    public void StartLevel()
    {
        if (!NetworkServer.active)
        {
            CheckPorts();
            SyncData.serverName = SyncData.name + "s FFA Server!";
            if (NetworkClient.isConnected)
            {
                NetworkManager.singleton.StopClient();
                NetworkManager.singleton.StartHost();
            }
            else
            {
                NetworkManager.singleton.StartHost();
            }
        }

        StartCoroutine(Load());
    }

    IEnumerator Load()
    {
        yield return new WaitForEndOfFrame();

        NetworkManager.singleton.StartGame();

        foreach (PlayerManagement playerManagement in FindObjectsOfType<PlayerManagement>())
        {
            playerManagement.isCampaign = false;
            playerManagement.StartGame();
        }
    }
}
