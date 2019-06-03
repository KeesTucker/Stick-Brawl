using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achieve : MonoBehaviour {

    public TMPro.TMP_InputField text;

    public int wins = 0;
    public int seconds = 0;

	// Use this for initialization
	void Start () {
        if (PlayerPrefs.HasKey("wins"))
        {
            wins = PlayerPrefs.GetInt("wins");
        }
        if (PlayerPrefs.HasKey("secondsSurvived"))
        {
            seconds = PlayerPrefs.GetInt("secondsSurvived");
        }
        text.text = "WINS: " + wins.ToString() + " SECONDS SURVIVED: " + seconds.ToString();
    }
}
