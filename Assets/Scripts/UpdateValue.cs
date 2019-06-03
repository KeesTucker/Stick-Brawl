using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpdateValue : MonoBehaviour {

    public int volume = 100;
    public int volumeSFX = 100;
    public TMPro.TMP_Text text;
    public Vector2 res;
    public bool fullScreen = true;
    public bool isRes = false;
    public TMPro.TMP_Text textFullScreen;
    public int health = 400;
    public int worldSize = 13;
    [SerializeField]
    public Vector2[] resolutions;

    void Start()
    {
        volume = 100;
        volumeSFX = 50;
        health = 400;
        worldSize = 13;
        
        if (text.text == "400")
        {
            if (PlayerPrefs.HasKey("health"))
            {
                SyncData.health = PlayerPrefs.GetInt("health");
                health = PlayerPrefs.GetInt("health");
            }
            text.text = SyncData.health.ToString();
        }
        if (text.text == "13")
        {
            if (PlayerPrefs.HasKey("worldSize"))
            {
                SyncData.worldSize = PlayerPrefs.GetInt("worldSize");
                worldSize = PlayerPrefs.GetInt("worldSize");
            }
            text.text = SyncData.worldSize.ToString();
        }
        if (text.gameObject.tag == "sfx")
        {
            if (PlayerPrefs.HasKey("sfx"))
            {
                volumeSFX = PlayerPrefs.GetInt("sfx");
                SyncData.sfx = (float)volumeSFX / 100f;
            }
            text.text = volumeSFX.ToString();
        }
        if (text.gameObject.tag == "volume")
        {
            if (PlayerPrefs.HasKey("volume"))
            {
                volume = PlayerPrefs.GetInt("volume");
                SyncData.volume = (float)volume / 100f;
            }
            text.text = volume.ToString();
        }
        
        if (isRes)
        {
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
            text.text = res.x.ToString() + "X" + res.y.ToString();
            textFullScreen.text = fullScreen.ToString();
            Screen.SetResolution((int)res.x, (int)res.y, fullScreen);
        }
    }

	public void VolumeDown()
    {
        volume -= 10;
        volume = Mathf.Clamp(volume, 0, 100);
        PlayerPrefs.SetInt("volume", volume);
        PlayerPrefs.Save();
        SyncData.volume = (float)volume / 100f;
        text.text = volume.ToString();
        EventSystem.current.SetSelectedGameObject(null);
    }
    public void VolumeUp()
    {
        volume += 10;
        volume = Mathf.Clamp(volume, 0, 100);
        PlayerPrefs.SetInt("volume", volume);
        PlayerPrefs.Save();
        SyncData.volume = (float)volume / 100f;
        text.text = volume.ToString();
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void VolumeDownSFX()
    {
        volumeSFX -= 10;
        volumeSFX = Mathf.Clamp(volumeSFX, 0, 100);
        text.text = volumeSFX.ToString();
        PlayerPrefs.SetInt("sfx", volumeSFX);
        PlayerPrefs.Save();
        SyncData.sfx = (float)volumeSFX / 100f;
        Debug.Log(volumeSFX.ToString() + (volumeSFX / 100f).ToString());
        EventSystem.current.SetSelectedGameObject(null);
    }
    public void VolumeUpSFX()
    {
        volumeSFX += 10;
        volumeSFX = Mathf.Clamp(volumeSFX, 0, 100);
        text.text = volumeSFX.ToString();
        PlayerPrefs.SetInt("sfx", volumeSFX);
        PlayerPrefs.Save();
        SyncData.sfx = (float)volumeSFX / 100f;
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void ResUp()
    {
        for (int i = 0; i < resolutions.Length; i++)
        { 
            if (Screen.width == resolutions[i].x && Screen.height == resolutions[i].y && i <= resolutions.Length - 1)
            {
                res = new Vector2(resolutions[Mathf.Clamp(i + 1, 6, resolutions.Length)].x, resolutions[Mathf.Clamp(i + 1, 0, resolutions.Length)].y);
                Screen.SetResolution((int)res.x, (int)res.y, fullScreen);
                PlayerPrefs.SetInt("resX", (int)res.x);
                PlayerPrefs.SetInt("resY", (int)res.y);
                //text.text = res.x.ToString() + "X" + res.y.ToString();
                text.text = Screen.width + "X" + Screen.height;
                StartCoroutine("ResUpdate");
                PlayerPrefs.Save();
            }
        }
        text.text = Screen.width + "X" + Screen.height;
    }
    public void ResDown()
    {
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (Screen.width == resolutions[i].x && Screen.height == resolutions[i].y && i >= 7)
            {
                res = new Vector2(resolutions[Mathf.Clamp(i - 1, 6, resolutions.Length)].x, resolutions[Mathf.Clamp(i - 1, 0, resolutions.Length)].y);
                Screen.SetResolution((int)res.x, (int)res.y, fullScreen);
                PlayerPrefs.SetInt("resX", (int)res.x);
                PlayerPrefs.SetInt("resY", (int)res.y);
                //text.text = res.x.ToString() + "X" + res.y.ToString();
                text.text = Screen.width + "X" + Screen.height;
                StartCoroutine("ResUpdate");
                PlayerPrefs.Save();
            }
        }
        text.text = Screen.width + "X" + Screen.height;
    }
    public void SwitchFullScreen()
    {
        fullScreen = !fullScreen;
        Screen.fullScreen = fullScreen;
        textFullScreen.text = fullScreen.ToString();
        if (fullScreen == true)
        {
            PlayerPrefs.SetInt("fullScreen", 1);
        }
        else
        {
            PlayerPrefs.SetInt("fullScreen", 0);
        }
        PlayerPrefs.Save();
    }

    public void HealthDown()
    {
        health -= 100;
        health = Mathf.Clamp(health, 100, 1000);
        text.text = health.ToString();
        SyncData.health = health;
        PlayerPrefs.SetInt("health", health);
        PlayerPrefs.Save();
        EventSystem.current.SetSelectedGameObject(null);
    }
    public void HealthUp()
    {
        health += 100;
        health = Mathf.Clamp(health, 100, 1000);
        text.text = health.ToString();
        SyncData.health = health;
        PlayerPrefs.SetInt("health", health);
        PlayerPrefs.Save();
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void WorldDown()
    {
        worldSize -= 1;
        worldSize = Mathf.Clamp(worldSize, 2, 200);
        text.text = worldSize.ToString();
        SyncData.worldSize = worldSize;
        PlayerPrefs.SetInt("worldSize", worldSize);
        PlayerPrefs.Save();
        EventSystem.current.SetSelectedGameObject(null);
    }
    public void WorldUp()
    {
        worldSize += 1;
        worldSize = Mathf.Clamp(worldSize, 2, 200);
        text.text = worldSize.ToString();
        SyncData.worldSize = worldSize;
        PlayerPrefs.SetInt("worldSize", worldSize);
        PlayerPrefs.Save();
        EventSystem.current.SetSelectedGameObject(null);
    }

    IEnumerator ResUpdate()
    {
        yield return new WaitForSeconds(0.2f);
        text.text = Screen.width + "X" + Screen.height;
    }
}
