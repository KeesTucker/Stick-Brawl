using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscMenu : MonoBehaviour {

    public GameObject escMenu;
    public bool state = false;
    public bool esc;
    public Vector2 res;
    public bool fullScreen;
    public int vsync;
    public int aA;

    void Start()
    {
        if (PlayerPrefs.HasKey("health"))
        {
            SyncData.health = PlayerPrefs.GetInt("health");
        }
        if (PlayerPrefs.HasKey("playerNum"))
        {
            SyncData.numPlayers = PlayerPrefs.GetInt("playerNum");
        }
        if (PlayerPrefs.HasKey("worldSize"))
        {
            SyncData.worldSize = PlayerPrefs.GetInt("worldSize");
        }
        if (PlayerPrefs.HasKey("sfx"))
        {
            SyncData.sfx = (float)PlayerPrefs.GetInt("sfx") / 100f;
        }
        if (PlayerPrefs.HasKey("volume"))
        {
            SyncData.volume = (float)PlayerPrefs.GetInt("volume") / 100f;
        }
        if (PlayerPrefs.HasKey("resX") && PlayerPrefs.HasKey("resY"))
        {
            res = new Vector2(PlayerPrefs.GetInt("resX"), PlayerPrefs.GetInt("resY"));
        }
        else
        {
            res = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
            PlayerPrefs.SetInt("resX", Screen.currentResolution.width);
            PlayerPrefs.SetInt("resY", Screen.currentResolution.height);
            PlayerPrefs.Save();
        }
        if (PlayerPrefs.HasKey("fullScreen"))
        {
            if (PlayerPrefs.GetInt("fullScreen") == 0)
            {
                fullScreen = false;
            }
            else
            {
                fullScreen = true;
            }
        }
        else
        {
            fullScreen = true;
        }
        Screen.SetResolution((int)res.x, (int)res.y, fullScreen);

        if (PlayerPrefs.HasKey("vsync"))
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
        }
        else
        {
            Application.targetFrameRate = -1;
        }
        QualitySettings.vSyncCount = vsync;

        if (PlayerPrefs.HasKey("aA"))
        {
            aA = PlayerPrefs.GetInt("aA");
        }

        QualitySettings.antiAliasing = aA;

        if (PlayerPrefs.HasKey("a"))
        {
            if (PlayerPrefs.GetString("a") != "")
            {
                SyncData.a = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("a"));
            }
            else
            {
                PlayerPrefs.SetString("a", KeyCode.A.ToString());
                PlayerPrefs.Save();
            }
        }
        else
        {
            PlayerPrefs.SetString("a", KeyCode.A.ToString());
            PlayerPrefs.Save();
        }
        if (PlayerPrefs.HasKey("d"))
        {
            if (PlayerPrefs.GetString("d") != "")
            {
                SyncData.d = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("d"));
            }
            else
            {
                PlayerPrefs.SetString("d", KeyCode.D.ToString());
                PlayerPrefs.Save();
            }
        }
        else
        {
            PlayerPrefs.SetString("d", KeyCode.D.ToString());
            PlayerPrefs.Save();
        }
        if (PlayerPrefs.HasKey("space"))
        {
            if (PlayerPrefs.GetString("space") != "")
            {
                SyncData.space = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("space"));
            }
            else
            {
                PlayerPrefs.SetString("space", KeyCode.Space.ToString());
                PlayerPrefs.Save();
            }
        }
        else
        {
            PlayerPrefs.SetString("space", KeyCode.Space.ToString());
            PlayerPrefs.Save();
        }
        if (PlayerPrefs.HasKey("f"))
        {
            if (PlayerPrefs.GetString("f") != "")
            {
                SyncData.f = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("f"));
            }
            else
            {
                PlayerPrefs.SetString("f", KeyCode.F.ToString());
                PlayerPrefs.Save();
            }
        }
        else
        {
            PlayerPrefs.SetString("f", KeyCode.F.ToString());
            PlayerPrefs.Save();
        }
        if (PlayerPrefs.HasKey("r"))
        {
            if (PlayerPrefs.GetString("r") != "")
            {
                SyncData.r = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("r"));
            }
            else
            {
                PlayerPrefs.SetString("r", KeyCode.R.ToString());
                PlayerPrefs.Save();
            }
        }
        else
        {
            PlayerPrefs.SetString("r", KeyCode.R.ToString());
            PlayerPrefs.Save();
        }
        if (PlayerPrefs.HasKey("i"))
        {
            if (PlayerPrefs.GetString("i") != "")
            {
                SyncData.i = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("i"));
            }
            else
            {
                PlayerPrefs.SetString("i", KeyCode.I.ToString());
                PlayerPrefs.Save();
            }
        }
        else
        {
            PlayerPrefs.SetString("i", KeyCode.I.ToString());
            PlayerPrefs.Save();
        }
        
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("escape"))
        {
            esc = true;
        }
        if (esc)
        {
            esc = false;
            escMenu.SetActive(state);
            if (transform.childCount > 1)
            {
                transform.GetChild(1).gameObject.SetActive(!state);
                GameObject.Find("LoadingPlayer").GetComponent<LoadingControl>().enabled = !state;
            }
            else
            {
                transform.GetChild(0).gameObject.SetActive(!state);
                GameObject.Find("LocalPlayer").GetComponent<PlayerControl>().enabled = state;
            }

            state = !state;
        }
	}
}
