using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourSetterLoad : MonoBehaviour {

    public Color m_NewColor;

    public Material grappleMat;
    //These are the values that the Color Sliders return
    float m_Red, m_Blue, m_Green;

    public bool local = false;

    public bool localLobby = false;

    void Start()
    {
        if (localLobby)
        {
            if (SyncData.color == null || SyncData.color == new Color())
            {
                if (PlayerPrefs.HasKey("r") && PlayerPrefs.HasKey("g") && PlayerPrefs.HasKey("b"))
                {
                    SyncData.color = new Color(PlayerPrefs.GetFloat("r"), PlayerPrefs.GetFloat("g"), PlayerPrefs.GetFloat("b"));
                }
                else
                {
                    Color randomColour = Color.HSVToRGB(Random.Range(0, 1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));
                    SyncData.color = randomColour;
                    PlayerPrefs.SetFloat("r", randomColour.r);
                    PlayerPrefs.SetFloat("g", randomColour.g);
                    PlayerPrefs.SetFloat("b", randomColour.b);
                }
            }
            SetColor(SyncData.color);
        }
        else
        {
            foreach (ColouriserAI cai in GetComponentsInChildren<ColouriserAI>())
            {
                cai.ColourFind();
            }
        }
    }

    public void SetColor(Color color)
    {
        m_NewColor = color;
        foreach (ColouriserAI cai in GetComponentsInChildren<ColouriserAI>())
        {
            PlayerPrefs.SetFloat("r", color.r);
            PlayerPrefs.SetFloat("g", color.g);
            PlayerPrefs.SetFloat("b", color.b);
            if (gameObject.name == "LoadingPlayer")
            {
                SyncData.color = color;
            }
            cai.ColourFind();
        }
    }
}
