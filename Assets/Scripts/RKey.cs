using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RKey : MonoBehaviour {

    public TMPro.TMP_Text text;

	// Use this for initialization
	void Start () {
        text.text = SyncData.r.ToString();
	}
}
