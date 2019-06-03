using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LLLATriggerAI : MonoBehaviour {

    public GameObject local;
    void OnCollisionEnter(Collision collsionInfo)
    {
        if (collsionInfo.collider.gameObject.layer == 12 || collsionInfo.collider.gameObject.layer == 11)
        {
            local.GetComponent<GroundForceAI>().hitLLLA = true;
        }
        if (collsionInfo.collider.gameObject.layer == 16)
        {
            local.GetComponent<GroundForceAI>().hitLLLAObject = true;
        }
    }
    void OnCollisionExit(Collision collsionInfo)
    {
        if (collsionInfo.collider.gameObject.layer == 12 || collsionInfo.collider.gameObject.layer == 11)
        {
            local.GetComponent<GroundForceAI>().hitLLLA = false;
        }
        if (collsionInfo.collider.gameObject.layer == 16)
        {
            local.GetComponent<GroundForceAI>().hitLLLAObject = false;
        }
    }
}
