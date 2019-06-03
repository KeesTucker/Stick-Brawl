using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LLRATriggerAI : MonoBehaviour {

    public GameObject local;
    void OnCollisionEnter(Collision collsionInfo)
    {
        if (collsionInfo.collider.gameObject.layer == 12 || collsionInfo.collider.gameObject.layer == 11)
        {
            local.GetComponent<GroundForceAI>().hitLLRA = true;
        }
        if (collsionInfo.collider.gameObject.layer == 16)
        {
            local.GetComponent<GroundForceAI>().hitLLRAObject = true;
        }
    }
    void OnCollisionExit(Collision collsionInfo)
    {
        if (collsionInfo.collider.gameObject.layer == 12 || collsionInfo.collider.gameObject.layer == 11)
        {
            local.GetComponent<GroundForceAI>().hitLLRA = false;
        }
        if (collsionInfo.collider.gameObject.layer == 16)
        {
            local.GetComponent<GroundForceAI>().hitLLRAObject = false;
        }
    }
}
