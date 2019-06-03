using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerRefrenceKeeper : MonoBehaviour {

    public int numOfWeapons;

    public UpdateUI updateUI;

    public bool e;

    public bool i;

    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            e = true;
        }
        if (Input.GetKeyUp("e"))
        {
            e = false;
        }

        if (Input.GetKeyDown("i"))
        {
            i = true;
            updateUI.OpenClose();
        }
        if (Input.GetKeyUp("i"))
        {
            i = false;
        }
    }

}
