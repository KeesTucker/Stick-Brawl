using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colouriser : MonoBehaviour {

    SpriteRenderer m_SpriteRenderer;
    //The Color to be assigned to the Renderer’s Material
    public Color m_NewColor;

    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        //Fetch the SpriteRenderer from the GameObject
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_NewColor = GameObject.Find("Local").GetComponent<ColourSetter>().m_NewColor;

        //Set the SpriteRenderer to the Color defined by the Sliders
        m_SpriteRenderer.color = m_NewColor;
    }
}
