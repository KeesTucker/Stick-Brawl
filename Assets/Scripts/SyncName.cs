using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SyncName : NetworkBehaviour {

    public TMPro.TMP_Text text;

    [SyncVar]
    public string playerName;

    public GameObject parent;

    [SyncVar]
    public GameObject serverParent;

    public int wins;
    public HealthAI healthAI;

    public bool instantiatedLobbyPlayer = false;

    // Use this for initialization
    IEnumerator Start () {
        if (GameObject.Find("LoadingPlayer"))
        {
            if (PlayerPrefs.HasKey("wins"))
            {
                wins = PlayerPrefs.GetInt("wins");
            }
            if (PlayerPrefs.HasKey("name") && !instantiatedLobbyPlayer)
            {
                SyncData.name = PlayerPrefs.GetString("name");
                playerName = SyncData.name;

                text.text = playerName;
            }
            text.color = parent.GetComponent<ColourSetterLoad>().m_NewColor;
        }
        else
        {
            yield return new WaitForSeconds(1f);
            if (!hasAuthority)
            {
                parent = serverParent;
                healthAI = parent.GetComponent<HealthAI>();
                text.text = playerName;
            }
            text.color = parent.GetComponent<ColourSetterAI>().m_NewColor;
        }
    }

    public void UpdateColor()
    {
        text.color = parent.GetComponent<ColourSetterLoad>().m_NewColor;
    }

    // Update is called once per frame
    void LateUpdate() {
        if (parent)
        {
            if (healthAI)
            {
                if (healthAI.deaded)
                {
                    Destroy(gameObject);
                }
            }
            transform.position = parent.transform.position + new Vector3(0, 10, 10);
        }
        else
        {
            Destroy(gameObject);
        }
	}

    public override void OnStartAuthority()
    {
        if (hasAuthority)
        {
            if (PlayerPrefs.HasKey("name"))
            {
                SyncData.name = PlayerPrefs.GetString("name");
            }
            if (PlayerPrefs.HasKey("wins"))
            {
                wins = PlayerPrefs.GetInt("wins");
            }
            else
            {
                wins = 0;
            }
            CmdUpdateName(SyncData.name, wins);
            UpdateName();
        }
    }

    public void UpdateName()
    {
        playerName = SyncData.name;

        text.text = playerName;
    }

    [Command]
    public void CmdUpdateName(string nameS, int winsS)
    {
        playerName = nameS;
        RpcUpdateTxt();
        text.text = playerName;
    }

    [Command]
    public void CmdUpdateParent(GameObject parentObject)
    {
        parent = parentObject;
        healthAI = parent.GetComponent<HealthAI>();
        serverParent = parent;
        RpcUpdateParent(parent);
    }

    [ClientRpc]
    public void RpcUpdateParent(GameObject parentObject)
    {
        parent = parentObject;
        healthAI = parent.GetComponent<HealthAI>();
    }

    [ClientRpc]
    public void RpcUpdateTxt()
    {
        text.text = playerName;
    }
}
