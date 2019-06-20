using UnityEngine;
using Mirror;

public class ColourSetterAI : NetworkBehaviour
{
    [SyncVar]
    public Color m_NewColor;

    public Material grappleMat;
    //These are the values that the Color Sliders return
    float m_Red, m_Blue, m_Green;

    public bool local = false;

    public override void OnStartAuthority()
    {
        if (hasAuthority)
        {
            Debug.Log("Color: " + SyncData.color);
            CmdSetColor(SyncData.color, SyncData.skinID);
        }
    }

    [Command]
    public void CmdSetColor(Color c, int id)
    {
        if (GetComponent<PlayerControl>())
        {
            m_NewColor = c; //Replace with colour from home menu
            GetComponent<SkinApply>().UpdateSkin(id);
        }
        else
        {
            m_NewColor = Random.ColorHSV(0, 1, 0.5f, 1, 0.5f, 1);
        }

        foreach (ColouriserAI cai in GetComponentsInChildren<ColouriserAI>())
        {
            cai.ColourFind(m_NewColor);
        }

        Debug.Log("Color object NameCmd: " + gameObject.name);

        RpcTriggerChildrenColour(m_NewColor, id);
    }

    [ClientRpc]
    public void RpcTriggerChildrenColour(Color color, int id)
    {
        m_NewColor = color;
        if (GetComponent<PlayerControl>())
        {
            GetComponent<SkinApply>().UpdateSkin(id);
        }
        foreach (ColouriserAI cai in GetComponentsInChildren<ColouriserAI>())
        {
            cai.ColourFind(m_NewColor);
        }

        Debug.Log("Color object NameRpc: " + gameObject.name);
    }
}