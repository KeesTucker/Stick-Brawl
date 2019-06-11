using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsDisplay : MonoBehaviour
{
    public TMPro.TMP_Text text;

    void Start()
    {
        if (!PlayerPrefs.HasKey("Counters"))
        {
            PlayerPrefs.SetInt("Counters", 250);
        }
        text.text = PlayerPrefs.GetInt("Counters").ToString();
    }

    public void UpdateAmount()
    {
        text.text = PlayerPrefs.GetInt("Counters").ToString();
    }
}
