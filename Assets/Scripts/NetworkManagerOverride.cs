using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkManagerOverride : NetworkErrorHandler
{
    public override void ErrorSend(string arg0)
    {
        base.ErrorSend(arg0);
        GetComponent<NetworkErrors>().Error(arg0);
    }
}
