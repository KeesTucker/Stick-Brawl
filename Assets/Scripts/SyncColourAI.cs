using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SyncColourAI : NetworkBehaviour
{

    [SyncVar]
    public Color m_NewColor;

    void Start()
    {
        if (hasAuthority)
        {
            CmdSetColor(GetComponent<PlayerSetupAI>().parent.GetComponent<ColourSetter>().m_NewColor);
        }
    }

    [Command]
    void CmdSetColor(Color newColor)
    {
        m_NewColor = newColor;
    }
}
