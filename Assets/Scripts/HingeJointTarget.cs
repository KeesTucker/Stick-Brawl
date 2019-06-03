using UnityEngine;
using System.Collections;

public class HingeJointTarget : MonoBehaviour {

    public HingeJoint hj;
    public Transform target;
    [Tooltip("Toggle invert if the rotation is backwards.")]
    public bool invert;

	void Start ()
    {

	}
	
	void Update ()
    {
        if (hj != null)
        {
            JointSpring js;
            js = hj.spring;
            js.targetPosition = target.transform.localEulerAngles.z;
            if (js.targetPosition > 180)
                js.targetPosition = js.targetPosition - 360;
            if (invert)
                js.targetPosition = js.targetPosition * -1;

            js.targetPosition = js.targetPosition;

            hj.spring = js;
        }
    }
}
