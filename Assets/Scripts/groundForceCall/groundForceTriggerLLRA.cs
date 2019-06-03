using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundForceTriggerLLRA : MonoBehaviour {
    void OnCollisionEnter(Collision collsionInfo)
    {
        if (collsionInfo.collider.gameObject.layer == 12 || collsionInfo.collider.gameObject.layer == 11)
        {
            GameObject.Find("Ragdoll").GetComponent<groundForce>().hitLLRA = true;
        }
        if (collsionInfo.collider.gameObject.layer == 16)
        {
            GameObject.Find("Ragdoll").GetComponent<groundForce>().hitLLRAObject = true;
        }
    }
    void OnCollisionExit(Collision collsionInfo)
    {
        if (collsionInfo.collider.gameObject.layer == 12 || collsionInfo.collider.gameObject.layer == 11)
        {
            GameObject.Find("Ragdoll").GetComponent<groundForce>().hitLLRA = false;
        }
        if (collsionInfo.collider.gameObject.layer == 16)
        {
            GameObject.Find("Ragdoll").GetComponent<groundForce>().hitLLRAObject = false;
        }
    }
}
