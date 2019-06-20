using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColouriserAI : MonoBehaviour {

    SpriteRenderer m_SpriteRenderer;
    //The Color to be assigned to the Renderer’s Material
    public Color m_NewColor;

    public GameObject local;

    public void ColourFind(Color color)
    {
        //Fetch the SpriteRenderer from the GameObject
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        if (local.GetComponent<ColourSetterAI>())
        {
            m_NewColor = color;
        }
        else if (local.GetComponent<ColourSetterLoad>())
        {
            m_NewColor = local.GetComponent<ColourSetterLoad>().m_NewColor;
        }

        //Set the SpriteRenderer to the Color defined by the Sliders
        m_SpriteRenderer.color = m_NewColor;
    }

    public void ColourFind()
    {
        //Fetch the SpriteRenderer from the GameObject
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        if (local.GetComponent<ColourSetterAI>())
        {
            m_NewColor = local.GetComponent<ColourSetterAI>().m_NewColor;
        }
        else if (local.GetComponent<ColourSetterLoad>())
        {
            m_NewColor = local.GetComponent<ColourSetterLoad>().m_NewColor;
        }

        //Set the SpriteRenderer to the Color defined by the Sliders
        m_SpriteRenderer.color = m_NewColor;
    }

}
