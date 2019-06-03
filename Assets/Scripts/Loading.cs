using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour {

	// Use this for initialization
	IEnumerator Start () {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
	}
}
