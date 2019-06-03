using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LLLLTriggerAI : MonoBehaviour {

    public GameObject local;
    void OnCollisionEnter(Collision collsionInfo)
    {
        if (collsionInfo.collider.gameObject.layer == 12 || collsionInfo.collider.gameObject.layer == 11)
        {
            local.GetComponent<GroundForceAI>().hitLLLL = true;
        }
        if (collsionInfo.collider.gameObject.layer == 16)
        {
            local.GetComponent<GroundForceAI>().hitLLLLObject = true;
        }
    }
    void OnCollisionExit(Collision collsionInfo)
    {
        if (collsionInfo.collider.gameObject.layer == 12 || collsionInfo.collider.gameObject.layer == 11)
        {
            local.GetComponent<GroundForceAI>().hitLLLL = false;
        }
        if (collsionInfo.collider.gameObject.layer == 16)
        {
            local.GetComponent<GroundForceAI>().hitLLLLObject = false;
        }
    }
}
