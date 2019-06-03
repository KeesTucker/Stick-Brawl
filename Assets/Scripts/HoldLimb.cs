using UnityEngine;

public class HoldLimb : MonoBehaviour {

    public HingeJoint hj;
    public Transform tr;
    public Vector3 adder;
    public float z = -3.72529f;
    private int count = 0;

    public Vector3 location;
	// Use this for initialization
	void Start () {
        tr = transform;
        hj = gameObject.GetComponent<HingeJoint>();
        location = hj.connectedAnchor - hj.anchor + adder;
        location.z = z;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        /*if (count == 20)
        {
            //tr.localPosition = location;
            count = 0;
        }
        count++;*/
	}
}
