using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillHolder : MonoBehaviour {

	// Use this for initialization
	IEnumerator Start () {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
	}
}
