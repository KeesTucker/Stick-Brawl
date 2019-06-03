using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HingeSwap : MonoBehaviour {

    public HingeJoint hj;
    public HingeJoint hjFallBack;

    public GroundForceAI groundForce;

    private JointSpring js;

    /*void Start()
    {
        js = hjFallBack.spring;
        js.spring = hj.spring.spring / 10;
        hjFallBack.spring = js;
    }*/

	// Update is called once per frame
	void Update () {
        if (hj)
        {
            if (hj.useSpring)
            {
                hjFallBack.useSpring = false;
            }
            else if (gameObject.name == "Lower Body")
            {
                hjFallBack.useSpring = true;
            }
            else if (groundForce.grappled)
            {
                hjFallBack.useSpring = false;
            }
            else
            {
                //hjFallBack.useSpring = true;
            }
        }
        else if (hjFallBack)
        {
            hjFallBack.useSpring = true;
        }
	}
}
