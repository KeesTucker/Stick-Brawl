using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public int vsync;
    public int targetFPS;
    public int AA;
    public Vector2 res;
    public int resX;

    public TMPro.TMP_Dropdown AADropdown;
    public TMPro.TMP_Dropdown FPSDropdown;
    public TMPro.TMP_Dropdown ResDropdown;

    public Vector2[] resList;

    public int gameModeCount;
    public int mapCount;

    void Start()
    {
        SyncData.gameModes = new bool[gameModeCount];
        SyncData.maps = new bool[mapCount];

        //Vsync
        if (PlayerPrefs.HasKey("vsync"))
        {
            if (PlayerPrefs.GetInt("vsync") == 1)
            {
                Application.targetFrameRate = -1;
                SyncData.targetFPS = PlayerPrefs.GetInt("targetFPS");
                vsync = 1;
                FPSDropdown.value = 3;
            }
            else
            {
                vsync = 0;
                if (PlayerPrefs.HasKey("targetFPS"))
                {
                    Application.targetFrameRate = PlayerPrefs.GetInt("targetFPS");
                    SyncData.targetFPS = PlayerPrefs.GetInt("targetFPS");
                    FPSDropdown.value = (int)(Mathf.Log10(PlayerPrefs.GetInt("targetFPS") / 15) / Mathf.Log10(2));
                }
                else
                {
                    PlayerPrefs.SetInt("targetFPS", 60);
                    SyncData.targetFPS = PlayerPrefs.GetInt("targetFPS");
                    Application.targetFrameRate = 120;
                    FPSDropdown.value = 2;
                }
            }
        }
        else
        {
            PlayerPrefs.SetInt("vsync", 1);
            PlayerPrefs.SetInt("targetFPS", -1);
            Application.targetFrameRate = -1;
            SyncData.targetFPS = PlayerPrefs.GetInt("targetFPS");
            vsync = 1;
            FPSDropdown.value = 3;
        }
        vsync = PlayerPrefs.GetInt("vsync");
        QualitySettings.vSyncCount = vsync;
        
        //Anti-Aliasing
        if (PlayerPrefs.HasKey("aA"))
        {
            AA = PlayerPrefs.GetInt("aA");
        }
        else
        {
            AA = 0;
            PlayerPrefs.SetInt("aA", 0);
        }
        QualitySettings.antiAliasing = AA;
        AADropdown.value = AA / 2;

        if(PlayerPrefs.HasKey("resX") && PlayerPrefs.HasKey("resY"))
        {
            res = new Vector2(PlayerPrefs.GetInt("resX"), PlayerPrefs.GetInt("resY"));
            for (int i = 0; i < resList.Length; i++)
            {
                if (res.x == resList[i].x)
                {
                    ResDropdown.value = i;
                }
            }
        }
        else
        {
            res = new Vector2(Screen.resolutions[Screen.resolutions.Length - 1].width, Screen.resolutions[Screen.resolutions.Length - 1].height);
            PlayerPrefs.SetInt("resX", Screen.currentResolution.width);
            PlayerPrefs.SetInt("resY", Screen.currentResolution.height);
            PlayerPrefs.Save();
            ResDropdown.value = 3;
        }
        Screen.SetResolution((int)res.x, (int)res.x * Screen.currentResolution.height / Screen.currentResolution.width, FullScreenMode.FullScreenWindow);
    }

    public void AntiAliasingUpdate(int value)
    {
        AA = value * 2;
        PlayerPrefs.SetInt("aA", value * 2);
        QualitySettings.antiAliasing = AA;
    }

    public void FPSUpdate(int value)
    {
        if (value == 3)
        {
            PlayerPrefs.SetInt("vsync", 1);
            Application.targetFrameRate = -1;
            PlayerPrefs.SetInt("targetFPS", -1);
            SyncData.targetFPS = PlayerPrefs.GetInt("targetFPS");
            vsync = 1;
        }
        else
        {
            PlayerPrefs.SetInt("vsync", 0);
            vsync = 0;

            Debug.Log("Target FPS: " + ((int)(15 * Mathf.Pow(2, value))).ToString());
            PlayerPrefs.SetInt("targetFPS", (int)(15 * Mathf.Pow(2, value)));
            Application.targetFrameRate = (int)(22.5f * Mathf.Pow(2, value));
            SyncData.targetFPS = PlayerPrefs.GetInt("targetFPS");
        }
    }

    public void ResUpdate(int value)
    {
        if (value == 3)
        {
            res = new Vector2(Screen.resolutions[Screen.resolutions.Length - 1].width, Screen.resolutions[Screen.resolutions.Length - 1].width);
            PlayerPrefs.SetInt("resX", (int)res.x);
            PlayerPrefs.SetInt("resY", (int)res.y);
        }
        else
        {
            res = resList[value];
            PlayerPrefs.SetInt("resX", Screen.currentResolution.width);
            PlayerPrefs.SetInt("resY", Screen.currentResolution.height);
        }
        Screen.SetResolution((int)res.x, (int)res.x * Screen.currentResolution.height / Screen.currentResolution.width, FullScreenMode.FullScreenWindow);
    }
}
