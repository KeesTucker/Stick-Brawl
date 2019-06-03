using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;
using UnityEngine.EventSystems;

public class SetOptions : MonoBehaviour {

    public TMPro.TMP_InputField inputField;
    public EscMenu esc;
    public NetworkManager manager;

    public GameObject LArrow;
    public GameObject RArrow;

    public GameObject options;
    public GameObject menu;

    public TMPro.TMP_Text text;
    public int vsync = 0;
    public int aA = 0;
    public bool isAA;
    public bool isVsync;
    public GameObject keybinds;

    void Start()
    {
        if (PlayerPrefs.HasKey("vsync") && isVsync)
        {
            if (PlayerPrefs.GetInt("vsync") == 1)
            {
                Application.targetFrameRate = -1;
            }
            else
            {
                Application.targetFrameRate = 60;
            }
            vsync = PlayerPrefs.GetInt("vsync");
            Vsync();
            Vsync();
        }
        else
        {
            Application.targetFrameRate = -1;
        }
        QualitySettings.vSyncCount = vsync;

        if (PlayerPrefs.HasKey("aA") && isAA)
        {
            aA = PlayerPrefs.GetInt("aA");
            AA();
            AA();
        }

        QualitySettings.antiAliasing = aA;

        manager = GameObject.Find("_NetworkManager").GetComponent<NetworkManager>();
    }

    public void SetName()
    {
        SyncData.name = inputField.text;
        PlayerPrefs.SetString("name", SyncData.name);
        PlayerPrefs.Save();
        GameObject.Find("Nametag(Clone)").GetComponent<SyncName>().UpdateName();
    }

    public void Resume()
    {
        esc.esc = true;
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void MainMenu()
    {
        manager.StopClient();
        manager.StopServer();
        SceneManager.LoadScene("Lobby");
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void Quit()
    {
        Application.Quit();
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void SwitchArrow()
    {
        LArrow.SetActive(!LArrow.activeInHierarchy);
        RArrow.SetActive(!RArrow.activeInHierarchy);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void SwitchOptions()
    {
        options.SetActive(!options.activeInHierarchy);
        menu.SetActive(!options.activeInHierarchy);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void Vsync()
    {
        if (vsync == 0)
        {
            vsync = 1;
            QualitySettings.vSyncCount = vsync;
            PlayerPrefs.SetInt("vsync", vsync);
            PlayerPrefs.Save();
            Application.targetFrameRate = -1;
            text.text = "ON";
        }
        else
        {
            vsync = 0;
            QualitySettings.vSyncCount = vsync;
            PlayerPrefs.SetInt("vsync", vsync);
            PlayerPrefs.Save();
            Application.targetFrameRate = -1;
            text.text = "OFF";
        }
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void AA()
    {
        if (aA == 0)
        {
            aA = 2;
            PlayerPrefs.SetInt("aA", aA);
            PlayerPrefs.Save();
            QualitySettings.antiAliasing = aA;
            text.text = "ON";
        }
        else
        {
            aA = 0;
            PlayerPrefs.SetInt("aA", aA);
            PlayerPrefs.Save();
            QualitySettings.antiAliasing = aA;
            text.text = "OFF";
        }
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void OpenKeybinds()
    {
        keybinds.SetActive(true);
        options.SetActive(false);
    }

    public void Back()
    {
        options.SetActive(false);
        menu.SetActive(true);
    }

    public void BackKey()
    {
        options.SetActive(true);
        keybinds.SetActive(false);
    }
}
