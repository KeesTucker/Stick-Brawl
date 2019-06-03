using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundForceTriggerULRA : MonoBehaviour {
    void OnCollisionEnter(Collision collsionInfo)
    {
        if (collsionInfo.collider.gameObject.layer == 12 || collsionInfo.collider.gameObject.layer == 11)
        {
            GameObject.Find("Ragdoll").GetComponent<groundForce>().hitULRA = true;
        }
        if (collsionInfo.collider.gameObject.layer == 16)
        {
            GameObject.Find("Ragdoll").GetComponent<groundForce>().hitULRAObject = true;
        }
    }
    void OnCollisionExit(Collision collsionInfo)
    {
        if (collsionInfo.collider.gameObject.layer == 12 || collsionInfo.collider.gameObject.layer == 11)
        {
            GameObject.Find("Ragdoll").GetComponent<groundForce>().hitULRA = false;
        }
        if (collsionInfo.collider.gameObject.layer == 16)
        {
            GameObject.Find("Ragdoll").GetComponent<groundForce>().hitULRAObject = false;
        }
    }
}
