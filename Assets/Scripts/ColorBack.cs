using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBack : MonoBehaviour {

    public SpriteRenderer spr;

    public Color biomeCol;

    public Color loadingCol;

    public bool isLoading = false;

	// Use this for initialization
	void Start () {
        if (isLoading)
        {
            spr.color = loadingCol;
        }
	}

    public void Color()
    {
        spr.color = biomeCol;
    }
}
