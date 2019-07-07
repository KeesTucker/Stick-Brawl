using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayLoadingOnReconnect : MonoBehaviour
{
    public GameObject loadingObject;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(SyncData.backToHome);
        if ((SyncData.reconnect || SyncData.reconnectServer) && !SyncData.backToHome)
        {
            loadingObject.SetActive(true);
        }
    }

    /*void Update()
    {
        if (!SyncData.reconnect && !SyncData.reconnectServer && loadingObject.activeInHierarchy)
        {
            loadingObject.SetActive(false);
        }
    }*/
}
