using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundForceTriggerLLLA : MonoBehaviour {
    public bool gound;
    void OnCollisionEnter(Collision collsionInfo)
    {
        if (collsionInfo.collider.gameObject.layer == 12 || collsionInfo.collider.gameObject.layer == 11)
        {
            GameObject.Find("Ragdoll").GetComponent<groundForce>().hitLLLA = true;
        }
        if (collsionInfo.collider.gameObject.layer == 16)
        {
            GameObject.Find("Ragdoll").GetComponent<groundForce>().hitLLLAObject = true;
        }
    }
    void OnCollisionExit(Collision collsionInfo)
    {
        if (collsionInfo.collider.gameObject.layer == 12 || collsionInfo.collider.gameObject.layer == 11)
        {
            GameObject.Find("Ragdoll").GetComponent<groundForce>().hitLLLA = false;
        }
        if (collsionInfo.collider.gameObject.layer == 16)
        {
            GameObject.Find("Ragdoll").GetComponent<groundForce>().hitLLLAObject = false;
        }
    }
}
