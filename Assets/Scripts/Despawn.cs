using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawn : MonoBehaviour {
    public float secondsLeft = 1f;
	// Use this for initialization
	void Start () {
        StartCoroutine("Destroyer");
	}

    IEnumerator Destroyer()
    {
        yield return new WaitForSeconds(secondsLeft);
        Destroy(gameObject);
    }
}
