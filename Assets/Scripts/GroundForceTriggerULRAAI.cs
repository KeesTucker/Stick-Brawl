using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundForceTriggerULRAAI : MonoBehaviour
{
    public GameObject local;
    void OnCollisionEnter(Collision collsionInfo)
    {
        if (collsionInfo.collider.gameObject.layer == 12 || collsionInfo.collider.gameObject.layer == 11)
        {
            local.GetComponent<groundForce>().hitULRA = true;
        }
        if (collsionInfo.collider.gameObject.layer == 16)
        {
            local.GetComponent<groundForce>().hitULRAObject = true;
        }
    }
    void OnCollisionExit(Collision collsionInfo)
    {
        if (collsionInfo.collider.gameObject.layer == 12 || collsionInfo.collider.gameObject.layer == 11)
        {
            local.GetComponent<groundForce>().hitULRA = false;
        }
        if (collsionInfo.collider.gameObject.layer == 16)
        {
            local.GetComponent<groundForce>().hitULRAObject = false;
        }
    }
}
