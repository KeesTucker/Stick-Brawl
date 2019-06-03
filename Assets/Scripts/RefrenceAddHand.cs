using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefrenceAddHand : MonoBehaviour
{

    // Use this for initialization
    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        gameObject.SetActive(false);
    }
}
