using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ULLLTriggerAI : MonoBehaviour {

    public GameObject local;
    void OnCollisionEnter(Collision collsionInfo)
    {
        if (collsionInfo.collider.gameObject.layer == 12 || collsionInfo.collider.gameObject.layer == 11)
        {
            local.GetComponent<GroundForceAI>().hitULLL = true;
        }
        if (collsionInfo.collider.gameObject.layer == 16)
        {
            local.GetComponent<GroundForceAI>().hitULLLObject = true;
        }
    }
    void OnCollisionExit(Collision collsionInfo)
    {
        if (collsionInfo.collider.gameObject.layer == 12 || collsionInfo.collider.gameObject.layer == 11)
        {
            local.GetComponent<GroundForceAI>().hitULLL = false;
        }
        if (collsionInfo.collider.gameObject.layer == 16)
        {
            local.GetComponent<GroundForceAI>().hitULLLObject = false;
        }
    }
}
