using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour {

    IEnumerator OnCollisionEnter(Collision collsionInfo)
    {
        if (collsionInfo.gameObject.layer == 24 || collsionInfo.gameObject.layer == 18 || collsionInfo.gameObject.layer == 19)
        {
            yield return new WaitForSeconds(1f);
            GetComponent<Rigidbody>().isKinematic = false;
        }
        else if (collsionInfo.gameObject.layer == 9 || collsionInfo.gameObject.layer == 13)
        {
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
