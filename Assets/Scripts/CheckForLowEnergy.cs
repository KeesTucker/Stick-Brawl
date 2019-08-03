using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForLowEnergy : MonoBehaviour
{
    // Start is called before the first frame update
    public bool energyOn = false;

    void Start()
    {
        if (energyOn)
        {
            if (PlayerPrefs.GetInt("Energy") <= 0)
            {
                FindObjectOfType<Energy>().DepleteEnergy();
            }
        }
    }
}
