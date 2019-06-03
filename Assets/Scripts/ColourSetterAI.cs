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

    void Start()
    {
        foreach (ColouriserAI cai in GetComponentsInChildren<ColouriserAI>())
        {
            cai.ColourFind();
        }
    }

    public override void OnStartAuthority()
    {
        if (hasAuthority)
        {
            CmdSetColor(SyncData.color);
        }
    }

    [Command]
    public void CmdSetColor(Color c)
    {
        if (GetComponent<PlayerControl>())
        {
            m_NewColor = c; //Replace with colour from home menu
        }
        else
        {
            m_NewColor = Random.ColorHSV(0, 1, 0.5f, 1, 0.5f, 1);
        }

        foreach (ColouriserAI cai in GetComponentsInChildren<ColouriserAI>())
        {
            cai.ColourFind();
        }

        RpcTriggerChildrenColour(m_NewColor);
    }

    [ClientRpc]
    public void RpcTriggerChildrenColour(Color color)
    {
        m_NewColor = color;
        foreach (ColouriserAI cai in GetComponentsInChildren<ColouriserAI>())
        {
            cai.ColourFind();
        }
    }
}