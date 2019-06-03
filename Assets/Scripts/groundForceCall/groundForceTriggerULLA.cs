using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundForceTriggerULLA : MonoBehaviour {
    void OnCollisionEnter(Collision collsionInfo)
    {
        if (collsionInfo.collider.gameObject.layer == 12 || collsionInfo.collider.gameObject.layer == 11)
        {
            GameObject.Find("Ragdoll").GetComponent<groundForce>().hitULLA = true;
        }
        if (collsionInfo.collider.gameObject.layer == 16)
        {
            GameObject.Find("Ragdoll").GetComponent<groundForce>().hitULLAObject = true;
        }
    }
    void OnCollisionExit(Collision collsionInfo)
    {
        if (collsionInfo.collider.gameObject.layer == 12 || collsionInfo.collider.gameObject.layer == 11)
        {
            GameObject.Find("Ragdoll").GetComponent<groundForce>().hitULLA = false;
        }
        if (collsionInfo.collider.gameObject.layer == 16)
        {
            GameObject.Find("Ragdoll").GetComponent<groundForce>().hitULLAObject = false;
        }
    }
}
