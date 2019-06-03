using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundForceTriggerLLRAAI : MonoBehaviour
{
    public GameObject local;
    void OnCollisionEnter(Collision collsionInfo)
    {
        if (collsionInfo.collider.gameObject.layer == 12 || collsionInfo.collider.gameObject.layer == 11)
        {
            local.GetComponent<groundForce>().hitLLRA = true;
        }
        if (collsionInfo.collider.gameObject.layer == 16)
        {
            local.GetComponent<groundForce>().hitLLRAObject = true;
        }
    }
    void OnCollisionExit(Collision collsionInfo)
    {
        if (collsionInfo.collider.gameObject.layer == 12 || collsionInfo.collider.gameObject.layer == 11)
        {
            local.GetComponent<groundForce>().hitLLRA = false;
        }
        if (collsionInfo.collider.gameObject.layer == 16)
        {
            local.GetComponent<groundForce>().hitLLRAObject = false;
        }
    }
}
