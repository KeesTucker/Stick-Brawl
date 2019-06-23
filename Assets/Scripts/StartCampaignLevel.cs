using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.LiteNetLib4Mirror;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartCampaignLevel : MonoBehaviour
{
    LiteNetLib4MirrorTransport transport;

    public int id;

    public int gameMode;

    public int numBots;

    public int chunkID;

    public int health = 200;

    public int botHealth = 100;

    private string[] grades = new string[] { "F", "F Plus", "D", "D Plus", "C", "C Plus", "B", "B Plus", "A", "A Plus" };

    void Start()
    {
        transport = NetworkManager.singleton.gameObject.GetComponent<LiteNetLib4MirrorTransport>();
        if (PlayerPrefs.HasKey(chunkID.ToString() + "level"))
        {
            Debug.Log(PlayerPrefs.GetFloat(chunkID.ToString() + "level"));
            transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "Grade: " + grades[Mathf.Clamp((int)(PlayerPrefs.GetFloat(chunkID.ToString() + "level") / 10) - 1, 0, 10)];
        }
        else
        {
            transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "Uncompleted";
            if (!PlayerPrefs.HasKey((chunkID - 1).ToString() + "level") && chunkID != 0)
            {
                GetComponent<Button>().interactable = false;
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

    public void StartLevel()
    {
        SyncData.health = health;
        SyncData.numPlayers = numBots;
        SyncData.gameMode = gameMode;
        SyncData.chunkID = chunkID;
        SyncData.botHealth = botHealth;

        if (!NetworkServer.active)
        {
            CheckPorts();
            SyncData.serverName = SyncData.name + "s Campaign Server!";
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

        SyncData.isCampaign = true;

        StartCoroutine(WaitForServer());
    }

    IEnumerator WaitForServer()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Main");
        GameObject.Find("LocalConnectionLobby").GetComponent<NetworkLobbyPlayer>().CmdChangeReadyState(true);
    }
}
