using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchToLastShop : MonoBehaviour
{
    public DisplayStoreItems[] buttons;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("lastShop"))
        {
            buttons[PlayerPrefs.GetInt("lastShop")].Switch();
        }
        else
        {
            buttons[0].Switch();
        }
    }
}
