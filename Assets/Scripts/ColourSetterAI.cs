using UnityEngine;
using Mirror;
using System.Collections;

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
        if (gameObject.name == "Player(Clone)")
        {
            m_NewColor = Random.ColorHSV(0f, 1f, 0.3f, 0.7f, 0.5f, 1f);
            foreach (ColouriserAI cai in GetComponentsInChildren<ColouriserAI>())
            {
                cai.ColourFind();
            }
        }
    }

    public override void OnStartAuthority()
    {
        StartCoroutine(ServerSet());
    }

    IEnumerator ServerSet()
    {
        yield return new WaitForEndOfFrame();
        if (gameObject.name == "LocalPlayer")
        {
            CmdSetColor(SyncData.color, SyncData.skinID);
            yield return new WaitForSeconds(0.3f);
            CmdSetColor(SyncData.color, SyncData.skinID);
            yield return new WaitForSeconds(1f);
            CmdSetColor(SyncData.color, SyncData.skinID);
            yield return new WaitForSeconds(5f);
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
            cai.ColourFind();
        }

        RpcTriggerChildrenColour(c, id);
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
            cai.ColourFind();
        }
    }
}