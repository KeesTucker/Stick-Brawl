using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundForceTriggerLLRLAI : MonoBehaviour
{
    public GameObject local;
    void OnCollisionEnter(Collision collsionInfo)
    {
        if (collsionInfo.collider.gameObject.layer == 12 || collsionInfo.collider.gameObject.layer == 11)
        {
            local.GetComponent<groundForce>().hitLLRL = true;
        }
        if (collsionInfo.collider.gameObject.layer == 16)
        {
            local.GetComponent<groundForce>().hitLLRLObject = true;
        }
    }
    void OnCollisionExit(Collision collsionInfo)
    {
        if (collsionInfo.collider.gameObject.layer == 12 || collsionInfo.collider.gameObject.layer == 11)
        {
            local.GetComponent<groundForce>().hitLLRL = false;
        }
        if (collsionInfo.collider.gameObject.layer == 16)
        {
            local.GetComponent<groundForce>().hitLLRLObject = false;
        }
    }
}
