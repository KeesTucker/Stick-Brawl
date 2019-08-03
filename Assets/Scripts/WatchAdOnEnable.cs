using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchAdOnEnable : MonoBehaviour
{
    void OnEnable()
    {
        if (Random.Range(0, 3) == 1 && !PlayerPrefs.HasKey("BrawlPro"))
        {
            FindObjectOfType<ShowAds>().IntersitialAd();
        }
    }
}
