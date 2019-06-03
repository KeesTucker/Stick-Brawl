using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallNoPlayer : MonoBehaviour {

    void OnCollisionEnter(Collision collsionInfo)
    {
        if (collsionInfo.gameObject.layer == 9)
        {
            GetComponent<Rigidbody>().isKinematic = false;
            
        }
    }
}
