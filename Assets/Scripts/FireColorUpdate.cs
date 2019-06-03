using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireColorUpdate : MonoBehaviour {

    public ColourSetSliders colourSet;

    public Rigidbody rb;

	void FixedUpdate()
    {
        if (rb.velocity.x != 0)
        {
            colourSet.UpdateColor();
        }
    }
}
