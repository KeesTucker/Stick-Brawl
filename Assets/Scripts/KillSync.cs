using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSync : MonoBehaviour {

    public int kills;

    public TMPro.TMP_Text text;

    // Use this for initialization
    void Start()
    {
        SyncData.kills = 0;
        kills = 0;
        text.text = kills.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (kills != SyncData.kills)
        {
            kills = SyncData.kills;
            text.text = kills.ToString();
        }
    }
}
