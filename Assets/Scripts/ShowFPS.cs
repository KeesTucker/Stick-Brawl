using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowFPS : MonoBehaviour
{
    public TMPro.TMP_Text text;
    public GameObject fPSObject;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (fPSObject == null)
        {
            fPSObject = GameObject.Find("FPS");
        }
        if (PlayerPrefs.HasKey("showFPS"))
        {
            if (PlayerPrefs.GetInt("showFPS") == 1)
            {
                fPSObject.SetActive(true);
                text.text = "Hide FPS";
            }
            else
            {
                fPSObject.SetActive(false);
                text.text = "Show FPS";
            }
        }
        else
        {
            fPSObject.SetActive(false);
            text.text = "Show FPS";
            PlayerPrefs.SetInt("showFPS", 0);
        }
    }

    public void FPSSwitch()
    {
        if (fPSObject == null)
        {
            fPSObject = GameObject.Find("FPS");
        }
        if (PlayerPrefs.GetInt("showFPS") == 1)
        {
            fPSObject.SetActive(false);
            text.text = "Show FPS";
            PlayerPrefs.SetInt("showFPS", 0);
        }
        else
        {
            fPSObject.SetActive(true);
            text.text = "Hide FPS";
            PlayerPrefs.SetInt("showFPS", 1);
        }
    }
}
