using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillDust : MonoBehaviour {

    public float time = 1f;

	// Use this for initialization
	IEnumerator Start () {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
	}
}
