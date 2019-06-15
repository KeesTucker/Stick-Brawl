using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetMapsAndGamemodes : MonoBehaviour
{
    public bool isMap;
    public bool isGamemode;

    public int id;

    public Toggle toggle;

    public int specialID = 999;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        if (!toggle.interactable && specialID == 999)
        {
            PlayerPrefs.SetInt("Toggle " + isMap.ToString() + isGamemode.ToString() + id.ToString(), 0);
        }
        if (PlayerPrefs.HasKey("Toggle " + isMap.ToString() + isGamemode.ToString() + id.ToString()))
        {
            if (PlayerPrefs.GetInt("Toggle " + isMap.ToString() + isGamemode.ToString() + id.ToString()) == 1)
            {
                toggle.isOn = true;
            }
            else
            {
                toggle.isOn = false;
            }
        }
        else
        {
            if (specialID != 999)
            {
                PlayerPrefs.SetInt("Toggle " + isMap.ToString() + isGamemode.ToString() + id.ToString(), 1);
                toggle.isOn = true;
            }
        }

        yield return new WaitForEndOfFrame();

        if (isMap)
        {
            SyncData.maps[id] = toggle.isOn;
        }
        if (isGamemode)
        {
            SyncData.gameModes[id] = toggle.isOn;
        }

        UpdateIntereact();
    }

    public void UpdateIntereact()
    {
        if (specialID != 999)
        {
            if (isMap && !PlayerPrefs.HasKey("Owned " + ShopItemType.Maps.ToString() + " number " + specialID.ToString()))
            {
                toggle.interactable = false;
                toggle.isOn = false;
            }
            else if (isGamemode && !PlayerPrefs.HasKey("Owned " + ShopItemType.Gamemode.ToString() + " number " + specialID.ToString()))
            {
                toggle.interactable = false;
                toggle.isOn = false;
            }
            else if (!PlayerPrefs.HasKey("Toggle " + isMap.ToString() + isGamemode.ToString() + id.ToString()))
            {
                toggle.interactable = true;
                toggle.isOn = true;
                PlayerPrefs.SetInt("Toggle " + isMap.ToString() + isGamemode.ToString() + id.ToString(), 1);
            }
            else
            {
                toggle.interactable = true;
            }
        }
    }

    public void ToggleSetting(bool state)
    {
        int stateID;
        if (state == true)
        {
            stateID = 1;
        }
        else
        {
            stateID = 0;
        }

        PlayerPrefs.SetInt("Toggle " + isMap.ToString() + isGamemode.ToString() + id.ToString(), stateID);

        if (isMap && SyncData.maps != null)
        {
            SyncData.maps[id] = state;
        }
        if (isGamemode && SyncData.gameModes != null)
        {
            SyncData.gameModes[id] = state;
        }
    }
}
