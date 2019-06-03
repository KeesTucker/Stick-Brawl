using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinedBadGame : MonoBehaviour {

    public GameObject message;

	// Use this for initialization
	IEnumerator Start () {
        if (SyncData.failed)
        {
            message.SetActive(true);
            yield return new WaitForSeconds(5);
            message.SetActive(false);
        }
        else{
            message.SetActive(false);
        }
	}
}
