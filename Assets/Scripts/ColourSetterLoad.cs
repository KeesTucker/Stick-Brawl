using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourSetterLoad : MonoBehaviour {

    public Color m_NewColor;

    public Material grappleMat;
    //These are the values that the Color Sliders return
    float m_Red, m_Blue, m_Green;

    public bool local = false;

    void Start()
    {
        foreach (ColouriserAI cai in GetComponentsInChildren<ColouriserAI>())
        {
            cai.ColourFind();
        }
    }

    public void SetColor(Color color)
    {
        m_NewColor = color;
        foreach (ColouriserAI cai in GetComponentsInChildren<ColouriserAI>())
        {
            cai.ColourFind();
        }
    }
}
