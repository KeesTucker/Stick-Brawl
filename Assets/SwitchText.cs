using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchText : MonoBehaviour
{
    private bool grappling = true;

    public TMPro.TextMeshProUGUI text;

    public void ButtonClick()
    {
        grappling = !grappling;
        if (grappling)
        {
            text.text = "GRAPPLE";
        }
        else
        {
            text.text = "SHOOT";
        }
    }
}
