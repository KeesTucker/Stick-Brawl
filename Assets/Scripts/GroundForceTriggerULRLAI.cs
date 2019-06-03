using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundForceTriggerULRLAI : MonoBehaviour
{
    public GameObject local;
    void OnCollisionEnter(Collision collsionInfo)
    {
        if (collsionInfo.collider.gameObject.layer == 12 || collsionInfo.collider.gameObject.layer == 11)
        {
            local.GetComponent<groundForce>().hitULRL = true;
        }
        if (collsionInfo.collider.gameObject.layer == 16)
        {
            local.GetComponent<groundForce>().hitULRLObject = true;
        }
    }
    void OnCollisionExit(Collision collsionInfo)
    {
        if (collsionInfo.collider.gameObject.layer == 12 || collsionInfo.collider.gameObject.layer == 11)
        {
            local.GetComponent<groundForce>().hitULRL = false;
        }
        if (collsionInfo.collider.gameObject.layer == 16)
        {
            local.GetComponent<groundForce>().hitULRLObject = false;
        }
    }
}
