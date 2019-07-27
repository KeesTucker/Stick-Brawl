using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForLowEnergy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Energy") <= 0)
        {
            FindObjectOfType<Energy>().DepleteEnergy();
        }
    }
}
