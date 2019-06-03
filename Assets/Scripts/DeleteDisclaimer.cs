using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteDisclaimer : MonoBehaviour {

	// Update is called once per frame
    void Start()
    {
        GameObject.Find("LoadingPlayer").GetComponent<Rigidbody>().isKinematic = true;
    }

	void Update () {
        if (Input.GetKey("space"))
        {
            gameObject.SetActive(false);
            GameObject.Find("LoadingPlayer").GetComponent<Rigidbody>().isKinematic = false;
        }
	}
}
