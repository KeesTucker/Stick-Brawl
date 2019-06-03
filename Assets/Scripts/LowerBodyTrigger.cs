using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerBodyTrigger : MonoBehaviour
{
    public GameObject local;
    void OnCollisionEnter(Collision collsionInfo)
    {
        if (collsionInfo.collider.gameObject.layer == 12 || collsionInfo.collider.gameObject.layer == 11)
        {
            local.GetComponent<GroundForceAI>().hitLowerBody = true;
        }
    }
    void OnCollisionExit(Collision collsionInfo)
    {
        if (collsionInfo.collider.gameObject.layer == 12 || collsionInfo.collider.gameObject.layer == 11)
        {
            local.GetComponent<GroundForceAI>().hitLowerBody = false;
        }
    }
}
