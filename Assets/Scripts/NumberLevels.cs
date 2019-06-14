using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberLevels : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = "Level " + (21 - i).ToString();
            transform.GetChild(i).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "Grade: A";
        }
    }
}
