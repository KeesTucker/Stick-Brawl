using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCampaign : MonoBehaviour
{
    void Start()
    {
        if (SyncData.openCampaignScreen)
        {
            GetComponent<UINavigation>().Switch();
        }
    }
}
