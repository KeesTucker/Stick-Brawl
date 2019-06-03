using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBottom : MonoBehaviour {

    public bool kill = true;

    void OnCollisionEnter(Collision collsionInfo)
    {
        if ((collsionInfo.gameObject.name == "Killer" || collsionInfo.gameObject.name == "KillerPart" || collsionInfo.gameObject.name == "KillPlayers") && kill)
        {
            Destroy(gameObject);
        }
    }
}
