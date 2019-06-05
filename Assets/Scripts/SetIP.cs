using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using System;

public class SetIP : MonoBehaviour {

    public TMPro.TMP_InputField mainInputField;

    int result;

    void Start()
    {
        if (mainInputField.text == "5 Bots")
        {
            if (PlayerPrefs.HasKey("playerNum"))
            {
                SyncData.numPlayers = PlayerPrefs.GetInt("playerNum");
                mainInputField.text = PlayerPrefs.GetInt("playerNum").ToString() + " Bots";
            }
        }
    }

    public void ValueChanged()
    {
        NetworkManager.singleton.networkAddress = mainInputField.text;
    }

    public void PlayerNumValueChanged()
    {
        try
        {
            int number = int.Parse(mainInputField.text);
            result = number;
        }
        catch (FormatException)
        {
            result = 5;
            mainInputField.text = "Thats not a number -_-";
        }
        catch (OverflowException)
        {
            result = 5;
            mainInputField.text = "Thats not a number -_-";
        }
        SyncData.numPlayers = result;
        PlayerPrefs.SetInt("playerNum", result);
        PlayerPrefs.Save();
    }
}
