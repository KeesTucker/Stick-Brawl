﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (Image image in FindObjectsOfType<Image>())
        {
            if (image.gameObject.tag != "DontDisable")
            {
                image.enabled = false;
            }
        }
        foreach (TMPro.TextMeshProUGUI text in FindObjectsOfType<TMPro.TextMeshProUGUI>())
        {
            if (text.gameObject.tag != "DontDisable")
            {
                text.enabled = false;
            }
        }
        foreach (TMPro.TMP_Text text in FindObjectsOfType<TMPro.TextMeshProUGUI>())
        {
            if (text.gameObject.tag != "DontDisable")
            {
                text.enabled = false;
            }
        }
    }
}
