using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MyParty : MonoBehaviour
{
    private int count;

    public GameObject playerName;

    public Transform parent;

    public GameObject chatBox;

    public Transform chatWindow;

    public TMPro.TMP_InputField input;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        count++;
        if (count > 120)
        {
            count = 0;
            for (int i = 0; i < parent.childCount; i++)
            {
                Destroy(parent.GetChild(i).gameObject);
            }
            foreach (ActualPlayerCheck player in FindObjectsOfType<ActualPlayerCheck>())
            {
                GameObject pName = Instantiate(playerName, parent);
                if (player.gameObject.GetComponent<SetupLoading>())
                {
                    pName.GetComponent<TMPro.TextMeshProUGUI>().text = player.gameObject.GetComponent<SetupLoading>().nameTag.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text;
                }
                else
                {
                    pName.GetComponent<TMPro.TextMeshProUGUI>().text = player.gameObject.GetComponent<AISetup>().nameTag.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text;
                }
            }
        }
    }

    public void Send()
    {
        if (input.text != "" || input.text != null)
        {
            if (NetworkClient.isConnected)
            {
                FindObjectOfType<ChatInterfacer>().CmdSendChat(SyncData.name + ": " + input.text);
            }
            else
            {
                CreateChat(SyncData.name + ": " + input.text);
            }
            input.text = "";
        }
    }

    public void CreateChat(string message)
    {
        GameObject messageBox = Instantiate(chatBox, chatWindow);
        messageBox.GetComponent<TMPro.TextMeshProUGUI>().text = message;
    }
}
