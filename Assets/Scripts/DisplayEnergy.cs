using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayEnergy : MonoBehaviour
{
    public Image meter;

    public GameObject[] objects;

    int count;

    public bool energyOn = false;

    void Start()
    {
        if (PlayerPrefs.HasKey("BrawlPro") || !energyOn)
        {
            foreach (GameObject GO in objects)
            {
                Destroy(GO);
            }
        }
    }

    void Update()
    {
        count++;
        if (count > 60)
        {
            count = 0;
            meter.fillAmount = (float)PlayerPrefs.GetInt("Energy") / 7f;

            if (PlayerPrefs.HasKey("BrawlPro"))
            {
                foreach (GameObject GO in objects)
                {
                    Destroy(GO);
                }
            }
        }
    }
}
