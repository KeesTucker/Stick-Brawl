using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollide : MonoBehaviour {

    void OnCollisionEnter(Collision collsionInfo)
    {
        if(collsionInfo.gameObject.layer == 24)
        {
            StartCoroutine(HitPlayer());
        }
        else
        {
            StartCoroutine(HitOther());
        }
    }

    IEnumerator HitPlayer()
    {
        yield return new WaitForSeconds(0.5f); //Change depending on how much it knocks player back
        Destroy(gameObject);
    }
    IEnumerator HitOther()
    {
        yield return new WaitForSeconds(0.2f); //Change depending on how much it knocks player back
        Destroy(gameObject);
    }
}
