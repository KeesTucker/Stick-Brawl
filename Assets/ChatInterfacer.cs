using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ChatInterfacer : NetworkBehaviour
{
    [Command]
    public void CmdSendChat(string message)
    {
        RpcRecieveChat(message);
    }

    [ClientRpc]
    public void RpcRecieveChat(string message)
    {
        FindObjectOfType<MyParty>().CreateChat(message);
    }
}
