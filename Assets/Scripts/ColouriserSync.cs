using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColouriserSync : MonoBehaviour {

    SpriteRenderer m_SpriteRenderer;
    //The Color to be assigned to the Renderer’s Material
    Color m_NewColor = new Color(0, 0, 0);
    [SerializeField]
    GameObject parent;

    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        m_NewColor = parent.GetComponent<SyncColour>().m_NewColor;
        //Fetch the SpriteRenderer from the GameObject
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_SpriteRenderer.color = m_NewColor;
    }
}
