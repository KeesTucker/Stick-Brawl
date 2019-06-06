using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
    public TMPro.TMP_Text sfx;
    public TMPro.TMP_Text volume;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("sfx"))
        {
            SyncData.sfx = PlayerPrefs.GetInt("sfx");
            sfx.text = SyncData.sfx.ToString();
        }
        else
        {
            SyncData.sfx = 100;
            PlayerPrefs.SetInt("sfx", 100);
            sfx.text = SyncData.sfx.ToString();
        }

        if (PlayerPrefs.HasKey("volume"))
        {
            SyncData.volume = PlayerPrefs.GetInt("volume");
            volume.text = SyncData.volume.ToString();
        }
        else
        {
            SyncData.volume = 100;
            PlayerPrefs.SetInt("volume", 100);
            volume.text = SyncData.volume.ToString();
        }
    }

    public void SFXUp()
    {
        PlayerPrefs.SetInt("sfx", Mathf.Clamp(PlayerPrefs.GetInt("sfx") + 10, 0, 100));
        SyncData.sfx = PlayerPrefs.GetInt("sfx");
        sfx.text = SyncData.sfx.ToString();
    }
    public void SFXDown()
    {
        PlayerPrefs.SetInt("sfx", Mathf.Clamp(PlayerPrefs.GetInt("sfx") - 10, 0, 100));
        SyncData.sfx = PlayerPrefs.GetInt("sfx");
        sfx.text = SyncData.sfx.ToString();
    }

    public void VolumeUp()
    {
        PlayerPrefs.SetInt("volume", Mathf.Clamp(PlayerPrefs.GetInt("volume") + 10, 0, 100));
        SyncData.volume = PlayerPrefs.GetInt("volume");
        volume.text = SyncData.volume.ToString();
    }
    public void VolumeDown()
    {
        PlayerPrefs.SetInt("volume", Mathf.Clamp(PlayerPrefs.GetInt("volume") - 10, 0, 100));
        SyncData.volume = PlayerPrefs.GetInt("volume");
        volume.text = SyncData.volume.ToString();
    }
}
