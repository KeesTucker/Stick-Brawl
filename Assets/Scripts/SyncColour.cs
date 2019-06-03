using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SyncColour : NetworkBehaviour {

    [SyncVar]
    public Color m_NewColor;

    void Start()
    {
        if (gameObject.name == "LocalRelay")
        {
            CmdSetColor(GameObject.Find("Local").GetComponent<ColourSetter>().m_NewColor);
        }
    }

    [Command]
    void CmdSetColor(Color newColor)
    {
        m_NewColor = newColor;
    }

}
