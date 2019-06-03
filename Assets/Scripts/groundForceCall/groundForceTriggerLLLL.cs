using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundForceTriggerLLLL : MonoBehaviour {
    void OnCollisionEnter(Collision collsionInfo)
    {
        if (collsionInfo.collider.gameObject.layer == 12 || collsionInfo.collider.gameObject.layer == 11)
        {
            GameObject.Find("Ragdoll").GetComponent<groundForce>().hitLLLL = true;
        }
        if (collsionInfo.collider.gameObject.layer == 16)
        {
            GameObject.Find("Ragdoll").GetComponent<groundForce>().hitLLLLObject = true;
        }
    }
    void OnCollisionExit(Collision collsionInfo)
    {
        if (collsionInfo.collider.gameObject.layer == 12 || collsionInfo.collider.gameObject.layer == 11)
        {
            GameObject.Find("Ragdoll").GetComponent<groundForce>().hitLLLL = false;
        }
        if (collsionInfo.collider.gameObject.layer == 16)
        {
            GameObject.Find("Ragdoll").GetComponent<groundForce>().hitLLLLObject = false;
        }
    }
}
