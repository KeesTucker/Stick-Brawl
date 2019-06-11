using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountSettings : MonoBehaviour
{
    public TMPro.TMP_Text text;

    public TMPro.TMP_Text achievments;

    void Start()
    {
        SyncData.name = PlayerPrefs.GetString("name");
        text.text = SyncData.name;
        UpdateCount();
    }

    public void SetName(string input)
    {
        SyncData.name = input;
        PlayerPrefs.SetString("name", SyncData.name);
        PlayerPrefs.Save();
        if (GameObject.Find("Nametag(Clone)"))
        {
            GameObject.Find("Nametag(Clone)").GetComponent<SyncName>().UpdateName();
        }
        text.text = SyncData.name;

        if (GameObject.Find("LocalConnection"))
        {
            GameObject.Find("LocalConnection").GetComponent<PlayerManagement>().CmdUpdateColorAndNameN(SyncData.name);
        }
    }

    public void ShowLeaderboard()
    {
        //Show GPGS leaderboard
        Debug.Log("GPGS LEADERBOARD SHOW");
    }

    public void ShowAchievements()
    {
        //Show GPGS leaderboard
        Debug.Log("GPGS ACHIEVEMENTS SHOW");
    }

    public void SignOut()
    {
        Debug.Log("SIGN OUT GPGS");
    }

    public void UpdateCount()
    {
        //Update Achievement Count Text
        //achievments.text = "";
        Debug.Log("Updated Achievement COUNT");
    }
}
