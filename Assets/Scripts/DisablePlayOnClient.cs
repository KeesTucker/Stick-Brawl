using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class DisablePlayOnClient : MonoBehaviour
{
    public Button button;
    public TMPro.TMP_Text text;
    public MoveTextWithClick moveText;

    void Update()
    {
        if (!NetworkServer.active && NetworkClient.isConnected)
        {
            text.text = "Host is choosing!";
            button.interactable = false;
            moveText.disabled = true;
        }
        else
        {
            text.text = "Play";
            button.interactable = true;
            moveText.disabled = false;
        }
    }
}
