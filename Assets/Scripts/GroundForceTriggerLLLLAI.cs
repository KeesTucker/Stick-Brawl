using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundForceTriggerLLLLAI : MonoBehaviour
{
    public GameObject local;
    void OnCollisionEnter(Collision collsionInfo)
    {
        if (collsionInfo.collider.gameObject.layer == 12 || collsionInfo.collider.gameObject.layer == 11)
        {
            local.GetComponent<groundForce>().hitLLLL = true;
        }
        if (collsionInfo.collider.gameObject.layer == 16)
        {
            local.GetComponent<groundForce>().hitLLLLObject = true;
        }
    }
    void OnCollisionExit(Collision collsionInfo)
    {
        if (collsionInfo.collider.gameObject.layer == 12 || collsionInfo.collider.gameObject.layer == 11)
        {
            local.GetComponent<groundForce>().hitLLLL = false;
        }
        if (collsionInfo.collider.gameObject.layer == 16)
        {
            local.GetComponent<groundForce>().hitLLLLObject = false;
        }
    }
}
