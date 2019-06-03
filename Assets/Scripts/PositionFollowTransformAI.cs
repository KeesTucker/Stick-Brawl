using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionFollowTransformAI : MonoBehaviour {

    public Transform playert;

    public bool run = false;

    void Start()
    {
        if (GetComponent<PlayerSetupAI>().parent)
        {
            playert = GetComponent<PlayerSetupAI>().parent.transform.GetChild(0);
            run = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (run)
        {
            transform.position = playert.position;
        }
    }
}
