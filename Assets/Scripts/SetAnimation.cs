using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAnimation : MonoBehaviour {

    public int anim = 0;

	// Use this for initialization
	void Start () {
        GetComponent<Animator>().SetInteger("AnimationNum", anim);
	}

}
