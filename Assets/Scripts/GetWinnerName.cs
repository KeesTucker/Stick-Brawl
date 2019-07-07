using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetWinnerName : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text;

    void OnEnable()
    {
        StartCoroutine(WAITBITCH());
    }

    IEnumerator WAITBITCH()
    {
        yield return new WaitForEndOfFrame();
        text.text = "Well DOne " + GameObject.Find("Nametag(Clone)").transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text + " You won";
    }
}
